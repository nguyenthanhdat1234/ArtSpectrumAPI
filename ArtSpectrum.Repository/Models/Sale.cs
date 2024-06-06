using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class Sale
    {
        public Sale()
        {
            Paintings = new HashSet<Painting>();
        }

        public int SaleId { get; set; }
        public DateTime? StartTimeSales { get; set; }
        public DateTime? EndTimeSales { get; set; }
        public string? VoucherName { get; set; }
        public int VoucherDiscount { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}
