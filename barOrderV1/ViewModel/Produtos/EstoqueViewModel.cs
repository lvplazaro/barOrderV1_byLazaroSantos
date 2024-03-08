using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.Services;
using barOrderV1.View;
using barOrderV1.ViewModel.Comandas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace barOrderV1.ViewModel
{
    public partial class EstoqueViewModel : BaseViewModel
    {

        private readonly IProdutoService _produtoService;
        private readonly EstoqueViewModel _estoqueViewModel;

        public ObservableCollection<ProdutoModel> Produtos { get; set; } = new ObservableCollection<ProdutoModel>();


        [ObservableProperty]
        public CategoriaProduto _catSelecionada;

        public EstoqueViewModel(IProdutoService produtoService)
        {
            _produtoService = produtoService;
            Task.Run(GetProdutosAsync);
            MessagingCenter.Subscribe<AddProdutoViewModel>(this, "NovoProdutoAdicionado", async (sender) =>
            {
                await GetProdutosAsync();
            });

            MessagingCenter.Subscribe<EstoqueDetailsViewModel>(this, "ProdutoDeletado", async (sender) =>
            {
                await GetProdutosAsync();
            });

            MessagingCenter.Subscribe<ComandaAbertaViewModel>(this, "ProdutoAtualizado", async (sender) =>
            {
                await GetProdutosAsync();
            });

            MessagingCenter.Subscribe<ProdutosPopUpViewModel>(this, "ProdutoAtualizado", async (sender) =>
            {
                await GetProdutosAsync();
            });

        }

        [RelayCommand]
        public async Task GetProdutosAsync()
        {

            try
            {
                await _produtoService.InitializeAsync();
                var produtos = await _produtoService.GetProdutos();

                Produtos.Clear();

                if (produtos.Any())
                {
                    foreach (var produto in produtos)
                    {
                        Produtos.Add(produto);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        Task IrParaItens(CategoriaProduto CatSelecionada)
        {
            var detalhesViewModel = new EstoqueDetailsViewModel(Produtos, CatSelecionada, _produtoService);
            detalhesViewModel.FiltrarPorCategoria(CatSelecionada);
            var estoqueDetailsView = new EstoqueDetailsView(detalhesViewModel);
            return Shell.Current.Navigation.PushAsync(estoqueDetailsView);
        }

        [RelayCommand]
        Task IrParaAddProdutos()
        {
            return Shell.Current.GoToAsync(nameof(AddProdutoView));
        }
    }
}