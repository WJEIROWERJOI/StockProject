namespace StockProject.Data.Services
{
    public class PointService
    {
        private readonly ApplicationDbContext _context;
        public PointService(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
