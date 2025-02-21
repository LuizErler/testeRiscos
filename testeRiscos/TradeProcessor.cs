using System.Globalization;
using TesteRiscosDomain.Entities;
using TesteRiscosDomain.Interface;

public class TradeProcessor
{
    private readonly ITrade _trade;

    public TradeProcessor(ITrade trade)
    {
        _trade = trade;
    }

    public async Task ProcessTrades()
    {
        Console.WriteLine("Digite a data de referência (MM/dd/yyyy):");
        DateTime referenceDate;
        CultureInfo culture = new CultureInfo("en-US");
        while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", culture, DateTimeStyles.None, out referenceDate))
        {
            Console.WriteLine("Formato inválido! Digite novamente no formato MM/dd/yyyy:");
        }

        Console.WriteLine("Digite o número de operações:");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Número inválido! Digite um número inteiro positivo:");
        }

        List<ITrade> trades = new List<ITrade>();

        Console.WriteLine($"Digite as {n} operações (valor setor dataPagamento - separados por espaço):");
        for (int i = 0; i < n; i++)
        {
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length != 3)
            {
                Console.WriteLine("Entrada inválida! Formato esperado: <valor> <setor> <dataPagamento>");
                i--;
                continue;
            }

            if (!double.TryParse(parts[0], NumberStyles.Any, culture, out double valor))
            {
                Console.WriteLine("Valor inválido! Digite novamente:");
                i--;
                continue;
            }

            string setor = parts[1];

            if (!DateTime.TryParseExact(parts[2], "MM/dd/yyyy", culture, DateTimeStyles.None, out DateTime dataPagamento))
            {
                Console.WriteLine("Data inválida! Digite novamente no formato MM/dd/yyyy:");
                i--;
                continue;
            }

            try
            {
                ITrade trade = new Trade(valor, setor, dataPagamento, new RiskEvaluator());
                trades.Add(trade);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                i--;
            }
        }

        Console.WriteLine("\nResumo das operações:");
        Console.WriteLine($"Data de referência: {referenceDate:MM/dd/yyyy}");
        foreach (var trade in trades)
        {
            var riskCategory = await trade.RiskCategoryAsync(referenceDate);
            if (!string.IsNullOrEmpty(riskCategory))
            {
                Console.WriteLine($"{riskCategory}");
            }
        }
    }
}
