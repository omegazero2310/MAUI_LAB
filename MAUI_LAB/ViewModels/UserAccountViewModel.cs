using CommunityToolkit.Maui.Converters;
using MAUI_LAB.Entities.Respone;
using MAUI_LAB.Properties;
using MAUI_LAB.Services;
using MAUI_LAB.Services.Interface;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MAUI_LAB.ViewModels
{
    public class UserAccountViewModel : ViewModelBase, IPageLifecycleAware
    {
        #region Services
        private IAdminUserServices _adminUserService;
        private IPageDialogService _pageDialogService;
        #endregion

        #region Thuộc tính binding với View
        private ImageSource _profilePicture = ImageSource.FromFile("icon_default_profile_pic.png");
        public ImageSource ProfilePicture
        {
            get { return _profilePicture; }
            set { SetProperty(ref _profilePicture, value); }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }
        #endregion

        #region Command binding với View
        private DelegateCommand _commandChangeProfilePicture;

        public event EventHandler IsActiveChanged;

        public DelegateCommand CommandName =>
            _commandChangeProfilePicture ?? (_commandChangeProfilePicture = new DelegateCommand(ExecuteChangeProfilePicture));
        private DelegateCommand _commandLogout;
        public DelegateCommand CommandLogout =>
            _commandLogout ?? (_commandLogout = new DelegateCommand(ExecuteCommandLogout));
        #endregion



        #region Contructor
        public UserAccountViewModel(INavigationService navigationService, IAdminUserServices adminUserServices, IPageDialogService pageDialog) : base(navigationService)
        {
            this._adminUserService = adminUserServices;
            this._pageDialogService = pageDialog;
        }
        #endregion

        #region override menthod
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            RaiseIsActiveChanged();
        }
        public void OnAppearing()
        {
            this.IsActive = true;
        }

        public void OnDisappearing()
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region Interface menthod
        public async void RaiseIsActiveChanged()
        {
            if (this.IsActive)
            {
                string jsonToken = SecureStorage.GetAsync("JWT").Result;
                var token = JsonConvert.DeserializeObject<ResponeToken>(jsonToken);
                //load dữ liệu
                var userInfo = await this._adminUserService.GetByID(token.UserName, token.Token);
                this.UserName = userInfo?.DisplayName ?? "PlaceHolder Name";
                this.PhoneNumber = userInfo?.PhoneNumber ?? "PlaceHolder PhoneNumber";
                var userImage = await this._adminUserService.GetUserPicture(token.UserName, token.Token);
                if (userImage.IsSuccess)
                {
                    if (userImage.Message.Equals("GetImageSuccess"))
                    {
                        try
                        {
                            ByteArrayToImageSourceConverter byteArrayToImageSourceConverter = new ByteArrayToImageSourceConverter();
                            this.ProfilePicture = byteArrayToImageSourceConverter.ConvertFrom(Convert.FromBase64String(userImage.Result.ToString()));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                            this.ProfilePicture = ImageSource.FromFile("icon_default_profile_pic.png");
                        }
                    }

                }
                else
                {
                    this.ProfilePicture = ImageSource.FromFile("icon_default_profile_pic.png");
                }

            }

        }
        #endregion

        #region Command method
        private async void ExecuteChangeProfilePicture()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        private async Task LoadPhotoAsync(FileResult photo)
        {
            string photoPath = "";
            // trường hợp bị hủy
            if (photo == null)
            {
                photoPath = null;
                return;
            }
            // Lưu tạm ảnh vào bộ nhớ điện thoại
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);
            }


            photoPath = newFile;



            //Lấy ảnh upload lên API
            //lấy id user đăng nhập 
            string jsonToken = await SecureStorage.GetAsync("JWT");
            var token = JsonConvert.DeserializeObject<ResponeToken>(jsonToken);
            //tải ảnh lên api
            var result = this._adminUserService.UploadUserPicture(token.UserName, photoPath, token.Token);
            try
            {
                byte[] imageImg = File.ReadAllBytes(photoPath);
                ByteArrayToImageSourceConverter byteArrayToImageSourceConverter = new ByteArrayToImageSourceConverter();
                this.ProfilePicture = byteArrayToImageSourceConverter.ConvertFrom(imageImg);
            }
            catch (Exception)
            {
                this.ProfilePicture = ImageSource.FromResource("MobileAppLab.AssetImages.icon_default_profile_pic.png");
            }
        }

        private async void ExecuteCommandLogout()
        {
            if (await this._pageDialogService.DisplayAlertAsync(AppResource.Account_Confirm_Logout_Title, AppResource.Account_Confirm_Logout_Msg, "Ok", "Cancel"))
            {
                try
                {
                    AdminUserServices.Logout();
                    await this.NavigationService.NavigateAsync("/LoginPage");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        
        #endregion
    }
}
