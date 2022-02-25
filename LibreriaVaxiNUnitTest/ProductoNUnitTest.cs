using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVaxi
{
    [TestFixture]
    public class ProductoNUnitTest
    {
        
        [Test]
        public void GetPrecio_PremiumCliente_ReturnsPrecio80()
        {
            Producto producto = new Producto
            {
                Precio = 50
            };

            var mocking = new Mock<ICliente>();
            mocking.Setup(c => c.IsPremium).Returns(true);

            double precio = producto.GetPrecio(mocking.Object);
            Assert.That(precio, Is.EqualTo(40));
        }
    }
}
