using FougeraClub.Core.IRespository;
using FougeraClub.Core.Models;
using FougeraClub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Infrastructure.Repository
{
    public class PurchaseOrdersRepository: Repository<PurchaseOrders> , IPurchaseOrdersRepository
    {
        public PurchaseOrdersRepository(ApplicationContext context) : base(context) { }

        public async Task<List<PurchaseOrders>> GetAllOrderandSupplier(DateOnly? fromDate = null, DateOnly? toDate = null, string? SupplierName = null)
        {
            IQueryable<PurchaseOrders> query = _db.Include(po => po.supplier);
            if(fromDate != null )
                query = query.Where(po => po.Date >=  fromDate);
            if(toDate != null)
                query = query.Where(po => po.Date <= fromDate);
            if(SupplierName != null)
                query = query.Where(po => po.supplier.Name.Contains(SupplierName));
            return await query.ToListAsync();            
        }

        public async Task<PurchaseOrders> GetAllOrderandSupplierAndItems(int id)
        {
            IQueryable<PurchaseOrders> query = _db.Include(po => po.supplier)
                                                  .Include(po => po.items);
            return await query.FirstOrDefaultAsync();
        }
    }
}
