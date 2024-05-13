using barOrderV1.ViewModel.Relatorios;

namespace barOrderV1.View.Relatorios;

public partial class RelatorioProdutosVendidosView : ContentPage
{
	public RelatorioProdutosVendidosView(RelatorioProdutosVendidosViewModel relatorioProdutosViewModel)
	{
		InitializeComponent();
        BindingContext = relatorioProdutosViewModel;


    }
}


