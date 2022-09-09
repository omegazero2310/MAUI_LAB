using MAUI_LAB.Enums;

namespace MAUI_LAB.Entities.Request
{
    /// <summary>
    /// Mẫu request CRUD bảng Admin.Staff
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    public class AdminStaffRequest
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public GenderOptions Gender { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string ProfileImage { get; set; }

        public int PartID { get; set; }
    }
}
