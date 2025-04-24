using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosApp
{
    public class Pedido
    {
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public bool Urgente { get; set; }
        public double Peso { get; set; }
        public int Distancia { get; set; }
        public IMetodoEntrega MetodoEntrega { get; set; }

        public Pedido(string cliente, string producto, bool urgente, double peso, int distancia)
        {
            Cliente = cliente;
            Producto = producto;
            Urgente = urgente;
            Peso = peso;
            Distancia = distancia;
            MetodoEntrega = EntregaFactory.CrearEntrega(producto, urgente, peso);
        }

        public double ObtenerCosto() => MetodoEntrega.CalcularCosto(Distancia);
    }
}
