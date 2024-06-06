using ArtSpectrum.Contracts.Response;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface IPaymentService
    {
        public Task<CheckoutDto> CreatePaymentLinkAsync(int orderId, CancellationToken cancellationToken);

        public Task<ResponseCheckoutData> GetInfoLinkPayment(int orderId, CancellationToken cancellationToken);
    }
}
