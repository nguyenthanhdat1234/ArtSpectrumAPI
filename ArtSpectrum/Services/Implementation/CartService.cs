using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;

namespace ArtSpectrum.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;

        public CartService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CartDto> CreateCartAsync(CreateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await _uow.CartRepository.FirstOrDefaultAsync(x =>
                x.UserId == request.UserId, cancellationToken);

            if (cart is not null)
            {
                throw new Exception("This cart has already been taken.");
            }

            var cartEntity = new Cart()
            {
                UserId = request.UserId,
                PaintingId = request.PaintingId,
                Quantity = request.Quantity,
            };
            var result = await _uow.CartRepository.AddAsync(cartEntity);

            await _uow.Commit(cancellationToken);
            return _mapper.Map<CartDto>(result);
        }

        public async Task<CartDto> DeleteCartByIdAsync(int cartId, CancellationToken cancellationToken)
        {
            var cart = await _uow.CartRepository.FirstOrDefaultAsync(x => x.CartId == cartId, cancellationToken);
            if (cart is null)
            {
                throw new KeyNotFoundException("Cart not found.");
            }
            else
            {
                _uow.CartRepository.Delete(cart);
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<List<CartDto>> GetAll()
        {
            var result = await _uow.CartRepository.GetAll();
            return _mapper.Map<List<CartDto>>(result);
        }

        public async Task<CartDto> GetCartByIdAsync(int cartId, CancellationToken cancellationToken)
        {
            var result = await _uow.CartRepository.FirstOrDefaultAsync(x => x.CartId == cartId, cancellationToken);

            if (result is null)
            {
                throw new KeyNotFoundException("Cart not found.");
            }

            return _mapper.Map<CartDto>(result);
        }

        public async Task<CartDto> UpdateCartAsync(int cartId, UpdateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await _uow.CartRepository.FirstOrDefaultAsync(x => x.CartId == cartId, cancellationToken);

            if (cart is null)
            {
                throw new KeyNotFoundException("Cart is not found.");
            }

            cart.UserId = request.UserId;
            cart.PaintingId = request.PaintingId;
            cart.Quantity = request.Quantity;


            _uow.CartRepository.Update(cart);
            await _uow.Commit(cancellationToken);

            return _mapper.Map<CartDto>(cart);
        }
    }
}
