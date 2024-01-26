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
                                  DataAbertura = DateTime.Now,
                                  Total = 00.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  },
                new ComandaModel{ Id = 2,
                                  Nome = "Claudia",
                                  DataAbertura = DateTime.Now,
                                  Total = 00.00,
                                  FormaDePagamento = Model.Enums.FormaPagamento.Dinheiro
                                  }

        };
        }
    }
}
