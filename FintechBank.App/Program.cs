using FintechBank.App;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var conta = new ContaCorrenteService();
var emprestimo = new EmprestimoService();

Console.WriteLine("=== FintechBank ===");
Console.WriteLine($"Taxa: saldo R$ 6.000, cliente comum: R$ {conta.CalcularTaxaManutencao(6000.00m, false):N2}");
Console.WriteLine($"Taxa: saldo R$ 500, cliente especial: R$ {conta.CalcularTaxaManutencao(500.00m, true):N2}");
Console.WriteLine($"Taxa: saldo R$ 2.500, cliente comum: R$ {conta.CalcularTaxaManutencao(2500.00m, false):N2}");
Console.WriteLine($"Taxa: saldo R$ 300, cliente comum: R$ {conta.CalcularTaxaManutencao(300.00m, false):N2}");
Console.WriteLine($"Saque de R$ 1.200 (saldo R$ 1.000 + limite R$ 500): {conta.PodeRealizarSaque(1000.00m, 500.00m, 1200.00m)}");
Console.WriteLine($"Saque de R$ 1.600 (saldo R$ 1.000 + limite R$ 500): {conta.PodeRealizarSaque(1000.00m, 500.00m, 1600.00m)}");
Console.WriteLine($"Parcela de R$ 10.000 em 10x, score 800: R$ {emprestimo.CalcularValorParcela(10000.00m, 10, 800):N2}");
Console.WriteLine($"Parcela de R$ 10.000 em 10x, score 600: R$ {emprestimo.CalcularValorParcela(10000.00m, 10, 600):N2}");
Console.WriteLine($"Parcela de R$ 10.000 em 10x, score 400: R$ {emprestimo.CalcularValorParcela(10000.00m, 10, 400):N2}");
Console.WriteLine($"Renda R$ 5.000, parcela R$ 1.500, aprovado: {emprestimo.AprovarEmprestimo(5000.00m, 1500.00m)}");
Console.WriteLine($"Renda R$ 5.000, parcela R$ 1.600, aprovado: {emprestimo.AprovarEmprestimo(5000.00m, 1600.00m)}");
