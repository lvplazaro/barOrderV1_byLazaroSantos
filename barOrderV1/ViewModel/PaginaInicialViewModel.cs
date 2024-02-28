using barOrderV1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.ViewModel
{
    internal class PaginaInicialViewModel
    {
        public List<ComandaModel> Comandas { get; set; } = new List<ComandaModel>();

        public PaginaInicialViewModel()
        {
            Comandas = new List<ComandaModel>()
            {
                new ComandaModel{ Id = 1,
                                  Nome = "Pintinho",
                                  DataAbertura = new DateTime(2024, 1, 27, 10, 30, 0),
                                  Total = 20.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  },
                new ComandaModel{ Id = 2,
                                  Nome = "Claudia",
                                  DataAbertura = DateTime.Now,
                                  Total = 00.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  },
                new ComandaModel{ Id = 3,
                                  Nome = "Claudia",
                                  DataAbertura = DateTime.Now,
                                  Total = 00.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  },
                new ComandaModel{ Id = 4,
                                  Nome = "Claudia",
                                  DataAbertura = DateTime.Now,
                                  Total = 00.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  },
                new ComandaModel{ Id = 5,
                                  Nome = "Claudia",
                                  DataAbertura = DateTime.Now,
                                  Total = 00.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  },
                new ComandaModel{ Id = 6,
                                  Nome = "Claudia",
                                  DataAbertura = DateTime.Now,
                                  Total = 00.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  },
                new ComandaModel{ Id = 7,
                                  Nome = "Claudia",
                                  DataAbertura = DateTime.Now,
                                  Total = 00.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  },
                new ComandaModel{ Id = 8,
                                  Nome = "Claudia",
                                  DataAbertura = DateTime.Now,
                                  Total = 00.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  },

        };
        }
    }
}
