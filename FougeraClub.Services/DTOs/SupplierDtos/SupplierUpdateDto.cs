using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.SupplierDtos
{
    public class SupplierUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string VATNumber { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
