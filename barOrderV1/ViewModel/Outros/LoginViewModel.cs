using barOrderV1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace barOrderV1.ViewModel.Outros
{
    public partial class LoginViewModel : BaseViewModel
    {
        private UsuarioModel myLoginRequestModel = new UsuarioModel();
        public UsuarioModel MyLoginRequestModel
        {
            get { return myLoginRequestModel; }
            set
            {
                myLoginRequestModel = value;

                OnPropertyChanged(nameof(MyLoginRequestModel));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(PerformLoginOperation);
        }

        private async void PerformLoginOperation(object obj)
        {
            //Perform API Operation / Db Operation

            UsuarioModel data = MyLoginRequestModel;

            Preferences.Set("UserAlreadyLoggedIn", true);

            await Shell.Current.GoToAsync(state: "//MainPage");
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }





    }
}

