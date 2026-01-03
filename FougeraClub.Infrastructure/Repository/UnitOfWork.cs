using FougeraClub.Core.IRespository;
using FougeraClub.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public ISupplierRepository Supplier { get; private set; }

        public IPurchaseItemsRepository PurchaseItems { get; private set; }

        public IPurchaseOrdersRepository PurchaseOrders { get; private set; }
        public IInvoiceRepository Invoice { get; private set; }
        public UnitOfWork(ApplicationContext context,
                          ISupplierRepository supplier,
                          IPurchaseItemsRepository purchaseItems,
                          IPurchaseOrdersRepository purchaseOrders,
                          IInvoiceRepository invoice)
        {
            _context = context;
            Supplier = supplier;
            PurchaseItems = purchaseItems;
            PurchaseOrders = purchaseOrders;
            Invoice = invoice;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
