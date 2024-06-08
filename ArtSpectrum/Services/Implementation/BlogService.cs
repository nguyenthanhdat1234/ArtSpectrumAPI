using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;

namespace ArtSpectrum.Services.Implementation
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;

        public BlogService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<BlogDto> CreateBlogAsync(CreateBlogRequest request, CancellationToken cancellationToken)
        {

            var blogEntity = new Blog()
            {
                TiteBlog = request.TiteBlog,
                DescriptionBlog = request.DescriptionBlog,
                ImgBlog = request.ImgBlog,
                ArtistId = request.ArtistId
            };
            var result = await _uow.BlogRepository.AddAsync(blogEntity);

            await _uow.Commit(cancellationToken);
            return _mapper.Map<BlogDto>(result);
        }

        public async Task<BlogDto> DeleteBlogByIdAsync(int blogId, CancellationToken cancellationToken)
        {
            var blog = await _uow.BlogRepository.FirstOrDefaultAsync(x => x.BlogId == blogId, cancellationToken);
            if (blog is null)
            {
                throw new KeyNotFoundException("Blog not found.");
            }
            else
            {
                _uow.BlogRepository.Delete(blog);
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<BlogDto>(blog);
        }

        public async Task<List<BlogDto>> GetAll()
        {
            var result = await _uow.BlogRepository.GetAll();
            return _mapper.Map<List<BlogDto>>(result);
        }

        public async Task<BlogDto> GetBlogByIdAsync(int blogId, CancellationToken cancellationToken)
        {
            var result = await _uow.BlogRepository.FirstOrDefaultAsync(x => x.BlogId == blogId, cancellationToken);

            if (result is null)
            {
                throw new KeyNotFoundException("Blog not found.");
            }

            return _mapper.Map<BlogDto>(result);
        }

        public async Task<BlogDto> UpdateBlogAsync(int blogId, UpdateBlogRequest request, CancellationToken cancellationToken)
        {
            var blog = await _uow.BlogRepository.FirstOrDefaultAsync(x => x.BlogId == blogId, cancellationToken);

            if (blog is null)
            {
                throw new KeyNotFoundException("Blog is not found.");
            }

            blog.TiteBlog = request.TiteBlog;
            blog.DescriptionBlog = request.DescriptionBlog;
            blog.ImgBlog = request.ImgBlog;

            _uow.BlogRepository.Update(blog);
            await _uow.Commit(cancellationToken);

            return _mapper.Map<BlogDto>(blog);
        }
    }
}
