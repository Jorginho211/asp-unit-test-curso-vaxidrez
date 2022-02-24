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
    public class CuentaBancariaNUnitTest
    {
        [Test]
        public void Deposto_InputMonto100_ReturnsTrue()
        {
            CuentaBancaria cuentaBancaria = new CuentaBancaria(new LoggerFake());
            var resultado = cuentaBancaria.Deposito(100);
            Assert.IsTrue(resultado);
            Assert.That(cuentaBancaria.GetBalance(), Is.EqualTo(100));
        }

        [Test]
        public void Deposto_InputMonto100Mocking_ReturnsTrue()
        {
            var mocking = new Mock<ILoggerGeneral>();

            CuentaBancaria cuentaBancaria = new CuentaBancaria(mocking.Object);
            var resultado = cuentaBancaria.Deposito(100);
            Assert.IsTrue(resultado);
            Assert.That(cuentaBancaria.GetBalance(), Is.EqualTo(100));
        }
    }
}
