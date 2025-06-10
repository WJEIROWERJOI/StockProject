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

        //public async Task CalculatePointsAsync(char c,int bet, Point point)
        //{
        //    if (c == '+')
        //    {
        //        point.Money += bet;
        //    }
        //    else if (c == '-')
        //    {
        //        point.Money -= bet;
        //    }
        //    else if (c == "multiple")
        //    {
        //        point.Money *= bet;
        //    }
        //    else if (c == "divide")
        //    {
        //        if (bet != 0)
        //        {
        //            point.Money /= bet;
        //        }
        //        else
        //        {
        //            throw new DivideByZeroException("Cannot divide by zero.");
        //        }
        //    }
        //    point.UpdatedAt = DateTime.UtcNow;
        //    await _pointRepository.UpdatePoint(point);
        //}

        //
        public async Task AddPointsAsync(int bet, Point point)
        {
            point.Money += 100;
            point.UpdatedAt = DateTime.UtcNow;
            if (_currentUserService.IsSignedIn())
            {
                Point? newpoint = await _pointRepository.FindPointByIdAsync(point.Id);
                newpoint!.Money = point.Money;
                newpoint.UpdatedAt = point.UpdatedAt;
                await _pointRepository.UpdatePoint(newpoint);
            }

        }
        public async Task SubtractPointsAsync(int bet, Point point)
        {
            point.Money -= 200;
            point.UpdatedAt = DateTime.UtcNow;
            if (_currentUserService.IsSignedIn())
            {
                Point? newpoint = await _pointRepository.FindPointByIdAsync(point.Id);
                newpoint!.Money = point.Money;
                newpoint.UpdatedAt = point.UpdatedAt;
                await _pointRepository.UpdatePoint(newpoint);
            }
        }
        public async Task MultiplePointsAsync(int bet, Point point)
        {
            point.Money *= bet;
            point.UpdatedAt = DateTime.UtcNow;
            await _pointRepository.UpdatePoint(point);
            // OnPointUpdated?.Invoke();
        }
        public async Task DividePointsAsync(int bet, Point point)
        {
            point.Money /= bet;
            point.UpdatedAt = DateTime.UtcNow;
            await _pointRepository.UpdatePoint(point);
            // OnPointUpdated?.Invoke();
        }


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
            Point? newPoint = await _pointRepository.FindPointByUserIdAsync(_currentUserService.UserId!);
            newPoint!.UpdatedAt = DateTime.UtcNow;
            newPoint.Money = 100;
            await _pointRepository.UpdatePoint(newPoint);
            return newPoint;
        }



        public async Task<Point?> SetCurrentUser()
        {
            var currentPoint = await _pointRepository.FindPointByUserIdAsync(_currentUserService.UserId!);
            //비로그인
            if (_currentUserService == null || !_currentUserService.IsSignedIn())
            {
                return null;
            }
            //로그인인
            else
            {
                return currentPoint;
            }
        }
        public async Task<Point?> InitialCreate()
        {
            var currentPoint = await _pointRepository.FindPointByUserIdAsync(_currentUserService.UserId!);
            if (currentPoint != null)
            {
                await _pointRepository.DeletePoint(currentPoint!.Id);
            }
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
