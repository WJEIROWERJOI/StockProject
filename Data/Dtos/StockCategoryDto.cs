namespace StockProject.Data.Dtos
{
    public record StockCategoryDto
    {
        public required string Name { get; init; }
        public string Description { get; init; } = string.Empty;
    }
}
