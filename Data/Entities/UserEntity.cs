using Microsoft.AspNetCore.Identity;

namespace StockProject.Data.Entities;

    public class UserEntity : IdentityUser
    {
        public DateTime? LastLoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
