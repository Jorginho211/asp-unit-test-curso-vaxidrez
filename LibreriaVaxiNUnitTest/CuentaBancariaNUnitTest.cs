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

        [Test]
        [TestCase(200, 100)]
        [TestCase(200, 150)]
        public void Retiro_RetiroInferiorBalance_ReturnsTrue(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            loggerMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true);
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x>0))).Returns(true);

            CuentaBancaria cuentaBancaria = new(loggerMock.Object);
            cuentaBancaria.Deposito(balance);

            var resultado = cuentaBancaria.Retiro(retiro);
            Assert.IsTrue(resultado);
        }

        [Test]
        [TestCase(200, 300)]
        public void Retiro_RetiroSuperiorBalance_ReturnsFalse(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            CuentaBancaria cuentaBancaria = new(loggerMock.Object);
            cuentaBancaria.Deposito(balance);

            var resultado = cuentaBancaria.Retiro(retiro);
            Assert.IsFalse(resultado);
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMocking_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            string textoPrueba = "hola mundo";
            loggerGeneralMock.Setup(u => u.MessageConReturnStr(It.IsAny<string>())).Returns<string>(str => str.ToLower());

            var resultado = loggerGeneralMock.Object.MessageConReturnStr("hoLA MUndo");

            Assert.That(resultado, Is.EqualTo(textoPrueba));
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingOutPut_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            string textoPrueba = "hola";

            loggerGeneralMock.Setup(u => u.MessageConOutParametroReturnBoolean(It.IsAny<string>(), out textoPrueba)).Returns(true);

            string parametroOut = string.Empty;
            var resultado = loggerGeneralMock.Object.MessageConOutParametroReturnBoolean("Vaxi", out parametroOut);

            Assert.IsTrue(resultado);
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingObjetoRef_ReturnRef()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            Cliente cliente = new Cliente();
            Cliente clienteNoUsado = new();

            loggerGeneralMock.Setup(u => u.MessageConObjetoReferenciaReturnBoolean(ref cliente)).Returns(true);

            Assert.IsTrue(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref cliente));
            Assert.IsFalse(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref clienteNoUsado));
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingPropiedadPrioridadTipo_ReturnsTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            // Se queremos setear as propiedades manualmente debese engadir o seguinte se non o seteo e ingorado
            loggerGeneralMock.SetupAllProperties();
            loggerGeneralMock.Setup(u => u.TipoLogger).Returns("warning");
            loggerGeneralMock.Setup(u => u.PrioridadLogger).Returns(10);
            
            loggerGeneralMock.Object.PrioridadLogger = 10;

            Assert.That(loggerGeneralMock.Object.TipoLogger, Is.EqualTo("warning"));
            Assert.That(loggerGeneralMock.Object.PrioridadLogger, Is.EqualTo(10));

            // CALLBACKS
            int contador = 5;
            loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>()))
                .Returns(true)
                .Callback(() => contador++);

            loggerGeneralMock.Object.LogDatabase("drez"); //vaxidrez
            Assert.That(contador, Is.EqualTo(6));
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_VerifyEjemplo()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();

            CuentaBancaria cuentaBancaria = new CuentaBancaria(loggerGeneralMock.Object);
            cuentaBancaria.Deposito(100);
            Assert.That(cuentaBancaria.GetBalance, Is.EqualTo(100));

            // verifica cuantas veces el mock esta llamando al metodo .message
            loggerGeneralMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(3)); // Exactamente 3 veces con calquera string

            loggerGeneralMock.Verify(u => u.Message("visita vaxidrez.com"), Times.AtLeastOnce); // Ao menos unha vez con ese parametro

            // Setear co valor e obter o valor unha soa vez
            loggerGeneralMock.VerifySet(u => u.PrioridadLogger = 100, Times.Once);
            loggerGeneralMock.VerifyGet(u => u.PrioridadLogger, Times.Once);
        }
    }
}
