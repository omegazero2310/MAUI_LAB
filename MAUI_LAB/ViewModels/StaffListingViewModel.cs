using MAUI_LAB.Constaints;
using MAUI_LAB.Entities.Respone;
using MAUI_LAB.Helper;
using MAUI_LAB.Properties;
using MAUI_LAB.Services.Interface;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MAUI_LAB.ViewModels
{
    public class StaffListingViewModel : ViewModelBase, IPageLifecycleAware
    {
        #region Service      
        private IAdminStaffServices _adminStaffService;
        private IAdminPartServices _adminPartService;
        private IPageDialogService _dialogService;
        private IDialogService _dialog;
        #endregion

        #region các thuộc tính binding
        private static IReadOnlyDictionary<int, string> _staffPositions = new Dictionary<int, string>();
        public ObservableCollection<AdminStaff> Staffs { get; } = new ObservableCollection<AdminStaff>();
        private AdminStaff _selectedStaff;
        public AdminStaff SelectedStaff
        {
            get { return _selectedStaff; }
            set { SetProperty(ref _selectedStaff, value); }
        }
        public event EventHandler IsActiveChanged;
        /// <summary>
        /// kiểm tra xem tab đã được kích hoạt
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        private string _filterPrefix;
        public string FilterPrefix
        {
            get { return _filterPrefix; }
            set { SetProperty(ref _filterPrefix, value); }
        }
        #endregion

        #region các command binding

        /// <summary>
        /// lệnh load dữ liệu
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand _commandLoadData;
        public DelegateCommand CommandLoadData =>
            _commandLoadData ?? (_commandLoadData = new DelegateCommand(ExecuteCommandLoadData));

        /// <summary>
        /// lệnh gạt phải chỉnh sửa
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand<AdminStaff> _commandSwipeEdit;
        public DelegateCommand<AdminStaff> CommandSwipeEdit =>
            _commandSwipeEdit ?? (_commandSwipeEdit = new DelegateCommand<AdminStaff>(ExecuteCommandSwipeEdit));

        /// <summary>
        /// lệnh gạt phải xóa
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand<AdminStaff> _commandSwipeDelete;
        public DelegateCommand<AdminStaff> CommandSwipeDelete =>
            _commandSwipeDelete ?? (_commandSwipeDelete = new DelegateCommand<AdminStaff>(ExecuteCommandSwipeDelete));

        /// <summary>
        /// lệnh chạm để xem chi tiết
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand<AdminStaff> _commandView;
        public DelegateCommand<AdminStaff> CommandView =>
            _commandView ?? (_commandView = new DelegateCommand<AdminStaff>(ExecuteCommandView));

        /// <summary>
        /// lệnh tạo mới nhân viên
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand _commandNewStaff;
        public DelegateCommand CommandNewStaff =>
            _commandNewStaff ?? (_commandNewStaff = new DelegateCommand(ExecuteCommandNewStaff));

        /// <summary>
        /// lệnh quay về màn hình chính (home)
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>

        /// <summary>
        /// lệnh tìm kiếm khi nhập vào control searchbar
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand<object> _commandSearch;
        public DelegateCommand<object> CommandSearch =>
            _commandSearch ?? (_commandSearch = new DelegateCommand<object>(ExecuteCommandSearch));


        #endregion

        public StaffListingViewModel(INavigationService navigationService, IDialogService dialogService, IPageDialogService pageDialogService,
            IAdminStaffServices adminStaffServices, IAdminPartServices adminPartServices)
            : base(navigationService)
        {
            this._adminStaffService = adminStaffServices;
            this._adminPartService = adminPartServices;
            this._dialogService = pageDialogService;
            this._dialog = dialogService;
        }
        /// <summary>
        /// Tải dữ liệu nhân viên
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private async void LoadStaffs()
        {
            try
            {
                _staffPositions = await this._adminPartService.GetAllAsDictionary();
                this.IsRefreshing = true;
                this.Staffs.Clear();
                var listStaff = await this._adminStaffService.GetAll(isForceRefresh: true);
                foreach (var user in listStaff)
                {
                    if (user.ProfilePicture.Length == 0)
                    {
                        user.ProfilePicture = Convert.FromBase64String(Base64ImageString.DEFAULTPROFILE);
                    }
                    this.Staffs.Add(user);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await this._dialogService.DisplayAlertAsync("Load Error", ex.Message, "OK");
            }
            finally
            {
                this.IsRefreshing = false;
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                if (parameters.GetValue<bool>("IsSuccess"))//IsSuccess
                    this.IsRefreshing = true;
            }
            catch (Exception)
            {
                this.IsRefreshing = false;
            }
            
        }
        public void OnAppearing()
        {
            this.IsActive = true;
        }

        public void OnDisappearing()
        {
            this.IsActive = false;
        }
        private void RaiseIsActiveChanged()
        {
            //nếu tab được chọn. load lại list
            if (this.IsActive)
            {
                this.IsRefreshing = true;
                this.FilterPrefix = LocalizationResourceManager.Instance[nameof(AppResource.PlaceHolder_SearchBar)];
            }

        }
        #region các method của command
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        private async void ExecuteCommandSearch(object parameter)
        {
            try
            {
                //delay 500ms mỗi khi nhập từ tìm kiếm, hạn chế bị nháy list khi tìm kiếm
                Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel();
                await Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCts.Token)
                    .ContinueWith(async (obj) =>
                    {
                        if (string.IsNullOrEmpty(parameter?.ToString() ?? ""))
                            this.IsRefreshing = true;
                        else
                        {
                            var listStaff = await this._adminStaffService.GetAll();
                            IEnumerable<AdminStaff> filtered = listStaff;
                            filtered = listStaff.Where(x => x.StaffName.IndexOf(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0
                            || x.Email.IndexOf(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0
                            || x.PhoneNumber.IndexOf(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0
                            || x.PositionName.IndexOf(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0);

                            this.Staffs.Clear();
                            foreach (var user in filtered)
                            {
                                this.Staffs.Add(user);
                            }
                        }
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }
        private void ExecuteCommandLoadData()
        {
            this.LoadStaffs();
        }
        private async void ExecuteCommandView(AdminStaff parameter)
        {
            if (parameter == null)
                return;
            this.NavigationService.CreateBuilder()
                        .AddSegment("StaffDetailInfoPage", segment =>
                        {
                            segment.AddParameter("Type", AppResource.Label_Staff_View);
                            segment.AddParameter("Value", parameter.StaffID);
                        }).Navigate();
        }
        private async void ExecuteCommandNewStaff()
        {
            this.NavigationService.CreateBuilder()
                        .AddSegment("StaffEditPopupPage", segment =>
                        {
                            segment.AddParameter("Type", AppResource.Label_Staff_Add);
                            segment.AddParameter("Value", "");
                        }).Navigate();
        }
        private async void ExecuteCommandSwipeEdit(AdminStaff parameter)
        {
            if (parameter == null)
                return;
            this.NavigationService.CreateBuilder()
                        .AddSegment("StaffEditPopupPage", segment =>
                        {
                            segment.AddParameter("Type", AppResource.Label_Staff_Update);
                            segment.AddParameter("Value", parameter.StaffID);
                        }).Navigate();
        }
        private async void ExecuteCommandSwipeDelete(AdminStaff parameter)
        {
            if (parameter == null)
                return;
            if (await this._dialogService.DisplayAlertAsync(AppResource.MSG_CONFIRM_DELETE_TITLE, string.Format(AppResource.MSG_CONFIRM_DELETE, parameter.StaffID, parameter.StaffName), AppResource.Label_Delete, "Cancel"))
            {
                try
                {
                    await this._adminStaffService.Delete(parameter.StaffID);
                    this.Staffs.Remove(parameter);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    await this._dialogService.DisplayAlertAsync("Delete Error", ex.Message, "OK");
                }
            }


        }



        #endregion

    }
}
