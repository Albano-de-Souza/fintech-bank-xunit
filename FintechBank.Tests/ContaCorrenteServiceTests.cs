using FintechBank.App;

namespace FintechBank.Tests;

public class ContaCorrenteServiceTests
{
    private readonly ContaCorrenteService _service = new ContaCorrenteService();

    // O [InlineData] não aceita valores decimal; os valores chegam como double
    // e são convertidos para decimal dentro do teste.
    [Theory]
    [InlineData(6000.00, false, 0.00)]
    [InlineData(500.00, true, 0.00)]
    [InlineData(2500.00, false, 12.50)]
    [InlineData(300.00, false, 25.00)]
    public void CalcularTaxaManutencao_DeveRetornarTaxaCorreta(double saldo, bool clienteEspecial, double taxaEsperada)
    {
        var resultado = _service.CalcularTaxaManutencao((decimal)saldo, clienteEspecial);

        Assert.Equal((decimal)taxaEsperada, resultado);
    }

    [Theory]
    [InlineData(1000.00, 500.00, 1200.00, true)]
    [InlineData(1000.00, 500.00, 1600.00, false)]
    public void PodeRealizarSaque_DeveValidarSaldoMaisLimite(double saldo, double limiteChequeEspecial, double valorSaque, bool esperado)
    {
        var resultado = _service.PodeRealizarSaque((decimal)saldo, (decimal)limiteChequeEspecial, (decimal)valorSaque);

        Assert.Equal(esperado, resultado);
    }
}
