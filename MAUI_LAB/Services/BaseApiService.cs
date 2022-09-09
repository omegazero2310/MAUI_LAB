using MAUI_LAB.Constaints;
using MAUI_LAB.Entities.Respone;
using MonkeyCache.FileStore;
using Newtonsoft.Json;

namespace MAUI_LAB.Services
{
    public class BaseApiService
    {
        protected HttpClient HttpClient;
        protected string BaseUrl = "";
        protected ResponeToken UserToken;
        /// <summary>
        /// Tạo lớp cơ sở tương tác với API <see cref="BaseApiService"/> .
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="baseApiController">API controller tương tác.</param>
        /// <param name="isAuthorize">đặt <c>true</c> để sử dụng JWT token.</param>
        /// <Modified>
        /// Name Date Comments
        /// annv3 09/09/2022 created
        /// </Modified>
        public BaseApiService(HttpClient httpClient, string baseApiController, bool isAuthorize = false)
        {
            Barrel.ApplicationId = this.GetType().Assembly.GetName().Name;
            HttpClient = httpClient;
            this.BaseUrl = ApiConstaints.BaseUrl + baseApiController;
            if (isAuthorize)
                this.GetToken();
        }
        /// <summary>
        /// Lấy JWT đã lưu trong SecureStorage
        /// </summary>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        /// <exception cref="System.Exception">MSG_NOT_LOGINED</exception>
        public virtual bool GetToken()
        {
            if (UserToken == null)
            {
                var getJwtString = SecureStorage.GetAsync("JWT")?.Result ?? "";
                if (!string.IsNullOrEmpty(getJwtString))
                {
                    UserToken = JsonConvert.DeserializeObject<ResponeToken>(getJwtString);
                    return true;
                }
                else
                {
                    throw new System.Exception("MSG_NOT_LOGINED");
                }
            }
            else
                return true;
        }
    }
}
