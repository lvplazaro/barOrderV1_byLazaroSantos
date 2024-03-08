using barOrderV1.ViewModel.Comandas;

namespace barOrderV1.View.Comandas;

public partial class ComandaAbertaView : ContentPage
{
    private readonly ComandaAbertaViewModel _comandaAbertaViewModel;

    public ComandaAbertaView(ComandaAbertaViewModel comandaAbertaViewModel)
    {
        InitializeComponent();
        _comandaAbertaViewModel = comandaAbertaViewModel; // Inicializa o _comandaAbertaViewModel
        BindingContext = comandaAbertaViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _comandaAbertaViewModel.GetComandaProdutosAsync();
    }
}

