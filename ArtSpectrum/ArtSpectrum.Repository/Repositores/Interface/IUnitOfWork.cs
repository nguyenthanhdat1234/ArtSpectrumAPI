using ArtSpectrum.Models;
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
        Task Commit(CancellationToken cancellationToken);
    }
}
