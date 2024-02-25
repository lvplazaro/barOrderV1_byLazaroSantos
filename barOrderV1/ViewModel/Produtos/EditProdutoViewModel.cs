using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.ViewModel
{

    [QueryProperty(nameof(ProdutoEditavel), "ProdutoObject")]

    public partial class EditProdutoViewModel : BaseViewModel
    {
        private readonly IProdutoService _produtoService;

        [ObservableProperty]
        private ProdutoModel? _produtoEditavel;

        public EditProdutoViewModel(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public IList<CategoriaProduto> CategoriasList => Enum.GetValues(typeof(CategoriaProduto)).Cast<CategoriaProduto>().ToList();


        [RelayCommand]
        private async Task UpdateProduto()
        {
            if (ProdutoEditavel == null)
            {
                await Shell.Current.DisplayAlert("Erro", "Nenhum produto selecionado para edição.", "Ok");
                return;
            }
            else if (string.IsNullOrEmpty(ProdutoEditavel.Nome) || ProdutoEditavel.Preco < 0 || ProdutoEditavel.QuantidadeEstoque < 1)
            {
                if (string.IsNullOrEmpty(ProdutoEditavel.Nome))
                {
                    await Shell.Current.DisplayAlert("Erro", "Informe o nome do produto", "Ok");
                    return;
                }

                else if (ProdutoEditavel.Preco < 0)
                {
                    await Shell.Current.DisplayAlert("Erro", "Valor do produto não pode ser negativo", "Ok");
                    return;
                }
                else if (ProdutoEditavel.QuantidadeEstoque < 1)
                {
                    await Shell.Current.DisplayAlert("Erro", "Informe a quantidade em estoque do produto", "Ok");
                    return;
                }

            }

            try
            {
                

                await _produtoService.InitializeAsync();
                await _produtoService.UpdateProduto(ProdutoEditavel);

                await Shell.Current.DisplayAlert("Sucesso", "Produto Editado com sucesso!", "Ok");

                await Shell.Current.GoToAsync("../..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "ok");
            }
        }

        [RelayCommand]
        async Task BackButtonPressed()
        {
            await Shell.Current.GoToAsync("..");

        }

    }
}
