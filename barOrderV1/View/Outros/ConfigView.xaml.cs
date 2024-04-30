using barOrderV1.ViewModel.Outros;

namespace barOrderV1.View.Outros;

public partial class ConfigView : ContentPage
{
	public ConfigView()
	{
        InitializeComponent();
		BindingContext = new ConfigViewModel();
	}
}