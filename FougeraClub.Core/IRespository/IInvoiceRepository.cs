using FougeraClub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Core.IRespository
{
    public interface IInvoiceRepository: IRepository<Invoice>
    {
        public Task<Invoice> GetInvoiceWithItems(int id);
    }
}
