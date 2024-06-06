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
            PaintingCategories = new HashSet<PaintingCategory>();
            Reviews = new HashSet<Review>();
        }

        public int PaintingId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public int SaleId { get; set; }
        public int ArtistsId { get; set; }
        public int Price { get; set; }
        public int? SalesPrice { get; set; }

        public virtual Sale Sale { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<PaintingCategory> PaintingCategories { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
