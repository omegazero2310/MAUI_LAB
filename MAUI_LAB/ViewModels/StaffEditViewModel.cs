using MAUI_LAB.Constaints;
using MAUI_LAB.Entities.Respone;
using MAUI_LAB.Enums;
using MAUI_LAB.Helper;
using MAUI_LAB.Helper.Validation;
using MAUI_LAB.Helper.Validation.Rules;
using MAUI_LAB.Properties;
using MAUI_LAB.Services.Interface;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MAUI_LAB.ViewModels
{
    public class StaffEditViewModel : ViewModelBase
    {
        #region các Service
        private IAdminStaffServices _adminStaffService;
        private IAdminPartServices _adminPartService;
        private IPageDialogService _pageDialog;
        #endregion

        #region các Dictionary cho picker
        public Dictionary<string, string> ErrorMessages { get; set; }

        private readonly static IReadOnlyDictionary<string, GenderOptions> _staffGenders = new Dictionary<string, GenderOptions>
                {
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Male)],GenderOptions.Male },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Female)],GenderOptions.Female },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Other)],GenderOptions.Other },
                };
        public List<string> StaffGenders { get; } = _staffGenders.Keys.ToList();
        public ObservableCollection<AdminPart> StaffPositions { get; } = new ObservableCollection<AdminPart>();
        #endregion

        #region các thuộc tính binding
        private AdminPart _selectedStaffPosition;
        /// <summary>
        /// chức danh được chọn
        /// </summary>
        /// <value>
        /// The selected staff position.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public AdminPart SelectedStaffPosition
        {
            get { return _selectedStaffPosition; }
            set { SetProperty(ref _selectedStaffPosition, value); }
        }
        private bool _isEdit;
        /// <summary>
        /// Chế độ chỉnh sửa của dòng
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is edit mode; otherwise, <c>false</c>.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public bool IsEditMode
        {
            get { return _isEdit; }
            set { SetProperty(ref _isEdit, value); }
        }
        /// <summary>
        /// ID của nhân viên được chỉnh sửa, nếu null thì sẽ thêm mới
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public int? ID { get; set; }
        private ValidatableObject<string> _userName;
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public ValidatableObject<string> UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private ValidatableObject<string> _address;
        /// <summary>
        /// địa chỉ
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public ValidatableObject<string> Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private ValidatableObject<string> _phoneNumber;
        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public ValidatableObject<string> PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        private ValidatableObject<string> _positionName;
        /// <summary>
        /// tên chức danh
        /// </summary>
        /// <value>
        /// The name of the position.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public ValidatableObject<string> PositionName
        {
            get { return _positionName; }
            set { SetProperty(ref _positionName, value); }
        }

        private ValidatableObject<string> _emailAddress;
        /// <summary>
        /// địa chỉ email
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public ValidatableObject<string> EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }
        private ValidatableObject<string> _gender;
        /// <summary>
        /// giới tính
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public ValidatableObject<string> Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }
        #endregion

        #region Command
        private DelegateCommand _commandSave;
        public DelegateCommand CommandSave =>
            _commandSave ?? (_commandSave = new DelegateCommand(ExecuteCommandSave));
        private DelegateCommand _commandCancel;
        public DelegateCommand CommandCancel =>
            _commandCancel ?? (_commandCancel = new DelegateCommand(ExecuteCommandCancel));

        private DelegateCommand<object> _commandOpenPicker;
        public DelegateCommand<object> CommandOpenPicker =>
            _commandOpenPicker ?? (_commandOpenPicker = new DelegateCommand<object>(ExecuteCommandOpenPicker));


        #endregion



        #region Contructor
        public StaffEditViewModel(INavigationService navigationService, IAdminPartServices adminPartServices, IAdminStaffServices adminStaffServices, IPageDialogService pageDialogService) : base(navigationService)
        {
            this.InitVaildations();
            this._adminStaffService = adminStaffServices;
            _adminPartService = adminPartServices;
            this._pageDialog = pageDialogService;
        }
        #endregion


        private void InitVaildations()
        {
            this.UserName = new ValidatableObject<string>();
            this.Address = new ValidatableObject<string>();
            this.PositionName = new ValidatableObject<string>();
            this.PhoneNumber = new ValidatableObject<string>();
            this.Gender = new ValidatableObject<string>();
            this.EmailAddress = new ValidatableObject<string>();

            this.UserName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResource.MSG_USER_NAME_NOT_EMPTY });
            this.UserName.Validations.Add(new IsLenghtValidRule<string> { MaximunLenght = 50, ValidationMessage = AppResource.MSG_USER_NAME_OVER_CHARACTER });
            this.UserName.Validations.Add(new IsNotContainSpecialCharacterRule { ValidationMessage = AppResource.MSG_USER_NAME_CONTAIN_SPECIALCHARACTERS });

            this.Address.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResource.MSG_ADDRESS_CANNOT_EMPTY });
            this.Address.Validations.Add(new IsNotContainSpecialCharacterRule { ValidationMessage = AppResource.MSG_USER_NAME_CONTAIN_SPECIALCHARACTERS });

            this.PositionName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResource.MSG_POSITION_NOT_VALID });

            this.PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResource.MSG_PHONE_NUMBER_NOT_EMPTY });
            this.PhoneNumber.Validations.Add(new IsVaildPhoneNumberVNRule { ValidationMessage = AppResource.MSG_PHONE_NUMBER_NOT_VALID });

            this.Gender.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResource.MSG_GENDER_NOT_VALID });

            this.EmailAddress.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = AppResource.MSG_EMAIL_NOT_VALID });
        }

        #region override method
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters == null)
                return;
            else
            {
                if (parameters["Type"]?.ToString() == AppResource.Label_Staff_Add)
                {
                    this.Title = AppResource.Label_Staff_Add;
                    this.ID = null;
                    this.IsEditMode = true;
                    IEnumerable<AdminPart> parts = await this._adminPartService.GetAll();
                    this.StaffPositions.Clear();
                    foreach (var part in parts)
                    {
                        this.StaffPositions.Add(part);
                    }
                }
                else if (parameters["Type"]?.ToString() == AppResource.Label_Staff_Update)
                {
                    this.Title = AppResource.Label_Staff_Update;
                    this.IsEditMode = true;
                    this.ID = (int)parameters["Value"];
                    await this.LoadStaffInfo((int)parameters["Value"]);
                }
                else if (parameters["Type"]?.ToString() == AppResource.Label_Staff_View)
                {
                    this.Title = AppResource.Label_Staff_View;
                    this.IsEditMode = false;
                    await this.LoadStaffInfo((int)parameters["Value"]);
                }
            }
        }
        #endregion

        #region Command menthod
        private async Task LoadStaffInfo(int id)
        {
            AdminStaff adminStaff = await this._adminStaffService.GetByID(id);
            IEnumerable<AdminPart> parts = await this._adminPartService.GetAll();
            this.StaffPositions.Clear();
            foreach (var part in parts)
            {
                this.StaffPositions.Add(part);
            }
            if (adminStaff != null)
            {
                this.ID = adminStaff.StaffID;
                this.UserName.Value = adminStaff.StaffName;
                this.Address.Value = adminStaff.Address;
                this.PhoneNumber.Value = adminStaff.PhoneNumber;
                this.SelectedStaffPosition = this.StaffPositions.Where(part => part.PartID == adminStaff.PartID).FirstOrDefault();
                this.EmailAddress.Value = adminStaff.Email;
                this.Gender.Value = _staffGenders.Where(pos => pos.Value == adminStaff.Gender).FirstOrDefault().Key;
            }
        }
        private async void ExecuteCommandSave()
        {
            try
            {
                this.ErrorMessages = new Dictionary<string, string>();
                this.PositionName.Value = this.SelectedStaffPosition.PartName;
                this.UserName.Validate();
                this.Address.Validate();
                this.PositionName.Validate();
                this.PhoneNumber.Validate();
                this.Gender.Validate();
                this.EmailAddress.Validate();

                if (!this.UserName.IsValid)
                    this.ErrorMessages.Add("UserName", this.UserName.Errors.FirstOrDefault());
                if (!this.Address.IsValid)
                    this.ErrorMessages.Add("Address", this.Address.Errors.FirstOrDefault());
                if (!this.PositionName.IsValid)
                    this.ErrorMessages.Add("POSITION_ID", this.PositionName.Errors.FirstOrDefault());
                if (!this.PhoneNumber.IsValid)
                    this.ErrorMessages.Add("PhoneNumber", this.PhoneNumber.Errors.FirstOrDefault());
                if (!this.Gender.IsValid)
                    this.ErrorMessages.Add("GENDER", this.Gender.Errors.FirstOrDefault());
                if (!this.EmailAddress.IsValid)
                    this.ErrorMessages.Add("Email", this.EmailAddress.Errors.FirstOrDefault());

                if (this.ErrorMessages.Count > 0)
                {
                    RaisePropertyChanged(nameof(ErrorMessages));
                    return;
                }

                AdminStaff adminStaff = new AdminStaff();
                if (this.IsEditMode && this.ID.HasValue)
                    adminStaff.StaffID = this.ID.Value;
                adminStaff.StaffName = this.UserName.Value;
                adminStaff.Address = this.Address.Value;
                adminStaff.PhoneNumber = this.PhoneNumber.Value;
                adminStaff.PartID = this.SelectedStaffPosition.PartID;
                adminStaff.Gender = _staffGenders[this.Gender.Value];
                adminStaff.Email = this.EmailAddress.Value;

                ServerRespone serverRespone = null;
                if (IsEditMode && this.ID.HasValue)
                    serverRespone = await this._adminStaffService.Update(adminStaff);
                else
                    serverRespone = await this._adminStaffService.CreateNew(adminStaff);
                if (serverRespone.IsSuccess && (serverRespone.Message == "Updated" || serverRespone.Message == "Created"))
                {
                    NavigationParameters valuePairs = new NavigationParameters();
                    valuePairs.Add("IsSuccess", true);
                    await this.NavigationService.GoBackAsync(valuePairs);
                }
                else if (serverRespone.IsSuccess && serverRespone.Message == "NoChange")
                {
                    await this.NavigationService.GoBackAsync();
                }
                else if (!serverRespone.IsSuccess)
                {
                    if (serverRespone.Message == AdminStaffErrorCode.DUPLICATE_EMAIL)
                    {
                        ErrorMessages["Email"] = LocalizationResourceManager.Instance[AdminStaffErrorCode.DUPLICATE_EMAIL];
                        RaisePropertyChanged(nameof(ErrorMessages));
                    }
                    else if (serverRespone.Message == AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER)
                    {
                        ErrorMessages["PhoneNumber"] = LocalizationResourceManager.Instance[AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER];
                        RaisePropertyChanged(nameof(ErrorMessages));
                    }
                    else
                    {
                        await this._pageDialog.DisplayAlertAsync("Save Error", serverRespone.Message, "Ok");
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await this._pageDialog.DisplayAlertAsync("Save Error", ex.Message, "Ok");
            }
            finally
            {

            }
        }
        private async void ExecuteCommandCancel()
        {
            await this.NavigationService.GoBackAsync();
        }
        void ExecuteCommandOpenPicker(object parameter)
        {
            try
            {
                var picker = parameter as Picker;

                picker.IsEnabled = true;
                picker.IsVisible = true;
                picker.Focus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

    }
}
