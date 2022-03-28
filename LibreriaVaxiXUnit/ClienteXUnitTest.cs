using Xunit;

namespace LibreriaVaxi
{
    public class ClienteXUnitTest
    {
        private Cliente cliente;

        public ClienteXUnitTest()
        {
            cliente = new Cliente();
        }

        [Fact]
        public void CrearNombreCompleto_InputNombreApellido_ReturnNombreCompleto()
        {
            cliente.CrearNombreCompleto("Vaxi", "Drez");

            Assert.Equal("Vaxi Drez", cliente.ClienteNombre);
            Assert.Contains("Drez", cliente.ClienteNombre);
            Assert.StartsWith("Vaxi", cliente.ClienteNombre);
            Assert.EndsWith("Drez", cliente.ClienteNombre);
        }

        [Fact]
        public void ClienteNombre_NoValues_ReturnNull()
        {
            Assert.Null(cliente.ClienteNombre);
        }

        [Fact]
        public void DescuentoEvaluacion_DefaultClient_ReturnsDescuentoIntervalo()
        {
            int descuento = cliente.Descuento;
            Assert.InRange(descuento, 5, 24);
        }

        [Fact]
        public void CrearNombreCompleto_InputNombre_ReturnNotNull()
        {
            cliente.CrearNombreCompleto("Vaxi", "");

            Assert.NotNull(cliente.ClienteNombre);
            Assert.False(string.IsNullOrEmpty(cliente.ClienteNombre));
        }

        [Fact]
        public void CrearNombreCompleto_InputNombreEnBlanco_ThrowsException()
        {
            var exceptionDetalle = Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Drez"));
            Assert.Equal("El nombre esta en blanco", exceptionDetalle.Message);
            Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Drez"));
        }

        [Fact]
        public void GetClienteDetalle_CrearClienteConMenos500OrderTotal_ReturnsClienteBasico()
        {
            cliente.OrderTotal = 150;
            TipoCliente tipoCliente = cliente.GetClienteDetalle();
            Assert.IsType<ClienteBasico>(tipoCliente);
        }

        [Fact]
        public void GetClienteDetalle_CrearClienteConMas500OrderTotal_ReturnsClienteBasico()
        {
            cliente.OrderTotal = 500;
            TipoCliente tipoCliente = cliente.GetClienteDetalle();
            Assert.IsType<ClientePremium>(tipoCliente);
        }
    }
}
