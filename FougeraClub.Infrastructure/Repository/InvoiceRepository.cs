using FougeraClub.Core.IRespository;
using FougeraClub.Core.Models;
using FougeraClub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Infrastructure.Repository
{
    public class InvoiceRepository: Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationContext context ): base( context ) { }

        public async Task<Invoice> GetInvoiceWithItems(int id)
        {
            IQueryable<Invoice> query =  _db.Where( i => i.Id == id ).Include(i => i.PurchaseItems);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }
    }
}
