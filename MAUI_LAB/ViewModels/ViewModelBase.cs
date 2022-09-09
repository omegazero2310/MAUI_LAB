namespace MAUI_LAB.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
                RaisePropertyChanged();
            }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        private bool _isDisconnected = false;
        public bool IsDisconnected
        {
            get { return _isDisconnected; }
            set { SetProperty(ref _isDisconnected, value, OnDisconnected); }
        }
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            IsDisconnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
        }
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsDisconnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
        }
        private async void OnDisconnected()
        {
            if (IsDisconnected)
            {

            }

        }
        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedToAsync(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
