using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.Services;
using barOrderV1.View.Comandas;
using barOrderV1.ViewModel.Comandas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace barOrderV1.ViewModel
{
    public partial class PaginaInicialViewModel : BaseViewModel
    {
        private readonly IComandaService _comandaService;
        private readonly IComandaProdutoService _comandaProdutoService;

        public ObservableCollection<ComandaModel> Comandas { get; set; } = new ObservableCollection<ComandaModel>();

        [ObservableProperty]
        public string? _nome;

        [ObservableProperty]
        public DateTime _dataAbertura;

        [ObservableProperty]
        public DateTime _dataFechamento;

        public ObservableCollection<ProdutoModel>? Produtos { get; set; } = new ObservableCollection<ProdutoModel>();

        [ObservableProperty]
        public double _total;

        [ObservableProperty]
        public FormaPagamento _formaDePagamento;

        [ObservableProperty]
        public ComandaStatus _status;

        public PaginaInicialViewModel(IComandaService comandaService, IComandaProdutoService comandaProdutoService)
        {
            _comandaService = comandaService;
            _comandaProdutoService = comandaProdutoService;

            Task.Run(GetComandasAsync);
        }


        [RelayCommand]
        public async Task AddComandas()
        {
            try
            {
                string nome = await Shell.Current.DisplayPromptAsync("Adicionar Comanda", "Informe o nome do cliente:");

                if (nome == null)
                {
                    return; 
                }
                if (string.IsNullOrEmpty(nome))
                {
                    await Shell.Current.DisplayAlert("Erro", "O nome do cliente não pode estar em branco.", "Ok");
                    return;
                }

                var novaComanda = new ComandaModel
                {
                    Nome = nome,
                    DataAbertura = DateTime.Now,
                    DataFechamento = DateTime.Now.AddDays(2),
                    Total = 00.00,
                    FormaDePagamento = 0,
                    Status = 0,
                };

                await _comandaService.InitializeAsync();

                await _comandaService.AddComanda(novaComanda);

                await GetComandasAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "ok");
            }
        }

        [RelayCommand]
        Task IrParaComandaAberta(ComandaModel comandaEditar) 
        {
            ProdutoService produtoService = new ProdutoService();
            ComandaService comandaService = new ComandaService();
            ComandaProdutoService comandaProdutoService = new ComandaProdutoService(comandaService, produtoService);

            var comandaAbertaViewModel = new ComandaAbertaViewModel(comandaService, produtoService, comandaProdutoService, comandaEditar);
            var comandaAbertaView = new ComandaAbertaView(comandaAbertaViewModel);
            return Shell.Current.Navigation.PushAsync(comandaAbertaView);

        }

        [RelayCommand]
        public async Task GetComandasAsync()
        {
            try
            {
                await _comandaService.InitializeAsync();
                var comandas = await _comandaService.GetComandas();

                Comandas.Clear();

                if (comandas.Any())
                {
                    foreach (var comanda in comandas.Where(c => c.Status == 0).OrderByDescending(c => c.DataAbertura))
                    {
                        Comandas.Add(comanda);
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

