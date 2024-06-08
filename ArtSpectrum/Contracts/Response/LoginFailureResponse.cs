namespace ArtSpectrum.Contracts.Response
{
    public class LoginFailureResponse
    {
        bool Failure => false;
        public string[] Errors { get; set; } = Array.Empty<string>();


    }
}
