using LibreriaVaxi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibreriaVaxiMSTest
{
    [TestClass]
    public class OperacionMSTest
    {
        [TestMethod]
        public void SumarNumeros_InputDosNumeros_GetValorCorrect()
        {
            // 1. Arrange
            // inicializar las variables o componentes que ejecutan el test
            Operacion op = new();
            int numero1 = 50;
            int numero2 = 69;

            // 2. Act
            int resultado = op.SumarNumeros(numero1, numero2);

            // 3. Assert
            Assert.AreEqual(119, resultado);
        }
    }
}
