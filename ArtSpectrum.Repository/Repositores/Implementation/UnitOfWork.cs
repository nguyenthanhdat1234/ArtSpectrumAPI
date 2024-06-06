﻿using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtSpectrum.Repository.Repositores.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArtSpectrumDBContext _context;

        internal ArtSpectrumDBContext Context => _context;

        private IBaseRepository<User>? _userRepository;
        private IBaseRepository<Artist>? _artistRepository;
        private IBaseRepository<Painting>? _paintingRepository;
        private IBaseRepository<Sale>? _saleRepository;
        private IBaseRepository<Order>? _orderRepository;
        private IBaseRepository<OrderDetail>? _orderDetailRepository;
        private IBaseRepository<Cart>? _cartRepository;
        private IBaseRepository<Review>? _reviewRepository;
        private IBaseRepository<Category>? _categoryRepository;
        private IBaseRepository<PaintingCategory>? _paintingCategoryRepository;

        public UnitOfWork(ArtSpectrumDBContext context)
        {
            _context = context;
        }

        public IBaseRepository<User> UserRepository => _userRepository ??= new UserRepository(_context);

        public IBaseRepository<Artist> ArtistsRepository => _artistRepository ??= new ArtistRepository(_context);
        public IBaseRepository<Painting> PaintingRepository => _paintingRepository ??= new PaintingRepository(_context);
        public IBaseRepository<Sale> SaleRepository => _saleRepository ??= new SaleRepository(_context);
        public IBaseRepository<Order> OrderRepository => _orderRepository ??= new OrderRepository(_context);

        public IBaseRepository<OrderDetail> OrderDetailRepository => _orderDetailRepository ??= new OrderDetailRepository(_context);

        public IBaseRepository<Cart> CartRepository => _cartRepository ??= new CartRepository(_context);
        public IBaseRepository<Review> ReviewRepository => _reviewRepository ??= new ReviewRepository(_context);
        public IBaseRepository<Category> CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
        public IBaseRepository<PaintingCategory> PaintingCategoryRepository => _paintingCategoryRepository ??= new PaintingCategoryRepository(_context);



        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
