using MAUI_LAB.Enums;

namespace MAUI_LAB.Entities.Respone
{
    public class AdminUser
    {
        /// <summary>
        /// GUID của tài khoản
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public Guid Id { get; set; }
        /// <summary>
        /// ID đăng nhập
        /// </summary>
        /// <value>
        /// ID đăng nhập được cấp
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu (đã được qua hàm băm)
        /// </summary>
        /// <value>
        /// 
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public string HashedPassword { get; set; }

        /// <summary>
        /// Salt của mật khẩu đã qua hàm băm
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public string Salt { get; set; }

        /// <summary>
        /// đặt giá trị tài khoản cần đổi lại mật khẩu
        /// </summary>
        /// <value>
        ///   <c>true</c> nếu tài khoản cần đổi mật khẩu; còn lại, <c>false</c>.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public bool IsResetPassword { get; set; }

        /// <summary>
        /// ngày tạo tài khoản
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Lần cuối đổi mật khẩu của nhân viên
        /// </summary>
        /// <value>
        /// 
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        /// <summary>
        /// người chỉnh sửa tài khoản
        /// </summary>
        /// <value>
        /// The user modified.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public string UserModified { get; set; }

        /// <summary>
        /// trạng thái của tài khoản
        /// </summary>
        /// <value>
        /// The account status.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public AccountStatusOptions AccountStatus { get; set; }
        public string ProfilePictureName { get; set; }
        /// <summary>
        /// Số điện thoại đăng kí với tài khoản
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Tên ảnh đại diện
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public string DisplayName { get; set; }

        /// <summary>
        /// Đánh dấu ẩn hiện dòng
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 05/09/2022 created
        /// </Modified>
        public bool IsActive { get; set; }

        public byte[] ProfileImg { get; set; }
    }
}
