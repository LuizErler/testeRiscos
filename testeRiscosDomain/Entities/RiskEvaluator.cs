using TesteRiscosDomain.Interface;

namespace TesteRiscosDomain.Entities
{
    public class RiskEvaluator : IRiskEvaluator
    {
        private readonly List<Func<Trade, DateTime, string?>> _rules = new();

        public RiskEvaluator()
        {
            _rules.Add((trade, referenceDate) =>
                trade.NextPaymentDate < referenceDate.AddDays(-30) ? "EXPIRED" : null);

            _rules.Add((trade, referenceDate) =>
                trade.Value > 1000000 && trade.ClientSector == "Private" ? "HIGHRISK" : null);

            _rules.Add((trade, referenceDate) =>
                trade.Value > 1000000 && trade.ClientSector == "Public" ? "MEDIUMRISK" : null);

        }

        public void AddRule(Func<Trade, DateTime, string?> rule)
        {
            _rules.Add(rule);
        }

        public string Evaluate(Trade trade, DateTime referenceDate)
        {
            return _rules.Select(rule => rule(trade, referenceDate))
                         .FirstOrDefault(result => result != null) ?? "NO RULE";
        }
    }
}
