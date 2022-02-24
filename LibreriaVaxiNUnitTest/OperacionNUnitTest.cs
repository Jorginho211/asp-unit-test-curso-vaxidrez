using NUnit.Framework;
using System.Collections.Generic;

namespace LibreriaVaxi
{
    [TestFixture]
    public class OperacionNUnitTest
    {
        [Test]
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

        [Test]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(7, ExpectedResult = false)]
        public bool IsValorPar_InputNumeroImpar_ReturnFalse(int numeroImpar)
        {
            Operacion op = new();

            return op.IsValorPar(numeroImpar);
        }

        [Test]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(20)]
        public void IsValorPar_InputNumeroPar_ReturnTrue(int numeroPar)
        {
            Operacion op = new();

            bool isPar = op.IsValorPar(numeroPar);
            Assert.IsTrue(isPar);
        }

        [Test]
        [TestCase(2.2, 1.2)] // resultado debe ser 3.4
        [TestCase(2.23, 1.24)] // resultado debe ser 3.47
        public void SumarDecimal_InputDosNumeros_GetValorCorrect(double decimal1, double decimal2)
        {
            // 1. Arrange
            // inicializar las variables o componentes que ejecutan el test
            Operacion op = new();

            // 2. Act
            double resultado = op.SumarDecimal(decimal1, decimal2);

            // 3. Assert
            Assert.AreEqual(3.4, resultado, 0.1);
        }

        [ Test]
        public void GetListaNumerosImpares_InputMinimoMaximoIntervalos_ReturnsListaImpares()
        {
            Operacion op = new();
            List<int> numerosImparesEsperados = new() { 5, 7, 9 };

            List<int> resultados = op.GetListaNumerosImpares(5, 10);

            Assert.That(resultados, Is.EquivalentTo(numerosImparesEsperados));
            Assert.AreEqual(numerosImparesEsperados, resultados);
            Assert.That(resultados, Does.Contain(5));
            Assert.Contains(5, resultados);
            Assert.That(resultados, Is.Not.Empty);
            Assert.That(resultados.Count, Is.EqualTo(3));

            Assert.That(resultados, Has.No.Member(100));

            Assert.That(resultados, Is.Ordered);
            Assert.That(resultados, Is.Unique);
        }
    }
}
