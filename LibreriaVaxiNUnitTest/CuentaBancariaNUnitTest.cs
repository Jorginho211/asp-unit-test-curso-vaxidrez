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
        private CuentaBancaria cuentaBancaria;

        [SetUp]
        public void Setup()
        {
            cuentaBancaria = new CuentaBancaria(new LoggerFake());
        }

        [Test]
        public void Deposto_InputMonto100_ReturnsTrue()
        {
            var resultado = cuentaBancaria.Deposito(100);
            Assert.IsTrue(resultado);
            Assert.That(cuentaBancaria.GetBalance(), Is.EqualTo(100));
        }
    }
}
