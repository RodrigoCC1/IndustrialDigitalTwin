using DigitalTwin.Domain.Entities;
using DigitalTwin.Domain.Interfaces;
using DigitalTwin.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTwin.Infrastructure.Repositories
{
    public class MotorRepository : IMotorRepository
    {
        private readonly AppDbContext _context;

        public MotorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddMotorAsync(Motor motor)
        {
            await _context.Motors.AddAsync(motor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMotorAsync(Guid id)
        {
            var motor = await GetByIdAsync(id);
            if (motor != null)
            {
                _context.Motors.Remove(motor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Motor>> GetAllAsync()
        {
            return await _context.Motors.ToListAsync();
        }

        public async Task<Motor?> GetByIdAsync(Guid id)
        {
            return await _context.Motors.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateMotorAsync(Motor motor)
        {
            _context.Motors.Update(motor);
            await _context.SaveChangesAsync();
        }
    }
}
