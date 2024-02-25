using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace barOrderV1.ViewModel
{
    public partial class AddProdutoViewModel : BaseViewModel
    {


        private readonly IProdutoService _produtoService;

        public ProdutoModel ProdutoModel { get; private set; }

        public AddProdutoViewModel(IProdutoService produtoService)
        {
            _produtoService = produtoService;

        }

        public IList<CategoriaProduto> CategoriasList => Enum.GetValues(typeof(CategoriaProduto)).Cast<CategoriaProduto>().ToList();


        [ObservableProperty]
        public string _nome;

        [ObservableProperty]
        public double _preco;

        [ObservableProperty]
        public CategoriaProduto _categoria;

        [ObservableProperty]
        public int _quantidadeEstoque;

        [ObservableProperty]
        public int _quantidadeCritica;

        [ObservableProperty]
        public CategoriaProduto _selectedCategoriaProduto;


        [RelayCommand]
        public async Task AddProduto()
        {
            try
            {
                var novoProduto = new ProdutoModel
                {
                    Nome = Nome,
                    Preco = Preco,
                    Categoria = SelectedCategoriaProduto,
                    QuantidadeEstoque = QuantidadeEstoque,
                    QuantidadeCritica = QuantidadeCritica
                };
                if (string.IsNullOrEmpty(novoProduto.Nome) || novoProduto.Preco < 0 || novoProduto.QuantidadeEstoque < 1)
                {
                    if (string.IsNullOrEmpty(novoProduto.Nome))
                    {
                        await Shell.Current.DisplayAlert("Erro", "Informe o nome do produto", "Ok");
                        return;
                    }

                    else if (novoProduto.Preco < 0)
                    {
                        await Shell.Current.DisplayAlert("Erro", "Valor do produto não pode ser negativo", "Ok");
                        return;
                    }
                    else if (novoProduto.QuantidadeEstoque < 1)
                    {
                        await Shell.Current.DisplayAlert("Erro", "Informe a quantidade em estoque do produto", "Ok");
                        return;
                    }

                }
                await _produtoService.InitializeAsync();
                await _produtoService.AddProduto(novoProduto);

                await Shell.Current.DisplayAlert("Sucesso", "Produto adicionado com sucesso!", "Ok");

                MessagingCenter.Send(this, "NovoProdutoAdicionado");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "ok");
            }
        }
    }
}
