using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Core.IRespository
{
    public interface IUnitOfWork : IDisposable
    {
        public ISupplierRepository Supplier {  get; }
        public IPurchaseItemsRepository PurchaseItems { get;}
        public IPurchaseOrdersRepository PurchaseOrders { get;}
        public IInvoiceRepository Invoice { get;  }
        Task<int> Save();
    }
}
