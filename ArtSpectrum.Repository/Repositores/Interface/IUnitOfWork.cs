using ArtSpectrum.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtSpectrum.Repository.Repositores.Interface
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> UserRepository { get; }

        IBaseRepository<Artist> ArtistsRepository { get; }
        IBaseRepository<Painting> PaintingRepository { get; }
        IBaseRepository<Sale> SaleRepository { get; }
        IBaseRepository<Order> OrderRepository { get; }
        IBaseRepository<OrderDetail> OrderDetailRepository { get; }
        IBaseRepository<Cart> CartRepository { get; }
        IBaseRepository<Review> ReviewRepository { get; }
        IBaseRepository<Category> CategoryRepository { get; }
        IBaseRepository<PaintingCategory> PaintingCategoryRepository { get; }
        IBaseRepository<Blog> BlogRepository { get; }

        Task Commit(CancellationToken cancellationToken);
    }
}
