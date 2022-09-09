using MAUI_LAB.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_LAB.Entities.Respone
{
    public class AdminStaff
    {
        /// <summary>
        /// ID đăng nhập hệ thống
        /// </summary>
        /// <value>
        /// ID đăng nhập
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public int StaffID { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// <value>
        /// tên của nhân viên
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public string StaffName { get; set; }

        /// <summary>
        /// Giới tính của nhân viên
        /// </summary>
        /// 0 - Nam, 1 - Nữ, 2 - Khác
        /// The gender.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public GenderOptions Gender { get; set; }

        /// <summary>
        /// Email của nhân viên
        /// </summary>
        /// <value>
        /// địa chỉ Email hệ thống
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại của nhân viên
        /// </summary>
        /// <value>
        /// số điện thoại hợp lệ
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ của nhân viên
        /// </summary>
        /// <value>
        /// địa chỉ thường chú
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public string Address { get; set; }


        /// <summary>
        /// ID hình ảnh của nhân viên trên API
        /// </summary>
        /// <value>
        /// ảnh đại diện của nhân viên
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Chức danh của nhân viên
        /// </summary>
        /// <value>
        /// Chức danh hiện tại
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public int PartID { get; set; }

        /// <summary>
        /// ngày tạo
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
        /// Ngày chỉnh sửa
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public DateTime DateModified { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        /// <value>
        /// The user created.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public string UserCreated { get; set; }
        /// <summary>
        /// Người chỉnh sửa
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

        /// <summary>
        /// dữ liệu hình ảnh của nhân viên ( không map với database)
        /// </summary>
        /// <value>
        /// The profile picture.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public byte[] ProfilePicture { get; set; }
        /// <summary>
        /// Tên chức danh của nhân viên (không map với database)
        /// </summary>
        /// <value>
        /// The name of the position.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public string PositionName { get; set; }
    }
}
