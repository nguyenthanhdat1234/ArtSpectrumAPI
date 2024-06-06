using ArtSpectrum.Contracts.Request;
using ArtSpectrum.Contracts.Response;
using ArtSpectrum.DTOs;
using ArtSpectrum.Exceptions;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using ArtSpectrum.Utils;
using System.Text.Json;
using System.Text;

namespace ArtSpectrum.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public PaymentService(IUnitOfWork uow, IConfiguration configuration, HttpClient httpClient)
        {
            _uow = uow;
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<CheckoutDto> CreatePaymentLinkAsync(int orderId, CancellationToken cancellationToken)
        {

            var order = await _uow.OrderRepository.FirstOrDefaultAsync(o => o.OrderId == orderId, cancellationToken);

            int totalAmount = 0;
            // CHECK if order exists
            if (order == null)
            {
                throw new ConflictException("Order not found!");
            }

            // CHECK if user exists
            var user = await _uow.UserRepository.FirstOrDefaultAsync(u => u.UserId == order.UserId, cancellationToken);

            if (user == null)
            {
                throw new ConflictException("User not found!");
            }

            // GET order details by OrderId
            var orderDetails = await _uow.OrderDetailRepository.WhereAsync(od => od.OrderId == order.OrderId, cancellationToken);

            if (orderDetails == null || !orderDetails.Any())
            {
                throw new ConflictException("Order Details not found!");
            }

            // CALCULATE the total amount of the order
            

            foreach (var orderDetail in orderDetails)
            {
                var painting = await _uow.PaintingRepository.FirstOrDefaultAsync(p => p.PaintingId == orderDetail.PaintingId, cancellationToken);

                if (painting == null)
                {
                    throw new ConflictException($"Painting with ID {orderDetail.PaintingId} not found!");
                }

                int paintingAmount = painting.Price - (painting.SalesPrice ?? 0);
                totalAmount += paintingAmount * orderDetail.Quantity;
            }

            var currentTime = DateTimeOffset.UtcNow;
            var expirationTime = currentTime.AddHours(1);

            var data = new CheckoutDto()
            {
                orderCode = orderId,
                amount = totalAmount,
                description = "ArtSpectrum Payment",
                buyerName = user.FullName?.Trim() ?? string.Empty,
                buyerEmail = user.Email?.Trim() ?? string.Empty,
                buyerPhone = user.PhoneNumber?.Trim() ?? string.Empty,
                buyerAddress = user.Address?.Trim() ?? string.Empty,
                cancelUrl = $"https://localhost:7219/api/v1/order/{orderId}",
                returnUrl = $"https://localhost:7219/api/v1/order/{orderId}",
                expiredAt = (int)expirationTime.ToUnixTimeSeconds(), // Set the expiration time to be in the future
                signature = "string",
                items = new List<PaintingRequest>()
            };

            foreach(var item in orderDetails)
            {
                var painting = await _uow.PaintingRepository.FirstOrDefaultAsync(p => p.PaintingId == item.PaintingId, cancellationToken);

                if (painting == null)
                {
                    throw new ConflictException($"Painting with ID {item.PaintingId} not found!");
                }

                data.items.Add( new PaintingRequest{ name = painting.Title, price = painting.Price, quantity = item.Quantity});  
            }

            
            return data;
        }


        public async Task<ResponseCheckoutData> GetInfoLinkPayment(int orderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
