namespace MAUI_LAB.Constaints
{
    /// <summary>
    /// Thông báo lỗi trả về khi Login hoặc tạo tài khoản Admin.User
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    public static class UserLoginErrorCode
    {
        public const string EXIST = "MSG_USER_EXIST";
        public const string NOT_EXIST = "MSG_USER_LOGIN_NOT_EXIST";
        public const string SUPPENDED = "MSG_USER_LOGIN_SUPPENDED";
        public const string BANNED = "MSG_USER_LOGIN_BANNED";
        public const string WRONG_USER_NAME_PASSWORD = "MSG_USER_LOGIN_WRONG_USERNAME_OR_PASSWORD";
    }
}
