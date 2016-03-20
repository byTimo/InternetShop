using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models
{
    public class IdentityUserInfo
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool IsAuthorized { get; set; }
        public string Name { get; set; }

        public static IdentityUserInfo Create(User user)
        {
            if (user == null)
                return new IdentityUserInfo();
            else
                return new IdentityUserInfo
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Name = user.Name,
                    IsAuthorized = true
                };
        }
    }
}