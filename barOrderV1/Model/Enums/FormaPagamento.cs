using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Model.Enums
{
    public enum FormaPagamento : int
    {
        Definir = 0,
        Dinheiro = 1,
        CartaoCredito = 2,
        CartaoDebito = 3,
        Pix = 4,
        Outros = 5,
    }
}
