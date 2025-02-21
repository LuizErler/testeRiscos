using TesteRiscosDomain.Interface;

namespace TesteRiscosDomain.Entities
{
    public class Trade : ITrade
    {
        public double Value { get; }
        public string ClientSector { get; }
        public DateTime NextPaymentDate { get; }
        private readonly IRiskEvaluator _riskEvaluator;

        public Trade()
        {
        }
        public Trade(double value, string clientSector, DateTime nextPaymentDate, IRiskEvaluator riskEvaluator)
        {
            if (value <= 0)
                throw new ArgumentException("O valor da operação deve ser maior que zero.");
            if (clientSector != "Public" && clientSector != "Private")
                throw new ArgumentException("O setor do cliente deve ser 'Public' ou 'Private'.");

            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDate;
            _riskEvaluator = riskEvaluator ?? throw new ArgumentNullException(nameof(riskEvaluator));
        }

        public async Task<string> RiskCategoryAsync(DateTime referenceDate)
        {
            return await Task.FromResult(_riskEvaluator.Evaluate(this, referenceDate));
        }

        public override string ToString()
        {
            return $"{Value:C} | {ClientSector} | {NextPaymentDate:MM/dd/yyyy}";
        }
    }
}
