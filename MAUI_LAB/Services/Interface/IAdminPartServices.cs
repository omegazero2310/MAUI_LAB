using MAUI_LAB.Entities.Respone;

namespace MAUI_LAB.Services.Interface
{
    public interface IAdminPartServices : IService<AdminPart>
    {
        Task<IReadOnlyDictionary<int, string>> GetAllAsDictionary();
    }
}
