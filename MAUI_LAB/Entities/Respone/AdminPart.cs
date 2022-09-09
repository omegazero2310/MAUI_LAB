namespace MAUI_LAB.Entities.Respone
{
    public class AdminPart
    {
        /// <summary>
        /// Mã chức danh
        /// </summary>
        /// <value>
        /// The part identifier.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public int PartID { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        /// <value>
        /// The name of the part.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        public string PartName { get; set; }

        /// <summary>
        /// Ngày giờ tạo
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
        /// Ngày giờ chỉnh sửa
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
        public bool IsActive { get; set; }
    }
}
