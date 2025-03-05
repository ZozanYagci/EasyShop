using DTOs.DTOs.UserDtos;

namespace EasyShop.UI.Models
{
    public class AccountViewModel
    {
        public UserForLoginDto Login { get; set; }
        public UserForRegisterDto Register { get; set; }
    }
}
