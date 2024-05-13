using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.ViewModel
{
    public partial class RelatorioFaturamentoViewModel : BaseViewModel
    {
        private readonly IComandaService _comandaService;

        public RelatorioFaturamentoViewModel(IComandaService comandaService)
        {
            SelectedDateInicial = DateTime.Today;
            SelectedDateFinal = DateTime.Today;


            _comandaService = comandaService;
            Task.Run(GetComandasFechadasAsync);
        }

        public RelatorioFaturamentoViewModel()
        {
            
        }

        [ObservableProperty]
        public DateTime _selectedDateInicial;

        [ObservableProperty]
        public DateTime _selectedDateFinal;

        [ObservableProperty]
        public TimeSpan _selectedTimeInicial;

        [ObservableProperty]
        public TimeSpan _selectedTimeFinal;

        [ObservableProperty]
        public double _dinheiro;

        [ObservableProperty]
        public double _credito;

        [ObservableProperty]
        public double _debito;

        [ObservableProperty]
        public double _pix;

        [ObservableProperty]
        public double _outros;

        [ObservableProperty]
        public double _lucroTotal;

        public ObservableCollection<ComandaModel> ComandasFechadas { get; set; } = new ObservableCollection<ComandaModel>();

        public async Task GetComandasFechadasAsync()
        {
            try
            {
                await _comandaService.InitializeAsync();
                var comandas = await _comandaService.GetComandas();

                ComandasFechadas.Clear();

                if (comandas.Any())
                {
                    foreach (var comanda in comandas.Where(c => c.Status == ComandaStatus.Fechada).OrderByDescending(c => c.DataFechamento))
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

        [RelayCommand]
        public async Task CalculoDeLucro()
        {
            try
            {
                await GetComandasFechadasAsync();
                double dinheiroTotal = 0;
                double creditoTotal = 0;
                double debitoTotal = 0;
                double pixTotal = 0;
                double outrosTotal = 0;

                DateTime? datai = SelectedDateInicial;
                TimeSpan? horai = SelectedTimeInicial;

                if (datai.HasValue && horai.HasValue)
                {
                    DateTime inicial = new DateTime(datai.Value.Year, datai.Value.Month, datai.Value.Day,
                                                     horai.Value.Hours, horai.Value.Minutes, horai.Value.Seconds);
                    SelectedDateInicial = inicial;
                }

                DateTime? dataf = SelectedDateFinal;
                TimeSpan? horaf = SelectedTimeFinal;

                if (dataf.HasValue && horaf.HasValue)
                {
                    DateTime final = new DateTime(dataf.Value.Year, dataf.Value.Month, dataf.Value.Day,
                                                     horaf.Value.Hours, horaf.Value.Minutes, horaf.Value.Seconds);
                    SelectedDateFinal = final;
                }

                foreach (var comanda in ComandasFechadas.Where(c => c.DataFechamento >= SelectedDateInicial && c.DataFechamento <= SelectedDateFinal))
                {
                    switch (comanda.FormaDePagamento)
                    {
                        case FormaPagamento.Dinheiro:
                            dinheiroTotal += comanda.Total;
                            break;
                        case FormaPagamento.CartaoCredito:
                            creditoTotal += comanda.Total;
                            break;
                        case FormaPagamento.CartaoDebito:
                            debitoTotal += comanda.Total;
                            break;
                        case FormaPagamento.Pix:
                            pixTotal += comanda.Total;
                            break;
                        case FormaPagamento.Outros:
                            outrosTotal += comanda.Total;
                            break;
                        default:
                            break;
                    }
                }

                Dinheiro = dinheiroTotal;
                Credito = creditoTotal;
                Debito = debitoTotal;
                Pix = pixTotal;
                Outros = outrosTotal;

                LucroTotal = dinheiroTotal + creditoTotal + debitoTotal + pixTotal + outrosTotal;

                if(LucroTotal == 0)
                {
                    await Shell.Current.DisplayAlert("Aviso", "Nenhuma comanda no periodo selecionado", "Ok");

                }
                else
                {
                    await Shell.Current.DisplayAlert("Aviso", "Valores atualizados", "Ok");

                }



            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }



    }
}
