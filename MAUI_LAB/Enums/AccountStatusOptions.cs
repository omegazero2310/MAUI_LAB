namespace MAUI_LAB.Enums
{
    /// <summary>
    /// Trạng thái tài khoản Admin.User
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    public enum AccountStatusOptions
    {
        /// <summary>
        /// Tài khoản chưa kích hoạt
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 31/08/2022 created
        /// </Modified>
        Inactive,
        /// <summary>
        /// bình thường, tài khoản không bị hạn chế
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        Normal,
        /// <summary>
        /// tài khoản bị tạm khóa, không thể đăng nhập tạm thời
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        Suppended,
        /// <summary>
        /// tài khoản bị cấm, không thể đăng nhập vĩnh viễn
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        Banned,
    }
}
