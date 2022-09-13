using MAUI_LAB.Entities.Respone;
using MAUI_LAB.Enums;
using MAUI_LAB.Helper;
using MAUI_LAB.Properties;
using MAUI_LAB.Services.Interface;

namespace MAUI_LAB.ViewModels
{
    public class StaffInfoDetailViewModel : ViewModelBase
    {
        #region Service
        private IAdminStaffServices _adminStaffService;
        private IAdminPartServices _adminPartService;
        public event Action<IDialogParameters> RequestClose;
        #endregion

        #region List và Dictionary
        public Dictionary<string, string> ErrorMessages { get; set; }

        private readonly static IReadOnlyDictionary<string, GenderOptions> _staffGenders = new Dictionary<string, GenderOptions>
                {
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Male)],GenderOptions.Male },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Female)],GenderOptions.Female },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Other)],GenderOptions.Other },
                };
        public List<string> StaffGenders { get; } = _staffGenders.Keys.ToList();
        #endregion

        #region thuộc tính binding với các view
        public int? ID { get; set; }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        private string _positionName;
        public string PositionName
        {
            get { return _positionName; }
            set { SetProperty(ref _positionName, value); }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }
        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }

        }
        #endregion

        #region Command binding với view
        private DelegateCommand _commandCancel;
        public DelegateCommand CommandCancel =>
            _commandCancel ?? (_commandCancel = new DelegateCommand(ExecuteCommandCancel));
        #endregion



        #region Contructor
        public StaffInfoDetailViewModel(INavigationService navigationService, IAdminStaffServices adminStaffServices, IAdminPartServices adminPartServices) : base(navigationService)
        {
            this._adminStaffService = adminStaffServices;
            this._adminPartService = adminPartServices;
        }
        #endregion

        #region override method
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters == null)
                return;
            else
            {
                if (parameters["Type"]?.ToString() == AppResource.Label_Staff_View)
                {
                    this.Title = AppResource.Label_Staff_View;
                    await this.LoadStaffInfo(parameters.GetValue<int>("Value"));
                }
            }
        }
        #endregion

        #region command method
        private async Task LoadStaffInfo(int id)
        {
            AdminStaff adminStaff = await this._adminStaffService.GetByID(id);
            var listParts = await this._adminPartService.GetAllAsDictionary();
            if (adminStaff != null)
            {
                listParts.TryGetValue(adminStaff.PartID, out var part);
                this.ID = adminStaff.StaffID;
                this.UserName = adminStaff.StaffName;
                this.Address = adminStaff.Address;
                this.PhoneNumber = adminStaff.PhoneNumber;
                this.PositionName = part;
                this.EmailAddress = adminStaff.Email;
                this.Gender = _staffGenders.Where(pos => pos.Value == adminStaff.Gender).FirstOrDefault().Key;
            }
        }
        private async void ExecuteCommandCancel()
        {
            await this.NavigationService.GoBackAsync();
        }
        #endregion

    }
}
