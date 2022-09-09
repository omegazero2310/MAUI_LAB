using MAUI_LAB.Constaints;
using MAUI_LAB.Entities.Respone;
using MAUI_LAB.Services.Interface;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace MAUI_LAB.Services
{
    /// <summary>
    /// Service tương tác với API bảng Admin.Part
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="MobileAppLab.ApiServices.BaseApiService" />
    /// <seealso cref="MobileAppLab.ApiServices.IService&lt;CommonClass.Models.AdminParts&gt;" />
    public class AdminPartServices : BaseApiService, IAdminPartServices
    {
        public AdminPartServices(HttpClient httpClient) : base(httpClient, ApiConstaints.AdminPart, true)
        {
        }
        /// <summary>
        /// [Chưa tích hợp] tạo mới chức danh
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ServerRespone> CreateNew(AdminPart entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// [Chưa tích hợp] xóa chức danh
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ServerRespone> Delete(object key)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lấy ra danh sách chức danh
        /// </summary>
        /// <param name="skip">bỏ qua bao nhiêu dòng</param>
        /// <param name="take">Lấy bao nhiêu dòng</param>
        /// <param name="isforceRefresh">nếu đặt là <c>true</c> dữ liệu sẽ được làm mới hoàn toàn.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public async Task<IEnumerable<AdminPart>> GetAll(int skip = 0, int take = 0, bool isforceRefresh = false)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: BaseUrl + "/Get") || isforceRefresh == false)
                {
                    return Barrel.Current.Get<IEnumerable<AdminPart>>(key: BaseUrl + "/Get");
                }
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token);
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                ServerRespone serverRespone = JsonConvert.DeserializeObject<ServerRespone>(respone.Content.ReadAsStringAsync().Result);
                var listStaff = JsonConvert.DeserializeObject<IEnumerable<AdminPart>>(serverRespone.Result.ToString());
                Barrel.Current.Add(key: BaseUrl + "/Get", data: listStaff, expireIn: TimeSpan.FromDays(1));
                return listStaff;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<AdminPart>();
            }
        }
        /// <summary>
        /// lấy danh sách chức danh dưới dạng từ điển
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public async Task<IReadOnlyDictionary<int, string>> GetAllAsDictionary()
        {
            var res = await GetAll(isforceRefresh: true);
            return res.ToDictionary(objKey => objKey.PartID, objValue => objValue.PartName);
        }

        public Task<AdminPart> GetByID(object key)
        {
            throw new NotImplementedException();
        }

        public Task<ServerRespone> Update(AdminPart entity)
        {
            throw new NotImplementedException();
        }
    }
}
