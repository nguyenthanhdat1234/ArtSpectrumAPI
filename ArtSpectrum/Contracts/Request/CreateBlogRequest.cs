namespace ArtSpectrum.Contracts.Request
{
    public class CreateBlogRequest
    {
        public string TiteBlog { get; set; } = null!;
        public string DescriptionBlog { get; set; } = null!;
        public string ImgBlog { get; set; } = null!;
        public int ArtistId { get; set; }
    }
}
