using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.DTOs.PurchaseItemsDtos
{
    public class PurchaseItemCreateDto
    {
        public int Amount { get; set; }
        public double PricePerUnit { get; set; }
        public string Description { get; set; }
    }
}
