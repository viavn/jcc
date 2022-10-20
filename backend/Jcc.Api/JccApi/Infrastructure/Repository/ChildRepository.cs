﻿using JccApi.Entities;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class ChildRepository : IChildRepository
    {
        private readonly DataBaseContext _context;

        public ChildRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task Create(Child child)
        {
            _context.Children.Add(child);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Child child)
        {
            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Child>> GetAll()
        {
            return await _context.Children.AsNoTracking().ToListAsync();
        }

        public async Task<Child> GetById(Guid id)
        {
            return await _context.Children.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Child child)
        {
            _context.Children.Update(child);
            await _context.SaveChangesAsync();
        }
    }
}
