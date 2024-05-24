namespace ArtSpectrum.Contracts.Request
{
    public class CreateArtistRequest
    {
        public int UserId { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
