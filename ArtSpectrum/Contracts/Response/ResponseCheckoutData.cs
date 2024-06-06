namespace ArtSpectrum.Contracts.Response
{
    public class ResponseCheckoutData
    {
        string id { get; set; }
        string orderCode { get; set; }
        int amount { get; set; }
        int amountPaid { get; set; }
        int amountRemaining { get; set; }

        string status { get; set; }

        DateTime createdAt { get; set; }

        Transaction transactions { get; set; }

        string cancellationReason { get; set; }
        string canceledAt { get; set; }
    }

    public class Transaction
    {
        string reference { get; set; }
        int amount { get; set; }
        string accountNumber { get; set; }
        string description { get; set; }
        DateTime transactionDateTime { get; set; }
        string virtualAccountName { get; set; }
        string virtualAccountNumber { get; set; }
        string counterAccountBankId { get; set; }
        string counterAccountBankName { get; set; }
        string counterAccountName { get; set; }
        string counterAccountNumber { get; set; }

    }
}
