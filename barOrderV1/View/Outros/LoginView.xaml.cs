using barOrderV1.ViewModel.Outros;

namespace barOrderV1.View.Outros;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
	}
}