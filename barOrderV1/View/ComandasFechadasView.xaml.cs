using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class ComandasFechadasView : ContentPage
{
	public ComandasFechadasView()
	{
		InitializeComponent();
		BindingContext = new ComandasFechadasViewModel();
	}
}