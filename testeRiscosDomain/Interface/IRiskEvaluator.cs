using TesteRiscosDomain.Entities;

namespace TesteRiscosDomain.Interface
{
    public interface IRiskEvaluator
    {
        void AddRule(Func<Trade, DateTime, string?> rule);
        string Evaluate(Trade trade, DateTime referenceDate);
    }
}
