using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVaxi
{
    [TestFixture]
    public class ClienteNUnitTest
    {
        private Cliente cliente;

        [SetUp]
        public void Setup()
        {
            cliente = new Cliente();
        }

        [Test]
        public void CrearNombreCompleto_InputNombreApellido_ReturnNombreCompleto()
        {
            cliente.CrearNombreCompleto("Vaxi", "Drez");

            Assert.Multiple(() =>
            {
                Assert.That(cliente.ClienteNombre, Is.EqualTo("Vaxi Drez"));
                Assert.AreEqual(cliente.ClienteNombre, "Vaxi Drez");
                Assert.That(cliente.ClienteNombre, Does.Contain("Drez"));
                Assert.That(cliente.ClienteNombre, Does.Contain("drez").IgnoreCase);
                Assert.That(cliente.ClienteNombre, Does.StartWith("Vaxi"));
                Assert.That(cliente.ClienteNombre, Does.EndWith("Drez"));
            });
        }

        [Test]
        public void ClienteNombre_NoValues_ReturnNull()
        {
            Assert.IsNull(cliente.ClienteNombre);
        }

        [Test]
        public void DescuentoEvaluacion_DefaultClient_ReturnsDescuentoIntervalo()
        {
            int descuento = cliente.Descuento;
            Assert.That(descuento, Is.InRange(5, 24));
        }

        [Test]
        public void CrearNombreCompleto_InputNombre_ReturnNotNull()
        {
            cliente.CrearNombreCompleto("Vaxi", "");

            Assert.IsNotNull(cliente.ClienteNombre);
            Assert.IsFalse(string.IsNullOrEmpty(cliente.ClienteNombre));
        }

        [Test]
        public void CrearNombreCompleto_InputNombreEnBlanco_ThrowsException()
        {
            var exceptionDetalle = Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Drez"));
            Assert.AreEqual("El nombre esta en blanco", exceptionDetalle.Message);
            Assert.That(() => cliente.CrearNombreCompleto("", "Drez"), Throws.ArgumentException);
        }

        [Test]
        public void GetClienteDetalle_CrearClienteConMenos500OrderTotal_ReturnsClienteBasico()
        {
            cliente.OrderTotal = 150;
            TipoCliente tipoCliente = cliente.GetClienteDetalle();
            Assert.That(tipoCliente, Is.TypeOf<ClienteBasico>());
        }

        [Test]
        public void GetClienteDetalle_CrearClienteConMas500OrderTotal_ReturnsClienteBasico()
        {
            cliente.OrderTotal = 500;
            TipoCliente tipoCliente = cliente.GetClienteDetalle();
            Assert.That(tipoCliente, Is.TypeOf<ClientePremium>());
        }
    }
}
