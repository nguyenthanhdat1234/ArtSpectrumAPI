using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;
using AutoMapper;

namespace ArtSpectrum.DTOs
{
    public class BlogDto : IMapFrom<Blog>
    {
        public int BlogId { get; set; }
        public string TiteBlog { get; set; } = null!;
        public string DescriptionBlog { get; set; } = null!;
        public string ImgBlog { get; set; } = null!;
        public int ArtistId { get; set; }
    }
}
