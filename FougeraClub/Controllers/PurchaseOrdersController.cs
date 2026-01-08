using FougeraClub.Services.DTOs.PurchaseItemsDtos;
using FougeraClub.Services.DTOs.PurchaseOrdersDtos;
using FougeraClub.Services.DTOs.SupplierDtos;
using FougeraClub.Services.IServices;
using FougeraClub.Services.Services;
using FougeraClub.Services.Validations;
using FougeraClub.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FougeraClub.Controllers
    {
        
        public class PurchaseOrderController : Controller
        {
            private readonly IPurchaseOrdersServices _purchaseOrdersServices;
            private readonly IPurchaseItemsServices _purchaseItemsServices;
            private readonly ISupplierServices _supplierServices;
            private readonly IInvoiceServices _invoiceServices;
        private readonly IUserServices _userServices;

            public PurchaseOrderController(
                IPurchaseOrdersServices purchaseOrdersServices,
                IPurchaseItemsServices purchaseItemsServices,
                ISupplierServices supplierServices,
                IInvoiceServices invoiceServices,
                IUserServices userServices)
            {
                _purchaseOrdersServices = purchaseOrdersServices;
                _purchaseItemsServices = purchaseItemsServices;
                _supplierServices = supplierServices;
                _invoiceServices = invoiceServices;
                _userServices = userServices;
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
        public async Task<IActionResult> Create(CreatePurchaseOrderVM model)
        {
            

            try
            {
                
                var orderResult = await _purchaseOrdersServices.AddOrders(model.Order);

                if (orderResult.IsSuccess)
                {
                    
                    var lastOrder = await _purchaseOrdersServices.GetLastOrderNumber(); // أو طريقة لاسترجاع الـ ID الفعلي
                    int newOrderId = lastOrder.Value;

                    
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
                
                var result = await _purchaseOrdersServices.DeleteOrder(id);

                if (result.IsSuccess)
                {
                    
                    TempData["ToastType"] = "success";
                    TempData["ToastMessage"] = "تم حذف أمر الشراء بنجاح";
                }
                else
                {
                   
                    TempData["ToastType"] = "error";
                    TempData["ToastMessage"] = "فشل الحذف: " + (result.Error?.Message ?? "حدث خطأ غير متوقع");
                }
            }
            catch (Exception ex)
            {
                TempData["ToastType"] = "error";
                TempData["ToastMessage"] = "خطأ أثناء الحذف: " + ex.Message;
            }

           
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           
            var orderResult = await _purchaseOrdersServices.GetOrderAndSupplierWithFilter();
            var orderData = orderResult.Value.FirstOrDefault(x => x.Id == id);

            if (orderData == null) return NotFound();

            
            var itemsResult = await _purchaseItemsServices.GetOrderItems(id); 

           
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

            
            var suppliers = await _supplierServices.GetSuppliers();
            ViewBag.Suppliers = suppliers.Value;
            ViewBag.IsEdit = true;
            ViewBag.OrderId = id;

            return View(viewModel); 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePurchaseOrderVm model)
        {
            
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
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                
                var result = await _invoiceServices.GetAllDetailsInvoiceByOrderId(id);

                if (result.IsSuccess)
                {
                    var user = await _userServices.GetUSer(3);
                    ViewBag.User = user.Value;
                    return View(result.Value);
                }
                else
                {
                    
                    TempData["ToastType"] = "error";
                    TempData["ToastMessage"] = result.Error?.Message ?? "تعذر العثور على تفاصيل الفاتورة";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["ToastType"] = "error";
                TempData["ToastMessage"] = "خطأ: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInvoice(int orderId, string otp)
        {
            
            if (otp != "1111")
            {
                TempData["ToastType"] = "error";
                TempData["ToastMessage"] = "رمز التحقق غير صحيح | Invalid OTP";
                return RedirectToAction(nameof(Details), new { id = orderId });
            }

            
            var result = await _invoiceServices.AsignInvoice(orderId);

            if (result.IsSuccess)
            {
                TempData["ToastType"] = "success";
                TempData["ToastMessage"] = "تم التوقيع بنجاح | Signed Successfully";
            }
            else
            {
                TempData["ToastType"] = "error";
                TempData["ToastMessage"] = result.Error?.Message ?? "حدث خطأ أثناء التوقيع";
            }

            
            return RedirectToAction(nameof(Details), new { id = orderId });
        }
    }
}