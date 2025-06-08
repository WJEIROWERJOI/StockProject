using Microsoft.EntityFrameworkCore;
using StockProject.Data.Entities;

namespace StockProject.Data.Repositories;

public class PointRepository
{
    private readonly ApplicationDbContext _context;
    public PointRepository(ApplicationDbContext context)
    {

        _context = context;
    }

    //c
    public async Task CreatePoint(Point point)
    {
        // Console.WriteLine("Creating point for user: " + point.UserId);
        _context.Points.Add(point);
        await _context.SaveChangesAsync();
    }
    //r
    public async Task<Point?> FindPointByIdAsync(int id)
    {
        return await _context.Points.FindAsync(id);
    }
    public async Task<Point?> FindPointByUserIdAsync(string userId)
    {
        return await _context.Points.FirstOrDefaultAsync(p => p.UserId == userId);
    }
    //u
    public async Task UpdatePoint(Point point)
    {
        _context.Points.Update(point);
        await _context.SaveChangesAsync();
    }
    //d
    public async Task DeletePoint(int id)
    {
        var point = await FindPointByIdAsync(id);
        if (point != null)
        {
            _context.Points.Remove(point);
            await _context.SaveChangesAsync();
        }
    }

}