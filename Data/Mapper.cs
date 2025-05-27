using StockProject.Data.Dtos;
using StockProject.Data.Entities;

namespace StockProject.Data
{
    public static class Mapper
    {
        public static StockCategory StockCategoryDtoToEntity(StockCategoryDto dto)
        {
            return new StockCategory()
            {
                Name = dto.Name,
                Description = dto.Description ?? string.Empty
            };
        }
        public static StockCategoryDto StockCategoryEntityToDto(StockCategory Entity)
        {
            return new StockCategoryDto()
            {
                Name = Entity.Name,
                Description = Entity.Description ?? string.Empty
            };
        }


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

    }
}
