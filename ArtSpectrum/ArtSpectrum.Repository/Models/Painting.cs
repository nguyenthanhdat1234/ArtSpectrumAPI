using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class Painting
    {
        public Painting()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
            Categories = new HashSet<Category>();
        }

        public int PaintingId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public double? SalesPrice { get; set; }
        public int SaleId { get; set; }
        public int ArtistsId { get; set; }

        public virtual Sale Sale { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
