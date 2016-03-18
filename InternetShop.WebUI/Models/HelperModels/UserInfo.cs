using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool IsAuthorized { get; set; }
        public string Name { get; set; }

        public static UserInfo Create(User user)
        {
            if (user == null)
                return new UserInfo();
            else
                return new UserInfo
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Name = user.Name,
                    IsAuthorized = true
                };
        }
    }
}