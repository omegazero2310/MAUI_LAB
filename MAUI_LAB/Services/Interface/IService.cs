using MAUI_LAB.Entities.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_LAB.Services.Interface
{
    /// <summary>
    /// Interface CRUD cơ bản
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    public interface IService<T> where T : class
    {
        Task<ServerRespone> CreateNew(T entity);
        Task<IEnumerable<T>> GetAll(int skip = 0, int take = 0, bool isForceRefresh = false);
        Task<T> GetByID(object key);
        Task<ServerRespone> Update(T entity);
        Task<ServerRespone> Delete(object key);
    }
}
