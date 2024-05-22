using System;
using System.Collections.Generic;

namespace ArtSpectrum.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public int UserId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
