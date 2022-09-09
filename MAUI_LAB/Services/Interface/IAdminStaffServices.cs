using MAUI_LAB.Entities.Respone;

namespace MAUI_LAB.Services.Interface
{
    public interface IAdminStaffServices : IService<AdminStaff>
    {
        Task<ServerRespone> GetProfilePicture(int id);
        Task<ServerRespone> UploadProfilePicture(int id, string filePath);
    }
}
