using Microsoft.EntityFrameworkCore;
using StockProject.Data.Entities;

namespace StockProject.Data.Repositories
{
    public class BoardRepository
    {
        private readonly ApplicationDbContext _context;

        public BoardRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        //c
        public async Task CreateBoard(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
        }
        //r
        public async Task<Board?> FindBoardByIdAsync(int id)
        {
           return await _context.Boards.FindAsync(id);
        }
        public async Task<List<Board>> FindAllBoardAsync()
        {
            return await _context
                .Boards
                .Include(x=>x.Boards)
                .ToListAsync();
        }
        //u

        //d


    }
}
