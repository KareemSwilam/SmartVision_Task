using FougeraClub.Services.DTOs.PurchaseOrdersDtos;
using FougeraClub.Services.Services;
using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using FougeraClub.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FougeraClub.VM;

namespace FougeraClub.Controllers
    {
        //[Area("Admin")]
        public class PurchaseOrderController : Controller
        {
            private readonly IPurchaseOrdersServices _purchaseOrdersServices;
            private readonly IPurchaseItemsServices _purchaseItemsServices;
            private readonly ISupplierServices _supplierServices;

            public PurchaseOrderController(
                IPurchaseOrdersServices purchaseOrdersServices,
                IPurchaseItemsServices purchaseItemsServices,
                ISupplierServices supplierServices)
            {
                _purchaseOrdersServices = purchaseOrdersServices;
                _purchaseItemsServices = purchaseItemsServices;
                _supplierServices = supplierServices;
            }

            // GET: /Admin/PurchaseOrder/Index
            [HttpGet]
            public async Task<IActionResult> Index(DateOnly? fromDate, DateOnly? toDate,string? VATNumber, int pageNumber = 1, int pageSize = 50)
            {
                try
                {
                    var result = await _purchaseOrdersServices.GetOrderAndSupplierWithFilter(new PurchaseOrdersAndSupplierFilterDto
                    {
                        fromDate = fromDate,
                        toDate = toDate,
                        VaTNumber = VATNumber,

                    });
                    var Supplier = await _supplierServices.GetSuppliers();
                    ViewBag.Suppliers = Supplier.Value;
                    ViewBag.Count = result.Value.Count();
                    ViewBag.CurrentPage = pageNumber;
                    ViewBag.PageSize = pageSize;
                    ViewBag.TotalPages = (int)Math.Ceiling(result.Value.Count() / (double)pageSize);
                    ViewBag.SupplierVATNumber = VATNumber;
                    ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
                    ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

                    return View(result.Value);
                }
                catch (Exception ex)
                {
                    TempData["ToastType"] = "error";
                    TempData["ToastMessage"] = "Error loading purchase orders: " + ex.Message;
                    return View(new List<PurchaseOrdersAndSupplier>());
                }
            }

        // GET: /Admin/PurchaseOrder/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            var suppliersResult = await _supplierServices.GetSuppliers();
            ViewBag.Suppliers = suppliersResult.Value;

            
            var lastNoResult = await _purchaseOrdersServices.GetLastOrderNumber();
            ViewBag.NextOrderNumber = lastNoResult.Value + 1;

            return View(new CreatePurchaseOrderVM());
        }
        // POST: /Admin/PurchaseOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePurchaseOrderVM model)
        {
            //if (!ModelState.IsValid)
            //{
            //    // إعادة تحميل الموردين في حال وجود خطأ في البيانات
            //    var suppliersResult = await _supplierServices.GetSuppliers();
            //    ViewBag.Suppliers = suppliersResult.Value;
            //    return View(model);
            //}

            try
            {
                // 1. إضافة أمر الشراء الأساسي
                var orderResult = await _purchaseOrdersServices.AddOrders(model.Order);

                if (orderResult.IsSuccess)
                {
                    
                    var lastOrder = await _purchaseOrdersServices.GetLastOrderNumber(); // أو طريقة لاسترجاع الـ ID الفعلي
                    int newOrderId = lastOrder.Value;

                    // 2. إضافة الأصناف
                    foreach (var item in model.Items)
                    {
                        await _purchaseItemsServices.AddItem(newOrderId, item);
                    }

                    TempData["ToastType"] = "success";
                    TempData["ToastMessage"] = "تم حفظ أمر الشراء بنجاح";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "حدث خطأ أثناء حفظ الطلب");
            }
            catch (Exception ex)
            {
                TempData["ToastType"] = "error";
                TempData["ToastMessage"] = "خطأ: " + ex.Message;
            }

            
            var suppliers = await _supplierServices.GetSuppliers();
            ViewBag.Suppliers = suppliers.Value;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // استدعاء خدمة الحذف التي قمت بتعريفها
                var result = await _purchaseOrdersServices.DeleteOrder(id);

                if (result.IsSuccess)
                {
                    // إعداد رسالة النجاح لتظهر عبر SweetAlert في صفحة Index
                    TempData["ToastType"] = "success";
                    TempData["ToastMessage"] = "تم حذف أمر الشراء بنجاح";
                }
                else
                {
                    // إعداد رسالة الخطأ في حال فشل الحذف (مثلاً الطلب غير موجود)
                    TempData["ToastType"] = "error";
                    TempData["ToastMessage"] = "فشل الحذف: " + (result.Error?.Message ?? "حدث خطأ غير متوقع");
                }
            }
            catch (Exception ex)
            {
                TempData["ToastType"] = "error";
                TempData["ToastMessage"] = "خطأ أثناء الحذف: " + ex.Message;
            }

            // العودة دائماً إلى صفحة القائمة الرئيسية
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // 1. جلب بيانات أمر الشراء
            var orderResult = await _purchaseOrdersServices.GetOrderAndSupplierWithFilter();
            var orderData = orderResult.Value.FirstOrDefault(x => x.Id == id);

            if (orderData == null) return NotFound();

            // 2. جلب الأصناف التابعة لهذا الأمر
            // ملاحظة: افترضنا وجود خدمة تجلب الأصناف برقم الأوردو، إذا لم تكن موجودة استخدم الـ UnitOfWork مباشرة
            var itemsResult = await _purchaseItemsServices.GetOrderItems(id); // تأكد من وجود هذه الوظيفة

            // 3. تجهيز الـ ViewModel
            var viewModel = new UpdatePurchaseOrderVm
            {
                Order = new PurchaseOrdersUpdateDto
                {
                    Date = orderData.Date,
                    ApplyVAT = orderData.ApplyVAT, 
                    SupplierId = orderData.SupplierId,
                },
                Items = itemsResult.Value.Select(x => new PurchaseItemUpdateDto
                {                  
                    Id = x.Id,
                    Amount = x.Amount,
                    PricePerUnit = x.PricePerUnit,
                    Description = x.Description
                }).ToList()
            };

            // 4. تجهيز البيانات المساعدة
            var suppliers = await _supplierServices.GetSuppliers();
            ViewBag.Suppliers = suppliers.Value;
            ViewBag.IsEdit = true;
            ViewBag.OrderId = id;

            return View(viewModel); // سنستخدم نفس صفحة Create
        }

        // POST: /Admin/PurchaseOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePurchaseOrderVm model)
        {
            // 1. تحديث بيانات الأوردو الأساسية
            var updateDto = new PurchaseOrdersUpdateDto
            {
                Date = model.Order.Date,
                ApplyVAT = model.Order.ApplyVAT,
                SupplierId = model.Order.SupplierId
            };

            var orderUpdateResult = await _purchaseOrdersServices.UpdateOrder(id, updateDto);

            if (orderUpdateResult.IsSuccess)
            {
                
                var currentItemsResult = await _purchaseItemsServices.GetOrderItems(id);
                if (currentItemsResult.IsSuccess)
                {
                    var dbItemIds = currentItemsResult.Value.Select(x => x.Id).ToList();
                    var incomingItemIds = model.Items.Where(x => x.Id > 0).Select(x => x.Id).ToList();

                    
                    var idsToDelete = dbItemIds.Except(incomingItemIds).ToList();

                    foreach (var itemId in idsToDelete)
                    {
                        await _purchaseItemsServices.DeleteItem(id, itemId);
                    }
                }

                
                foreach (var item in model.Items)
                {
                    if (item.Id > 0)
                        await _purchaseItemsServices.UpdateItem(item);
                    
                    else
                    {
                        // صنف جديد (Id = 0) -> إضافة
                        await _purchaseItemsServices.AddItem(id, new PurchaseItemCreateDto
                        {
                            Amount = item.Amount,
                            PricePerUnit = item.PricePerUnit,
                            Description = item.Description
                        });
                    }
                }

                TempData["ToastType"] = "success";
                TempData["ToastMessage"] = "تم تحديث أمر الشراء بنجاح";
                return RedirectToAction(nameof(Index));
            }

            TempData["ToastType"] = "error";
            TempData["ToastMessage"] = "فشل في التعديل";
            return RedirectToAction(nameof(Index));
        }
    }
}