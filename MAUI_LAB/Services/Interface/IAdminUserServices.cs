using MAUI_LAB.Entities.Respone;

namespace MAUI_LAB.Services.Interface
{
    public interface IAdminUserServices : IService<AdminUser>
    {
        Task<AdminUser> GetByID(object key, string token);
        Task<ServerRespone> GetUserPicture(string userName, string token);
        Task<(bool, string)> Login(string userName, string password);
        Task<ServerRespone> UploadUserPicture(string userName, string filePath, string token);
    }
}
