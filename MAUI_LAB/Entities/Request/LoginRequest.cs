using System.ComponentModel.DataAnnotations;

namespace MAUI_LAB.Entities.Request
{
    /// <summary>Mẫu Request đăng nhập</summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 17/08/2022 created
    /// </Modified>
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
