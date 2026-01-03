using FougeraClub.Core.IRespository;
using FougeraClub.Core.Models;
using FougeraClub.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Infrastructure.Repository
{
    public class PurchaseItemsRepository: Repository<PurchaseItems>,IPurchaseItemsRepository
    {
        public PurchaseItemsRepository(ApplicationContext context) : base(context) { }

        public  double GetSubTotalPerOrder(int Orderid)
        {
            IQueryable<PurchaseItems> query =  _db.Where(pi => pi.PurchaseOrdersId == Orderid);
            var subTotal = query.Sum(pi => pi.TotalInLine);
            return subTotal;
        }
    }
}
