using barOrderV1.View.Outros;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace barOrderV1.ViewModel.Outros
{
   public partial class ConfigViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; }
        public ConfigViewModel()
        {
            LogoutCommand = new Command(PerformLogoutOperation);
        }

        private async void PerformLogoutOperation(object obj)
        {
            Preferences.Clear();
            await Shell.Current.GoToAsync("LoginView");
        }
    }
}
