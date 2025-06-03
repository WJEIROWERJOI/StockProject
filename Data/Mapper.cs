using StockProject.Data.Dtos;
using StockProject.Data.Entities;

namespace StockProject.Data
{
    public static class Mapper
    {
        public static UserEntity UserRegisterDtoToEntity(UserRegisterDto dto)
        {
            return new UserEntity()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                LastLoginAt = null,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };
        }
        public static StockDto StockEntityToDto(StockEntity entity)
        {
            return new StockDto()
            {
                Id = entity.Id,
                Category = entity.Category?.Name ?? string.Empty,
                ProductName = entity.ProductName,
                Description = entity.Description ?? string.Empty,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
                LastUpdatedAt = entity.LastUpdatedAt
            };
        }
        public static StockEntity StockDtoToEntity(StockDto dto, StockCategory category)
        {
            return new StockEntity()
            {
                // Id = dto.Id ?? Guid.NewGuid().ToString(),
                Category = category,
                ProductName = dto.ProductName,
                Description = dto.Description ?? string.Empty,
                Quantity = dto.Quantity,
                CreatedAt = dto.CreatedAt,
                LastUpdatedAt = dto.LastUpdatedAt
            };
        }

    }
}
