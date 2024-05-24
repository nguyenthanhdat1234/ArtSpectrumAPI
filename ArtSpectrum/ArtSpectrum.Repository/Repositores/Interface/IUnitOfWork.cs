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
        Task Commit(CancellationToken cancellationToken);
    }
}
