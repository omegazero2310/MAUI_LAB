using MAUI_LAB.Entities.Request;
using MAUI_LAB.Entities.Respone;
using MAUI_LAB.Services.Interface;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace MAUI_LAB.Services
{
    /// <summary>
    /// Service tương tác với API đăng nhập
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="MobileAppLab.ApiServices.BaseApiService" />
    /// <seealso cref="MobileAppLab.ApiServices.IService&lt;CommonClass.Models.AdminUser&gt;" />
    public class AdminUserServices : BaseApiService, IAdminUserServices
    {
        public AdminUserServices(HttpClient httpClient) : base(httpClient, "AdminUsers")
        {
        }
        /// <summary>
        /// Đăng nhập vào hệ thống
        /// </summary>
        /// <param name="userName">tên đăng nhập.</param>
        /// <param name="password">mật khẩu.</param>
        /// <returns>Lưu lại JWT và trả kết quả <c>true</c> và chuỗi trống, trả về false và thông báo lỗi nếu có lỗi xảy ra </returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public async Task<(bool, string)> Login(string userName, string password)
        {
            string contentRespone = "";
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, this.BaseUrl + "/Login");
                LoginRequest userLogin = new LoginRequest();
                userLogin.UserName = userName;
                userLogin.Password = password;
                message.Content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");
                var respone = await HttpClient.SendAsync(message);
                contentRespone = respone.Content.ReadAsStringAsync().Result;
                respone.EnsureSuccessStatusCode();
                //lấy token lưu tạm để dùng cho các lần sau
                ServerRespone serverRespone = JsonConvert.DeserializeObject<ServerRespone>(respone.Content.ReadAsStringAsync().Result);
                if (serverRespone.IsSuccess)
                {
                    await SecureStorage.SetAsync("JWT", serverRespone.Result.ToString());
                    return (true, "");
                }
                else
                {
                    return (false, serverRespone.Message);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return (false, ex.Message);
            }

        }
        /// <summary>
        /// Đăng xuất, xóa tất cả cache và JWT
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public async static void Logout()
        {
            Barrel.Current.EmptyAll();
            SecureStorage.Remove("JWT");
        }
        public async Task<ServerRespone> GetUserPicture(string userName, string token)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, this.BaseUrl + $"/GetProfilePicture?userName={userName}");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var respone = await HttpClient.SendAsync(message);
                return JsonConvert.DeserializeObject<ServerRespone>(await respone.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ServerRespone() { IsSuccess = false, Result = ex };
            }
        }
        public async Task<ServerRespone> UploadUserPicture(string userName, string filePath, string token)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, this.BaseUrl + $"/UploadProfilePicture?userName={userName}");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    //Load the file and set the file's Content-Type header
                    var fileStreamContent = new StreamContent(File.OpenRead(filePath));
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                    //Add the file
                    multipartFormContent.Add(fileStreamContent, name: "file", fileName: "profile.png");
                    message.Content = multipartFormContent;
                    //Send it
                    var response = await HttpClient.SendAsync(message);
                    return JsonConvert.DeserializeObject<ServerRespone>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ServerRespone() { IsSuccess = false, Result = ex };
            }
        }

        public Task<ServerRespone> CreateNew(AdminUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<ServerRespone> Delete(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminUser>> GetAll(int skip = 0, int take = 0, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminUser> GetByID(object key, string token)
        {
            try
            {


                if ((Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: this.BaseUrl + $"/Get/{key}")))
                {
                    return Barrel.Current.Get<AdminUser>(key: this.BaseUrl + $"/Get/{key}");
                }

                var asm = this.GetType().Assembly;
                //ảnh mặc định
                System.IO.Stream stream = asm.GetManifestResourceStream("MobileAppLab.AssetImages.icon_default_profile_pic.png");
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, this.BaseUrl + $"/Get?userName={key}");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                ServerRespone serverRespone = JsonConvert.DeserializeObject<ServerRespone>(respone.Content.ReadAsStringAsync().Result);
                var user = JsonConvert.DeserializeObject<AdminUser>(serverRespone.Result.ToString());
                var profilePic = await this.GetUserPicture(user.UserName, token);

                if (profilePic.IsSuccess && profilePic.Message != "NoImage")
                {
                    user.ProfileImg = Convert.FromBase64String(profilePic.Result.ToString());
                }
                else
                {
                    user.ProfileImg = data;
                }

                Barrel.Current.Add(key: this.BaseUrl + $"/Get/{key}", data: user, expireIn: TimeSpan.FromDays(1));
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<ServerRespone> Update(AdminUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<AdminUser> GetByID(object key)
        {
            throw new NotImplementedException();
        }
    }
}
