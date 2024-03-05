using barOrderV1.ViewModel.Comandas;
using CommunityToolkit.Maui.Views;

namespace barOrderV1.View.Comandas;

public partial class ProdutosPopUpView : ContentPage
{

    public ProdutosPopUpView(ProdutosPopUpViewModel produtoPopUpViewModel)
	{
        InitializeComponent();
		BindingContext = produtoPopUpViewModel;
	}
}

