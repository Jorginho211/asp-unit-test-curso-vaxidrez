using Xunit;

namespace LibreriaVaxi
{
    public class OperacionXUnitTest
    {
        [Fact]
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
            Assert.Equal(119, resultado);
        }

        [Theory]
        [InlineData(3, false)]
        [InlineData(5, false)]
        [InlineData(7, false)]
        public void IsValorPar_InputNumeroImpar_ReturnFalse(int numeroImpar, bool expectedResult)
        {
            Operacion op = new();
            var resultado = op.IsValorPar(numeroImpar);
            Assert.Equal(expectedResult, resultado);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(20)]
        public void IsValorPar_InputNumeroPar_ReturnTrue(int numeroPar)
        {
            Operacion op = new();

            bool isPar = op.IsValorPar(numeroPar);
            Assert.True(isPar);
        }

        [Theory]
        [InlineData(2.2, 1.2)] // resultado debe ser 3.4
        [InlineData(2.23, 1.24)] // resultado debe ser 3.47
        public void SumarDecimal_InputDosNumeros_GetValorCorrect(double decimal1, double decimal2)
        {
            // 1. Arrange
            // inicializar las variables o componentes que ejecutan el test
            Operacion op = new();

            // 2. Act
            double resultado = op.SumarDecimal(decimal1, decimal2);

            // 3. Assert
            Assert.Equal(3.4, resultado, 0);
        }

        [Fact]
        public void GetListaNumerosImpares_InputMinimoMaximoIntervalos_ReturnsListaImpares()
        {
            Operacion op = new();
            List<int> numerosImparesEsperados = new() { 5, 7, 9 };

            List<int> resultados = op.GetListaNumerosImpares(5, 10);

            Assert.Equal(numerosImparesEsperados, resultados);
            Assert.Contains(5, resultados);
            Assert.NotEmpty(resultados);
            Assert.Equal(3, resultados.Count);
            Assert.DoesNotContain(100, resultados);
            Assert.Equal(resultados.OrderBy(u => u), resultados);
        }
    }
}
