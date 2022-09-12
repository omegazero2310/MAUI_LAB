using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_LAB.ViewModels
{
    public class MainTabbedViewModel : ViewModelBase
    {
        private DelegateCommand<string> _commandNavigate;
        public DelegateCommand<string> CommandNavigate =>
            _commandNavigate ?? (_commandNavigate = new DelegateCommand<string>(ExecuteCommandNavigate));

        
        public MainTabbedViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        void ExecuteCommandNavigate(string parameter)
        {

        }
    }
}
