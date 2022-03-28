using Moq;
using Xunit;

namespace LibreriaVaxi
{
    public class CuentaBancariaXUnitTest
    {
        [Fact]
        public void Deposto_InputMonto100_ReturnsTrue()
        {
            CuentaBancaria cuentaBancaria = new CuentaBancaria(new LoggerFake());
            var resultado = cuentaBancaria.Deposito(100);
            Assert.True(resultado);
            Assert.Equal(100, cuentaBancaria.GetBalance());
        }

        [Fact]
        public void Deposto_InputMonto100Mocking_ReturnsTrue()
        {
            var mocking = new Mock<ILoggerGeneral>();

            CuentaBancaria cuentaBancaria = new CuentaBancaria(mocking.Object);
            var resultado = cuentaBancaria.Deposito(100);
            Assert.True(resultado);
            Assert.Equal(100, cuentaBancaria.GetBalance());
        }

        [Theory]
        [InlineData(200, 100)]
        [InlineData(200, 150)]
        public void Retiro_RetiroInferiorBalance_ReturnsTrue(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            loggerMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true);
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x>0))).Returns(true);

            CuentaBancaria cuentaBancaria = new(loggerMock.Object);
            cuentaBancaria.Deposito(balance);

            var resultado = cuentaBancaria.Retiro(retiro);
            Assert.True(resultado);
        }

        [Theory]
        [InlineData(200, 300)]
        public void Retiro_RetiroSuperiorBalance_ReturnsFalse(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            CuentaBancaria cuentaBancaria = new(loggerMock.Object);
            cuentaBancaria.Deposito(balance);

            var resultado = cuentaBancaria.Retiro(retiro);
            Assert.False(resultado);
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMocking_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            string textoPrueba = "hola mundo";
            loggerGeneralMock.Setup(u => u.MessageConReturnStr(It.IsAny<string>())).Returns<string>(str => str.ToLower());

            var resultado = loggerGeneralMock.Object.MessageConReturnStr("hoLA MUndo");

            Assert.Equal(textoPrueba, resultado);
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMockingOutPut_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            string textoPrueba = "hola";

            loggerGeneralMock.Setup(u => u.MessageConOutParametroReturnBoolean(It.IsAny<string>(), out textoPrueba)).Returns(true);

            string parametroOut = string.Empty;
            var resultado = loggerGeneralMock.Object.MessageConOutParametroReturnBoolean("Vaxi", out parametroOut);

            Assert.True(resultado);
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMockingObjetoRef_ReturnRef()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            Cliente cliente = new Cliente();
            Cliente clienteNoUsado = new();

            loggerGeneralMock.Setup(u => u.MessageConObjetoReferenciaReturnBoolean(ref cliente)).Returns(true);

            Assert.True(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref cliente));
            Assert.False(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref clienteNoUsado));
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMockingPropiedadPrioridadTipo_ReturnsTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            // Se queremos setear as propiedades manualmente debese engadir o seguinte se non o seteo e ingorado
            loggerGeneralMock.SetupAllProperties();
            loggerGeneralMock.Setup(u => u.TipoLogger).Returns("warning");
            loggerGeneralMock.Setup(u => u.PrioridadLogger).Returns(10);
            
            loggerGeneralMock.Object.PrioridadLogger = 10;

            Assert.Equal("warning", loggerGeneralMock.Object.TipoLogger);
            Assert.Equal(10, loggerGeneralMock.Object.PrioridadLogger);

            // CALLBACKS
            int contador = 5;
            loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>()))
                .Returns(true)
                .Callback(() => contador++);

            loggerGeneralMock.Object.LogDatabase("drez"); //vaxidrez
            Assert.Equal(6, contador);
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_VerifyEjemplo()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();

            CuentaBancaria cuentaBancaria = new CuentaBancaria(loggerGeneralMock.Object);
            cuentaBancaria.Deposito(100);
            Assert.Equal(100, cuentaBancaria.GetBalance());

            // verifica cuantas veces el mock esta llamando al metodo .message
            loggerGeneralMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(3)); // Exactamente 3 veces con calquera string

            loggerGeneralMock.Verify(u => u.Message("visita vaxidrez.com"), Times.AtLeastOnce); // Ao menos unha vez con ese parametro

            // Setear co valor e obter o valor unha soa vez
            loggerGeneralMock.VerifySet(u => u.PrioridadLogger = 100, Times.Once);
            loggerGeneralMock.VerifyGet(u => u.PrioridadLogger, Times.Once);
        }
    }
}
