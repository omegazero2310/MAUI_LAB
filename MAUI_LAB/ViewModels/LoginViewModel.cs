using MAUI_LAB.Helper;
using MAUI_LAB.Properties;
using MAUI_LAB.Services.Interface;
using Prism.Navigation.Builder;

namespace MAUI_LAB.ViewModels
{
    /// <summary>
    /// ViewModel màn hình login
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="MobileAppLab.ViewModels.ViewModelBase" />
    public class LoginViewModel : ViewModelBase
    {
        #region các services   
        private IAdminUserServices _adminUserServices;
        private IPageDialogService _pageDialogService;
        #endregion

        #region các thuộc tính         
        /// <summary>
        /// Phiên bản phần mềm
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        private string _appVersion;
        public string AppVersion
        {
            get { return _appVersion; }
            set { SetProperty(ref _appVersion, value); }
        }
        /// <summary>
        /// Đánh dấu lưu lại thông tin đăng nhập
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        private bool _isSaveLoginInfo;
        public bool IsSaveLoginInfo
        {
            get { return _isSaveLoginInfo; }
            set { SetProperty(ref _isSaveLoginInfo, value); }
        }
        private string _userName;
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _password;
        /// <summary>
        /// mật khẩu
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private string _language = "EN";
        /// <summary>
        /// ngôn ngữ được chọn ở combobox
        /// </summary>
        /// <value>
        /// The selected language.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public string SelectedLanguage
        {
            get { return _language; }
            set { SetProperty(ref _language, value, OnPickerLangChange); }
        }
        private ImageSource imageSource;
        /// <summary>
        /// Hình ảnh logo
        /// </summary>
        /// <value>
        /// The image language.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public ImageSource ImageLanguage
        {
            get { return imageSource; }
            set { SetProperty(ref imageSource, value); }
        }
        #endregion

        #region các Dictionary cho combobox
        private readonly static IReadOnlyDictionary<string, string> _listLanguages = new Dictionary<string, string>
                {
                    {"English","en-US" },
                    {"Tiếng Việt","vi-VN"}
                };
        public List<string> ListLanguages { get; } = _listLanguages?.Keys.ToList() ?? (new Dictionary<string, string>()).Keys.ToList();
        #endregion

        #region các command
        private DelegateCommand _commandLogin;
        public DelegateCommand CommandLogin =>
            _commandLogin ?? (_commandLogin = new DelegateCommand(ExecuteCommandLogin));
        private DelegateCommand _commandForgotPassword;
        public DelegateCommand CommandForgotPassword =>
            _commandForgotPassword ?? (_commandForgotPassword = new DelegateCommand(ExecuteCommandForgotPassword));
        #endregion


        #region Contructor và Destructor
        public LoginViewModel(INavigationService navigationService, IPageDialogService pageDialog, IAdminUserServices adminUserServices)
            : base(navigationService)
        {
            this._pageDialogService = pageDialog;
            Title = "Main Page Login";
            this.AppVersion = this.GetType().Assembly.GetName().Version.ToString();
            this._adminUserServices = adminUserServices;
        }
        #endregion

        #region Override method
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            this.GetSavedUserLogin();
        }

        public override void OnNavigatedToAsync(INavigationParameters parameters)
        {
            base.OnNavigatedToAsync(parameters);
        }
        #endregion

        #region Command method
        private void GetSavedUserLogin()
        {
            this.IsSaveLoginInfo = Preferences.Get("REMEMBER_LOGIN", false);
            if (this.IsSaveLoginInfo)
            {
                this.UserName = SecureStorage.GetAsync("USER_NAME").Result;
                this.Password = SecureStorage.GetAsync("USER_PASSWORD").Result;
            }
            string language = Preferences.Get("LANGUAGE", "en-US");
            try
            {
                this.SelectedLanguage = _listLanguages.Where(val => val.Value == language).FirstOrDefault().Key ?? "English";
            }
            catch (Exception)
            {
                this.SelectedLanguage = "English";
            }
        }

