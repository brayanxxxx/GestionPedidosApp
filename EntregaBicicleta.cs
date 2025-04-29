using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosApp
{
    public class EntregaBicicleta : IMetodoEntrega
    {
        public double CalcularCosto(int km) => 3 * km;
        public string TipoEntrega() => "Bicicleta";
    }
}