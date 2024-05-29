using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtSpectrum.Repository.Repositores.Implementation
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>
    {
        public OrderDetailRepository(ArtSpectrumDBContext context) : base(context)
        {
        }
    }
}
