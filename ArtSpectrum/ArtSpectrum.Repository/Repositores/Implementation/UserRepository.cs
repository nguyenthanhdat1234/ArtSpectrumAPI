using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;

namespace ArtSpectrum.Repository.Repositores.Implementation
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ArtSpectrumDBContext context) : base(context)
        {
        }

    }
}