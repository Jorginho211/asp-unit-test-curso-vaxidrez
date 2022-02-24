using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVaxi
{
    public class Cliente
    {
        public string ClienteNombre { get; set; }
        public int Descuento = 10;
        public int OrderTotal { get; set; }

        public string CrearNombreCompleto(string nombre, string apellido)
        {
            if(string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre esta en blanco");
            }

            Descuento = 30;
            ClienteNombre = $"{nombre} {apellido}";
            return ClienteNombre;
        }

        public TipoCliente GetClienteDetalle()
        {
            if (OrderTotal < 500)
                return new ClienteBasico();

            return new ClientePremium();
        }
    }

    public class TipoCliente
    {

    }

    public class ClienteBasico : TipoCliente { }

    public class ClientePremium : TipoCliente { }
}
