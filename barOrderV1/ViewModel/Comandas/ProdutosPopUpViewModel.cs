using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;

namespace barOrderV1.ViewModel.Comandas
{
    [QueryProperty(nameof(CategoriaSelecionada), "catSelecionada")]

    public partial class ProdutosPopUpViewModel : BaseViewModel
    {
        private readonly IComandaProdutoService _comandaProdutoService;
        private readonly IProdutoService _produtoService;
        private readonly ProdutosPopUpViewModel? _produtosPopUpViewModel;

        public ProdutosPopUpViewModel(CategoriaProduto catSelecionada, IProdutoService produtoService, ComandaModel comandaEditavel, IComandaProdutoService comandaProdutoService)
        {
            ComandaEditavel = comandaEditavel;
            _produtoService = produtoService;
            CategoriaSelecionada = catSelecionada;
            _comandaProdutoService = comandaProdutoService; 
            ProdutosPopUp = new ObservableCollection<ProdutoModel>();
            Task.Run(InitializeProdutosAsync);
        }
        public ProdutosPopUpViewModel()
        {
            
        }

        [ObservableProperty]
        private ComandaModel? _comandaEditavel;

        [ObservableProperty]
        private ProdutoModel? _produtoAdicionado;

        public ObservableCollection<ProdutoModel>? ProdutosPopUp { get; set; } = new ObservableCollection<ProdutoModel>();

        [ObservableProperty]
        public CategoriaProduto _categoriaSelecionada;

        public async Task InitializeProdutosAsync()
        {
            try
            {
                await _produtoService.InitializeAsync();
                var produtos = await _produtoService.GetProdutosPorCategoria(CategoriaSelecionada);
                ProdutosPopUp?.Clear();
                foreach (var produto in produtos)
                {
                    if (produto.QuantidadeEstoque > 0)
                    {
                        ProdutosPopUp?.Add(produto);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao recuperar produtos: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task AdicionarProdutoAComanda(ProdutoModel ProdutoAdicionado)
        {
            try
            {
                if (ComandaEditavel == null || ProdutoAdicionado == null)
                {
                    await Shell.Current.DisplayAlert("Erro", "Comanda ou produto não selecionado.", "Ok");
                    return;
                }

                else if (ProdutoAdicionado.QuantidadeEstoque <= 0)
                {
                    await Shell.Current.DisplayAlert("Erro", "Quantidade em estoque do produto é zero.", "Ok");
                    return;
                }

                await _comandaProdutoService.InitializeAsync();

                var comandasProdutos = await _comandaProdutoService.GetComandaProdutos();

                bool produtoAssociado = comandasProdutos.Any(cp => cp.ComandaId == ComandaEditavel.Id && cp.ProdutoId == ProdutoAdicionado.Id);

                if (produtoAssociado)
                {
                    await Shell.Current.DisplayAlert("Produto ja adicionado", "Altere a quantidade na lista de produtos adicionado", "Ok");
                    await Shell.Current.GoToAsync("..");

                }
                else
                {
                    if (ProdutoAdicionado.QuantidadeEstoque == 1)
                    {
                        await Shell.Current.DisplayAlert("Aviso", $"Último: \n\n \"{ProdutoAdicionado.Nome}\" em estoque!", "Ok");
                    }

                    else if (ProdutoAdicionado.QuantidadeEstoque <= ProdutoAdicionado.QuantidadeCritica +1)
                    {
                        await Shell.Current.DisplayAlert("Aviso", "Produto em quantidade crítica!", "Ok");
                    }

                    await _comandaProdutoService.AdicionarProdutoAComanda(ComandaEditavel.Id, ProdutoAdicionado.Id,1);

                    ProdutoAdicionado.QuantidadeEstoque -= 1;
                    await _produtoService.UpdateProduto(ProdutoAdicionado);

                    MessagingCenter.Send(this, "ProdutoAtualizado");

                    


                    await Shell.Current.DisplayAlert("Sucesso", "Produto adicionado à comanda com sucesso!", "Ok");


                    await Shell.Current.GoToAsync("..");
                }

                

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Erro ao adicionar produto à comanda: {ex.Message}", "Ok");
            }
        }


    }

}



