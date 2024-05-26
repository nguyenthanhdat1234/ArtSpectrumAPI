using ArtSpectrum.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtSpectrum.Repository.Repositores.Implementation
{
    public class SaleRepository : BaseRepository<Sale>
    {
        public SaleRepository(ArtSpectrumDBContext context) : base(context) { }
    }
}