        private async void ExecuteCommandLogin()
        {
            try
            {
                if (string.IsNullOrEmpty(this.UserName))
                {
                    await this._pageDialogService.DisplayAlertAsync(AppResource.MSG_LOGIN_ERROR, AppResource.MSG_ENTER_USER_ID, "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(this.Password))
                {
                    await this._pageDialogService.DisplayAlertAsync(AppResource.MSG_LOGIN_ERROR, AppResource.MSG_ENTER_PASSWORD, "Ok");
                    return;
                }

                this.IsBusy = true;
                var result = await this._adminUserServices.Login(this.UserName, this.Password);

                var language = _listLanguages[this.SelectedLanguage];
                Preferences.Set("LANGUAGE", language);
                if (result.Item1)//nếu login thành công
                {
                    //Lưu lại thông tin đăng nhập nếu tích vào checkbox
                    if (!string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password))
                    {
                        if (IsSaveLoginInfo)
                        {
                            await SecureStorage.SetAsync("USER_NAME", this.UserName);
                            await SecureStorage.SetAsync("USER_PASSWORD", this.Password);
                        }
                        else
                        {
                            SecureStorage.Remove("USER_NAME");
                            SecureStorage.Remove("USER_PASSWORD");
                        }
                        Preferences.Set("REMEMBER_LOGIN", IsSaveLoginInfo);

                    }
                    //lấy khung config tabbed page từ page MainTabbedPage, gọi prism để tự thêm các page vào code
                    this.NavigationService.CreateBuilder()
                        .AddTabbedSegment("MainTabbedPage", builder => this.InitTabbedPage(builder)).Navigate();

                    //await this.NavigationService.NavigateAsync("/MainTabbedPage");
                }
                else
                {
                    //thông báo lỗi đăng nhập
                    await this._pageDialogService.DisplayAlertAsync(AppResource.MSG_LOGIN_ERROR, LocalizationResourceManager.Instance[result.Item2], "Ok");
                }
            }
            catch (Exception ex)
            {
                await this._pageDialogService.DisplayAlertAsync(AppResource.MSG_LOGIN_ERROR, "Login Failed: " + ex.Message, "Ok");
            }
            finally
            {
                this.IsBusy = false;
            }

        }
        private void InitTabbedPage(ITabbedSegmentBuilder tabbedSegmentBuilder)
        {
            tabbedSegmentBuilder.CreateTab("HomePage");
            tabbedSegmentBuilder.CreateTab("StaffListingPage");
            tabbedSegmentBuilder.CreateTab("UserNotificationPage");
            tabbedSegmentBuilder.CreateTab("UserAccountPage");
        }    
        private async void ExecuteCommandForgotPassword()
        {
            IActionSheetButton selectCallAction = ActionSheetButton.CreateButton("Call", async () => await DialCallSupport());
            IActionSheetButton selectEmailAction = ActionSheetButton.CreateButton("Email", async () => await SendEmailSupport());
            IActionSheetButton selectHomePageAction = ActionSheetButton.CreateButton("Go to website", async () => await OpenBrowser(new Uri("https://bagps.vn/")));
            await this._pageDialogService.DisplayActionSheetAsync("Select Options", selectCallAction, selectEmailAction, selectHomePageAction);
        }
        private async Task DialCallSupport()
        {
            try
            {
                PhoneDialer.Open("19006464");
            }
            catch (ArgumentNullException)
            {
                await this._pageDialogService.DisplayAlertAsync("Unable to dial", "Phone number was not valid.", "OK");
            }
            catch (FeatureNotSupportedException)
            {
                await this._pageDialogService.DisplayAlertAsync("Unable to dial", "Phone dialing not supported.", "OK");
            }
            catch (Exception)
            {
                // Other error has occurred.
                await this._pageDialogService.DisplayAlertAsync("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
        private async Task SendEmailSupport()
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Account Support",
                    Body = "",
                    To = new List<string> { "example@email.com" },
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }
        public async Task OpenBrowser(Uri uri)
        {
            try
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }
        private async void OnPickerLangChange()
        {
            string language;
            if (Preferences.ContainsKey("LANGUAGE"))
            {
                language = Preferences.Get("LANGUAGE", "en-US");
            }
            try
            {
                language = _listLanguages[this.SelectedLanguage];
            }
            catch (Exception)
            {
                language = Preferences.Get("LANGUAGE", "en-US");
            }
            LocalizationResourceManager.Instance.SetCulture(new System.Globalization.CultureInfo(language));
            //tìm ảnh bắt đầu bằng mã quốc gia kết thúc bằng từ flag
            string resName = language.ToLower().Replace("-","_") + "_flag.png";
            this.ImageLanguage = ImageSource.FromFile(resName);
        }
        #endregion
    }
}
