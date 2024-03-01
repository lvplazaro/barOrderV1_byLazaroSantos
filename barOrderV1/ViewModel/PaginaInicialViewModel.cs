using barOrderV1.Model;
using barOrderV1.Services;
using barOrderV1.View.Comandas;
using barOrderV1.ViewModel.Comandas;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace barOrderV1.ViewModel
{
    public partial class PaginaInicialViewModel : BaseViewModel
    {
        private readonly IComandaService _comandaService;
        //public List<ComandaModel> Comandas { get; set; } = new List<ComandaModel>();

        public ObservableCollection<ComandaModel> Comandas { get; set; } = new ObservableCollection<ComandaModel>();


        public PaginaInicialViewModel(IComandaService comandaService)
        {
            _comandaService = comandaService;
            Task.Run(GetComandasAsync);

            MessagingCenter.Subscribe<AddComandaViewModel>(this, "NovoComandaAdicionada", async (sender) =>
            {
                await GetComandasAsync();
            });

            MessagingCenter.Subscribe<PaginaInicialViewModel>(this, "ComandaDeletada", async (sender) =>
            {
                await GetComandasAsync();
            });

        }

        public PaginaInicialViewModel()
        {

        }

        [RelayCommand]
        Task IrParaAddComandas()
        {
            return Shell.Current.GoToAsync(nameof(AddComandaView));
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

        [RelayCommand]
        public async Task DeletarComanda(ComandaModel comanda)
        {
            var result = await Shell.Current.DisplayAlert("Deletar", $"Confirma exclusão da comanda: \n\n \"{comanda.Nome}\" ?", "Sim", "Não");

            if (result is true)
            {
                try
                {
                    await _comandaService.InitializeAsync();

                    await _comandaService.DeleteComanda(comanda);

                    await Shell.Current.DisplayAlert("Sucesso", "Comanda deletada com sucesso!", "Ok");

                    MessagingCenter.Send(this, "ComandaDeletada");

                    _ = Task.Run(GetComandasAsync);

                    await Shell.Current.GoToAsync("..");

                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro", ex.Message, "Ok");
                }
            }

        }
    }
}

//Comandas = new List<ComandaModel>()
//            {
//                new ComandaModel{ Id = 1,
//                                  Nome = "Pintinho",
//                                  DataAbertura = new DateTime(2024, 1, 27, 10, 30, 0),
//                                  Total = 20.00,
//                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
//                                  },
//                new ComandaModel{ Id = 2,
//                                  Nome = "Claudia",
//                                  DataAbertura = DateTime.Now,
//                                  Total = 00.00,
//                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
//                                  },
//                new ComandaModel{ Id = 3,
//                                  Nome = "Claudia",
//                                  DataAbertura = DateTime.Now,
//                                  Total = 00.00,
//                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
//                                  },
//                new ComandaModel{ Id = 4,
//                                  Nome = "Claudia",
//                                  DataAbertura = DateTime.Now,
//                                  Total = 00.00,
//                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
//                                  },
//                new ComandaModel{ Id = 5,
//                                  Nome = "Claudia",
//                                  DataAbertura = DateTime.Now,
//                                  Total = 00.00,
//                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
//                                  },
//                new ComandaModel{ Id = 6,
//                                  Nome = "Claudia",
//                                  DataAbertura = DateTime.Now,
//                                  Total = 00.00,
//                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
//                                  },
//                new ComandaModel{ Id = 7,
//                                  Nome = "Claudia",
//                                  DataAbertura = DateTime.Now,
//                                  Total = 00.00,
//                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
//                                  },
//                new ComandaModel{ Id = 8,
//                                  Nome = "Claudia",
//                                  DataAbertura = DateTime.Now,
//                                  Total = 00.00,
//                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
//                                  },

//        };
//        }