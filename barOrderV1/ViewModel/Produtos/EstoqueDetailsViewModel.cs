using barOrderV1.Model.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using barOrderV1.ViewModel;
using barOrderV1.Services;
using barOrderV1.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using barOrderV1.View;

namespace barOrderV1.ViewModel
{

    [QueryProperty(nameof(CategoriaSelecionada), "catSelecionada")]
    public partial class EstoqueDetailsViewModel : BaseViewModel
    {
        private readonly IProdutoService _produtoService;

        public AddProdutoViewModel AddProdutoViewModel { get; set; }

        [ObservableProperty]
        public string? _categoriaSelecionada;

        [ObservableProperty]
        public ProdutoModel? _nome;

        public ObservableCollection<ProdutoModel>? ProdutosFiltrados { get; set; }
        private List<ProdutoModel> TodosProdutos { get; set; }

        public EstoqueDetailsViewModel(ObservableCollection<ProdutoModel> produtos, CategoriaProduto CatSelecionada, IProdutoService produtoService)
        {
            _produtoService = produtoService;
            CategoriaSelecionada = CatSelecionada.ToString();
            TodosProdutos = new List<ProdutoModel>(produtos);
            ProdutosFiltrados = new ObservableCollection<ProdutoModel>(TodosProdutos);
        }
        public EstoqueDetailsViewModel()
        {

        }
        public void FiltrarPorCategoria(CategoriaProduto categoria)
        {
            var produtosFiltrados = TodosProdutos.Where(produto => produto.Categoria == categoria);
            ProdutosFiltrados.Clear();
            foreach (var produto in produtosFiltrados)
            {
                ProdutosFiltrados.Add(produto);
            }
        }

        [RelayCommand]
        public async Task DeletarProduto(ProdutoModel produto)
        {
            var result = await Shell.Current.DisplayAlert("Deletar", $"Confirma exclusão do produto: \n\n \"{produto.Nome}\" ?", "Sim", "Não");

            if (result is true)
            {
                try
                {
                    await _produtoService.InitializeAsync();

                    await _produtoService.DeleteProduto(produto);

                    await Shell.Current.DisplayAlert("Sucesso", "Produto deletado com sucesso!", "Ok");

                    MessagingCenter.Send(this, "ProdutoDeletado");

                    await Shell.Current.GoToAsync("..");

                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro", ex.Message, "Ok");
                }
            }

        }


        [RelayCommand]
        private async Task EditarProdutos(ProdutoModel produtoEditar) =>
            await Shell.Current.GoToAsync("EditProdutoView", new Dictionary<string, object>
            {
                {"ProdutoObject",produtoEditar }
            });

    }
}
