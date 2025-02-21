using Microsoft.Extensions.DependencyInjection;
using TesteRiscosDomain.Entities;
using TesteRiscosDomain.Interface;

class Program
{
    static async Task Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<ITrade>(provider =>
            {
                return new Trade();
            })
            .AddScoped<TradeProcessor>()
            .BuildServiceProvider();

        var tradeProcessor = serviceProvider.GetRequiredService<TradeProcessor>();

        await tradeProcessor.ProcessTrades();
    }
}
