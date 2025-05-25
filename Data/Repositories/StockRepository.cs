namespace StockProject.Data.Repositories
{
    public class StockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }



    }
}
