using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class ComandasFechadasView : ContentPage
{
    private readonly ComandasFechadasViewModel _comandasFechadasViewModel;
    public ComandasFechadasView(ComandasFechadasViewModel comandasFechadasViewModel)
    {
        InitializeComponent();
        _comandasFechadasViewModel = comandasFechadasViewModel;
        BindingContext = _comandasFechadasViewModel;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _comandasFechadasViewModel.GetComandasFechadasAsync();
    }
}

