namespace TesteRiscosDomain.Interface
{
    public interface ITrade
    {
        double Value { get; }
        string ClientSector { get; }
        DateTime NextPaymentDate { get; }

        Task<string> RiskCategoryAsync(DateTime referenceDate);
    }
}
