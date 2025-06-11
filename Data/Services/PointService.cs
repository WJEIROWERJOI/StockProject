using StockProject.Data.Entities;
using StockProject.Data.Repositories;
using System.Data.SqlTypes;

namespace StockProject.Data.Services
{
    public class PointService
    {
        private readonly PointRepository _pointRepository;
        private readonly ICurrentUserService _currentUserService;
        public PointService(PointRepository pointRepository, ICurrentUserService currentUserService)
        {
            _pointRepository = pointRepository;
            _currentUserService = currentUserService;
        }

        public event Action? OnPointUpdated;
        public void CallPoint()
        {
            OnPointUpdated?.Invoke();
        }
        //c
        public async Task CreatePoint(Point point)
        {
            await _pointRepository.CreatePoint(point);
        }






        public async Task AddPointsAsync(int bet, Point point)
        {
            // point.Money += 5;
            point.UpdatedAt = DateTime.UtcNow;
            if (_currentUserService.IsSignedIn())
            {
                await _pointRepository.UpdatePoint(point);
            }

        }
        public async Task SubtractPointsAsync(int bet, Point point)
        {
            point.Money -= 50;
            point.UpdatedAt = DateTime.UtcNow;
            if (_currentUserService.IsSignedIn())
            {
                await _pointRepository.UpdatePoint(point);
            }
        }
        // public async Task MultiplePointsAsync(int bet, Point point)
        // {
        //     point.Money *= bet;
        //     point.UpdatedAt = DateTime.UtcNow;
        //     await _pointRepository.UpdatePoint(point);
        //     // OnPointUpdated?.Invoke();
        // }
        // public async Task DividePointsAsync(int bet, Point point)
        // {
        //     point.Money /= bet;
        //     point.UpdatedAt = DateTime.UtcNow;
        //     await _pointRepository.UpdatePoint(point);
        //     // OnPointUpdated?.Invoke();
        // }


        //r

        public async Task<Point?> GetPointByIdAsync(int id)
        {
            return await _pointRepository.FindPointByIdAsync(id);
        }
        public async Task<Point?> GetPointByUserIdAsync(string userId)
        {
            return await _pointRepository.FindPointByUserIdAsync(userId);
        }


        public async Task<Point?> ResetPoint()
        {
            if (_currentUserService.IsSignedIn())
            {
                Point? point = await _pointRepository.FindPointByUserIdAsync(_currentUserService.UserId!);
                point!.UpdatedAt = DateTime.UtcNow;
                point.Money = 100;
                await _pointRepository.UpdatePoint(point);
                return point;
            }
            else
            {
                return null;
            }
        }



        public async Task<Point?> SetCurrentUser()
        {
            var currentPoint = await _pointRepository.FindPointByUserIdAsync(_currentUserService.UserId!);
            return currentPoint;
        }


        public async Task<Point?> InitialCreate()
        {
            // var currentPoint = await _pointRepository.FindPointByUserIdAsync(_currentUserService.UserId!);
            // if (currentPoint != null)
            // {
            //     await _pointRepository.DeletePoint(currentPoint!.Id);
            // }
            var newPoint = new Point
            {
                UserId = _currentUserService.UserId!,
                UserName = _currentUserService.UserName ?? "Temp",
                Money = 100,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _pointRepository.CreatePoint(newPoint);

            return newPoint;
        }








        //u
        public async Task UpdatePoint(Point point)
        {
            await _pointRepository.UpdatePoint(point);
        }
        //d
        public async Task DeletePoint(int id)
        {
            await _pointRepository.DeletePoint(id);
        }

    }
}
