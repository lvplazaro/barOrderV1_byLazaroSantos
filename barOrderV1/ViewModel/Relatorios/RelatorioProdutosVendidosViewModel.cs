using barOrderV1.Model;
using barOrderV1.Model.Auxiliar;
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

namespace barOrderV1.ViewModel.Relatorios
{
    public partial class RelatorioProdutosVendidosViewModel : BaseViewModel
    {
        private readonly IComandaService _comandaService;
        private readonly IComandaProdutoService _comandaProdutoService;
        private readonly IProdutoService _produtoService;



        public RelatorioProdutosVendidosViewModel(IComandaService comandaService, IComandaProdutoService comandaProdutoService, IProdutoService produtoService)
        {
            SelectedDateInicial = DateTime.Today;
            SelectedDateFinal = DateTime.Today;


            _produtoService = produtoService;
            _comandaProdutoService = comandaProdutoService;
            _comandaService = comandaService;
            Task.Run(GetComandasFechadasAsync);
        }

        public RelatorioProdutosVendidosViewModel()
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


        public ObservableCollection<ComandaModel> ComandasFechadas { get; set; } = new ObservableCollection<ComandaModel>();
        public ObservableCollection<ProdutoVendidoModel> MaisVendidos { get; set; } = new ObservableCollection<ProdutoVendidoModel>();



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
        public async Task CalculoProdutos()
        {
            try
            {
                await GetComandasFechadasAsync();
                await _comandaProdutoService.InitializeAsync();
                await _produtoService.InitializeAsync();
                var comandasProdutos = await _comandaProdutoService.GetComandaProdutos();

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

                Dictionary<int, int> totalVendasPorProduto = new Dictionary<int, int>();

                
                if (!ComandasFechadas.Any(c => c.DataFechamento >= SelectedDateInicial && c.DataFechamento <= SelectedDateFinal))
                {
                    await Shell.Current.DisplayAlert("Aviso", "Nenhuma comanda encontrada no período.", "Ok");
                }
                else
                {
                    foreach (var comanda in ComandasFechadas.Where(c => c.DataFechamento >= SelectedDateInicial && c.DataFechamento <= SelectedDateFinal))
                    {
                        foreach (var comandaProduto in comandasProdutos.Where(cp => cp.ComandaId == comanda.Id))
                        {
                            if (totalVendasPorProduto.ContainsKey(comandaProduto.ProdutoId))
                            {
                                totalVendasPorProduto[comandaProduto.ProdutoId] += comandaProduto.QuantidadeDeProduto;
                            }
                            else
                            {
                                totalVendasPorProduto[comandaProduto.ProdutoId] = comandaProduto.QuantidadeDeProduto;
                            }
                        }
                    }
                }


                var produtosMaisVendidos = totalVendasPorProduto.OrderByDescending(kv => kv.Value).Take(5);

                MaisVendidos.Clear();

                foreach (var produtoMaisVendido in produtosMaisVendidos)
                {
                    var produto = await _produtoService.GetProdutoPorId(produtoMaisVendido.Key);
                    var produtoVendidoModel = new ProdutoVendidoModel
                    {
                        Nome = produto.Nome,
                        QuantidadeVendida = produtoMaisVendido.Value
                    };
                    MaisVendidos.Add(produtoVendidoModel);


                }

                await Shell.Current.DisplayAlert("Sucesso", "Produtos atualizados !", "Ok");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }

    }












}

