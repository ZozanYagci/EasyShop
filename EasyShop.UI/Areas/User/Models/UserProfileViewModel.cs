using DTOs.DTOs.UserDtos;

namespace EasyShop.UI.Areas.User.Models
{
    public class UserProfileViewModel
    {
        public UserProfileUpdateDto ProfileUpdate { get; set; }
        public ChangePasswordDto ChangePassword { get; set; }
    }
}
