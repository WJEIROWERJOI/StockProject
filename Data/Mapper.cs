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
                Category = entity.Category?.Name ?? string.Empty,
                ProductName = entity.ProductName,
                Description = entity.Description ?? string.Empty,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
                LastUpdatedAt = entity.LastUpdatedAt
            };
        }
        public static StockEntity StockDtoToEntity(StockDto dto)
        {
            return new StockEntity()
            {
                Category = null, // Assuming category is set separately
                ProductName = dto.ProductName,
                Description = dto.Description,
                Quantity = dto.Quantity,
                CreatedAt = dto.CreatedAt,
                LastUpdatedAt = dto.LastUpdatedAt
            };
        }

    }
}
