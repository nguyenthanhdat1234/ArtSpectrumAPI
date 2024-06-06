﻿using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArtSpectrum.Repository.Repositores.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private protected readonly ArtSpectrumDBContext _context;

        protected BaseRepository(ArtSpectrumDBContext context)
        {
            _context = context;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<T> AddAsync(T obj, CancellationToken cancellationToken)
        {
            return (await _context.Set<T>().AddAsync(obj, cancellationToken)).Entity;
        }

        public virtual T Update(T obj)
        {
            return _context.Set<T>().Update(obj).Entity;
        }

        public virtual T Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            return obj;
        }

        public virtual void RemoveRange(params T[] entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

    }
}
