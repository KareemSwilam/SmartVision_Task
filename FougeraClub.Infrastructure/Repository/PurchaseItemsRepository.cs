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
    }
}
