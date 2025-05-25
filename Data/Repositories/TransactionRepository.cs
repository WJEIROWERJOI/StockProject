namespace StockProject.Data.Repositories
{
    public class TransactionRepository
    {
        private readonly ApplicationDbContext _context;
        
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
