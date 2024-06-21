using ArtSpectrum.Contracts.Request;
using ArtSpectrum.Contracts.Response;
using ArtSpectrum.DTOs;
using ArtSpectrum.Exceptions;
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

        public async Task<List<ResponseCart>> CreateCartAsync(CreateCartRequest request, CancellationToken cancellationToken)
        {
            
            var existingCarts = await _uow.CartRepository
                .WhereAsync(x => x.UserId == request.UserId && request.PaintingQuantity.Select(pq => pq.PaintingId).Contains(x.PaintingId), cancellationToken);

            if (existingCarts.Any())
            {
                throw new ConflictException("Some paintings are already in the cart for this user.");
            }

            var cartEntities = new List<Cart>();

            // Thêm các painting vào cart
            foreach (var item in request.PaintingQuantity)
            {
                var painting = await _uow.PaintingRepository.FirstOrDefaultAsync(x => x.PaintingId == item.PaintingId, cancellationToken);

                if (painting == null)
                {
                    throw new ConflictException($"Painting with ID {item.PaintingId} not found.");
                }

                var cartEntity = new Cart
                {
                    UserId = request.UserId,
                    PaintingId = item.PaintingId,
                    Quantity = item.Quantity
                };

                cartEntities.Add(cartEntity);
                await _uow.CartRepository.AddAsync(cartEntity, cancellationToken);
            }

            await _uow.Commit(cancellationToken);

            var createdCarts = await _uow.CartRepository
                .WhereAsync(x => x.UserId == request.UserId, cancellationToken);

            List<ResponseCart> list = new List<ResponseCart>();
            foreach (var cart in createdCarts)
            {
                var painting = await _uow.PaintingRepository.FirstOrDefaultAsync(x => x.PaintingId == cart.PaintingId);

                var cartItem = new ResponseCart()
                {
                    UserId = cart.UserId,
                    Title = painting.Title,
                    Description = painting.Description,
                    ImageUrl = painting.ImageUrl,
                    Price = painting.Price,
                    SalesPrice = painting.SalesPrice,
                };

                cartItem.PaintingQuantity.Add(new PaintingQuantity { PaintingId = cart.PaintingId, Quantity = cart.Quantity });
                list.Add(cartItem);
            }

            return _mapper.Map<List<ResponseCart>>(list);
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

        public async Task<List<ResponseCart>> GetCartByIdAsync(int userId, CancellationToken cancellationToken)
        {
            List<ResponseCart> listResult = new List<ResponseCart>();
            var cartList = await _uow.CartRepository.WhereAsync(x => x.UserId == userId, cancellationToken);

            if (cartList == null || !cartList.Any())
            {
                throw new KeyNotFoundException("Cart not found.");
            }

            foreach (var item in cartList)
            {
                var painting = await _uow.PaintingRepository.FirstOrDefaultAsync(x => x.PaintingId == item.PaintingId);

                if (painting == null)
                {
                    throw new ConflictException("Painting not found!");
                }

                var responseCart = new ResponseCart
                {
                    CartId = item.CartId,
                    UserId = item.UserId,
                    Title = painting.Title,
                    Description = painting.Description,
                    ImageUrl = painting.ImageUrl,
                    Price = painting.Price,
                    SalesPrice = painting.SalesPrice,
                    PaintingQuantity = new List<PaintingQuantity>
            {
                new PaintingQuantity
                {
                    PaintingId = item.PaintingId,
                    Quantity = item.Quantity,
                }
            }
                };

                listResult.Add(responseCart);
            }

            return _mapper.Map<List<ResponseCart>>(listResult);
        }


        public async Task<CartDto> UpdateCartAsync(int cartId, UpdateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await _uow.CartRepository.FirstOrDefaultAsync(x => x.CartId == cartId, cancellationToken);
            var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
            if (cart is null)
            {
                throw new KeyNotFoundException("Cart is not found.");
            }
            if (user is null)
            {
                throw new KeyNotFoundException("User is not found.");
            }

            cart.PaintingId = request.PaintingId;
            cart.Quantity = request.Quantity;


            _uow.CartRepository.Update(cart);
            await _uow.Commit(cancellationToken);

            return _mapper.Map<CartDto>(cart);
        }
    }
}
