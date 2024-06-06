using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;

namespace ArtSpectrum.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public ReviewService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewRequest request, CancellationToken cancellationToken)
        {
            var reviewEntity = new Review()
            {
                UserId = request.UserId,
                PaintingId = request.PaintingId,
                Rating = request.Rating,
                Comment = request.Comment,
                ReviewDate = request.ReviewDate
            };
            var result = await _uow.ReviewRepository.AddAsync(reviewEntity);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<ReviewDto>(result);

        }

        public async Task<ReviewDto> DeleteReviewAsync(int reviewId, CancellationToken cancellationToken)
        {
            var review = await _uow.ReviewRepository.FirstOrDefaultAsync(x => x.ReviewId == reviewId, cancellationToken);
            if (review is null)
            {
                throw new KeyNotFoundException("Review not found. ");
            }
            else
            {
                _uow.ReviewRepository.Delete(review);
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<ReviewDto>(review);

        }

        public async Task<List<ReviewDto>> GetAll()
        {
            var result = await _uow.ReviewRepository.GetAll();
            return _mapper.Map<List<ReviewDto>>(result);
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int reviewId, CancellationToken cancellationToken)
        {
            var result = await _uow.ReviewRepository.FirstOrDefaultAsync(x => x.ReviewId == reviewId, cancellationToken).ConfigureAwait(false);
            if (result is null)
            {
                throw new KeyNotFoundException("Review not found. ");
            }
            return _mapper.Map<ReviewDto>(result);
        }

        public async Task<ReviewDto> UpdateReviewAsync(int reviewId, UpdateReviewRequest request, CancellationToken cancellationToken)
        {
            var review = await _uow.ReviewRepository.FirstOrDefaultAsync(x => x.ReviewId == reviewId, cancellationToken);
            if (review is null)
            {
                throw new KeyNotFoundException("Review not found. ");
            }
            review.Rating = request.Rating;
            review.Comment = request.Comment;
            review.ReviewDate = request.ReviewDate;

            _uow.ReviewRepository.Update(review);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<ReviewDto>(review);
        }
    }
}
