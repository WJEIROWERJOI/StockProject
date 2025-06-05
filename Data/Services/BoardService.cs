using StockProject.Data.Entities;
using StockProject.Data.Repositories;

namespace StockProject.Data.Services
{
    public class BoardService
    {
        private readonly BoardRepository _repository;
        public BoardService(BoardRepository repository) { _repository = repository; }

        //c




        //r
        public Task<List<Board>> GetAllBoardAsync()
        {
            return _repository.FindAllBoardAsync();
        }

        //u


        //d



    }
}
