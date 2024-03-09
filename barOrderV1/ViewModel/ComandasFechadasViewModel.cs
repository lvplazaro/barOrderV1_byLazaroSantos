using barOrderV1.Model;
using barOrderV1.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.ViewModel
{
    public partial class ComandasFechadasViewModel : BaseViewModel
    {
        private readonly IComandaService _comandaService;
        public ObservableCollection<ComandaModel> ComandasFechadas { get; set; } = new ObservableCollection<ComandaModel>();

        public ComandasFechadasViewModel(IComandaService comandaService)
        {
            _comandaService = comandaService;

            Task.Run(GetComandasFechadasAsync);
        }



        public async Task GetComandasFechadasAsync()
        {
            try
            {
                await _comandaService.InitializeAsync();
                var comandas = await _comandaService.GetComandas();

                ComandasFechadas.Clear();

                if (comandas.Any())
                {
                    foreach (var comanda in comandas.Where(c => c.Status == Model.Enums.ComandaStatus.Fechada).OrderByDescending(c => c.DataFechamento))
                    {
                        ComandasFechadas.Add(comanda);
                    }

                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

    }
}
