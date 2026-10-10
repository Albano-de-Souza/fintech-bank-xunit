using FintechBank.App;

namespace FintechBank.Tests;

public class EmprestimoServiceTests
{
    private readonly EmprestimoService _service = new EmprestimoService();

    // O [InlineData] não aceita valores decimal; os valores chegam como double
    // e são convertidos para decimal dentro do teste.
    [Theory]
    [InlineData(10000.00, 10, 800, 1050.00)]
    [InlineData(10000.00, 10, 600, 1120.00)]
    [InlineData(10000.00, 10, 400, 1200.00)]
    public void CalcularValorParcela_DeveAplicarJurosPorScore(double valorTotal, int parcelas, int scoreCredito, double parcelaEsperada)
    {
        var resultado = _service.CalcularValorParcela((decimal)valorTotal, parcelas, scoreCredito);

        Assert.Equal((decimal)parcelaEsperada, resultado);
    }

    [Theory]
    [InlineData(5000.00, 1500.00, true)]
    [InlineData(5000.00, 1600.00, false)]
    public void AprovarEmprestimo_DeveRespeitarMargemDe30PorCento(double rendaMensal, double valorParcela, bool esperado)
    {
        var resultado = _service.AprovarEmprestimo((decimal)rendaMensal, (decimal)valorParcela);

        Assert.Equal(esperado, resultado);
    }
}
