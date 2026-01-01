using FougeraClub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Core.IRespository
{
    public interface IPurchaseOrdersRepository: IRepository<PurchaseOrders>
    {
        public Task<List<PurchaseOrders>> GetAllOrderandSupplier(DateOnly? fromDate = null , DateOnly? toDate = null , string? SupplierName = null );
        public Task<PurchaseOrders> GetAllOrderandSupplierAndItems(int id);
    }
}
