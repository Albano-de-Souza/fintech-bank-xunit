# fintech-bank-xunit

## Visão geral

O FintechBank é uma aplicação de console em .NET 10 que reúne regras de negócio de um banco digital, divididas em duas classes de serviço: `ContaCorrenteService` (taxa de manutenção e validação de saque) e `EmprestimoService` (cálculo de parcelas com juros por score de crédito e aprovação de empréstimo). As regras são validadas por testes unitários parametrizados em xUnit. Atividade da disciplina Gestão e Qualidade de Software.

## Estrutura

- `FintechBank.App`: código de produção (classes `ContaCorrenteService` e `EmprestimoService`)
- `FintechBank.Tests`: testes unitários (classes `ContaCorrenteServiceTests` e `EmprestimoServiceTests`)

## Requisitos técnicos

- .NET 10 SDK (projetos com `TargetFramework` `net10.0`)
- Git
- xUnit (restaurado automaticamente pelo `dotnet`)

## Regras de negócio

### ContaCorrenteService

| Método | Retorno | Regra |
|---|---|---|
| `CalcularTaxaManutencao(decimal saldo, bool clienteEspecial)` | `decimal` | Isento (`0.00`) se o saldo for maior ou igual a R$ 5.000,00 ou se o cliente for especial. Não especial com saldo entre R$ 1.000,00 e R$ 4.999,99: `12.50`. Não especial com saldo menor que R$ 1.000,00: `25.00` |
| `PodeRealizarSaque(decimal saldo, decimal limiteChequeEspecial, decimal valorSaque)` | `bool` | `true` apenas se o valor do saque for menor ou igual a saldo + limite do cheque especial |

### EmprestimoService

| Método | Retorno | Regra |
|---|---|---|
| `CalcularValorParcela(decimal valorTotal, int parcelas, int scoreCredito)` | `decimal` | Juros totais de 5% para score maior ou igual a 700, 12% para score entre 500 e 699 e 20% para score menor que 500. Retorna o valor com juros dividido pelo número de parcelas |
| `AprovarEmprestimo(decimal rendaMensal, decimal valorParcela)` | `bool` | `true` apenas se a parcela comprometer no máximo 30% da renda mensal |

## Como clonar e compilar a solução

```bash
git clone https://github.com/Albano-de-Souza/fintech-bank-xunit.git
cd fintech-bank-xunit
dotnet build
```

Resultado esperado: `Construir êxito` (ou `Build succeeded`).

Para rodar a aplicação, que imprime exemplos de cada regra:

```bash
dotnet run --project FintechBank.App
```

## Como executar os testes unitários

Na pasta raiz do repositório:

```bash
dotnet test
```

Resultado esperado: `total: 11; falhou: 0; bem-sucedido: 11`.

## Testes parametrizados com [Theory] e [InlineData]

Os testes usam `[Theory]` com `[InlineData]`: cada método de teste recebe parâmetros e é executado uma vez para cada linha de dados, e cada `[InlineData]` aparece no resultado como um teste individual. São 4 métodos de teste e 11 cenários.

Como atributos em C# não aceitam valores `decimal`, os valores monetários são informados no `[InlineData]` como `double` e convertidos para `decimal` dentro de cada teste antes da chamada ao serviço.

### ContaCorrenteServiceTests

| Teste | Dados de entrada | Resultado esperado | Cenário |
|---|---|---|---|
| Taxa de manutenção | `6000.00`, `false` | `0.00` | Saldo alto, isento |
| Taxa de manutenção | `500.00`, `true` | `0.00` | Cliente especial, isento |
| Taxa de manutenção | `2500.00`, `false` | `12.50` | Saldo intermediário |
| Taxa de manutenção | `300.00`, `false` | `25.00` | Saldo baixo |
| Validação de saque | `1000.00`, `500.00`, `1200.00` | `true` | Dentro do limite total |
| Validação de saque | `1000.00`, `500.00`, `1600.00` | `false` | Excede saldo + limite |

### EmprestimoServiceTests

| Teste | Dados de entrada | Resultado esperado | Cenário |
|---|---|---|---|
| Cálculo de parcela | `10000.00`, `10`, `800` | `1050.00` | Score alto, 5% de juros |
| Cálculo de parcela | `10000.00`, `10`, `600` | `1120.00` | Score médio, 12% de juros |
| Cálculo de parcela | `10000.00`, `10`, `400` | `1200.00` | Score baixo, 20% de juros |
| Aprovação de empréstimo | `5000.00`, `1500.00` | `true` | Parcela igual a 30% da renda |
| Aprovação de empréstimo | `5000.00`, `1600.00` | `false` | Parcela acima de 30% da renda |

## Equipe

| Papel | Responsável | Entregas |
|---|---|---|
| Desenvolvedor 1 (Backend / Core) | Albano de Souza | Solução, projetos, `ContaCorrenteService` e `EmprestimoService` |
| Desenvolvedor 2 (QA / Testes) | Alice Fernandes Barbosa | `ContaCorrenteServiceTests` com `[Theory]` e `[InlineData]` |
| Desenvolvedor 2 (QA / Testes) | Débora Gomes | `EmprestimoServiceTests` com `[Theory]` e `[InlineData]` |
| Desenvolvedor 3 (Documentação / DevOps) | Ana Carolina de Sousa Freitas | `.gitignore`, licença MIT e `README.md` |

## Licença

MIT
