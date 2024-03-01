using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class PaginaInicialView : ContentPage
{
    private readonly PaginaInicialViewModel _paginaInicialViewModel;

    public PaginaInicialView(PaginaInicialViewModel paginaInicialViewModel)
    {
        InitializeComponent();
        _paginaInicialViewModel = paginaInicialViewModel;
        BindingContext = _paginaInicialViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _paginaInicialViewModel.GetComandasAsync();
    }
}

