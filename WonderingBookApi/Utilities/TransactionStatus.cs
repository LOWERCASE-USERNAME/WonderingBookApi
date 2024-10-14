using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Utilities
{
    public enum TransactionStatus
    {
        Pending,
        Success,
        Failed,
        Cancel,
        Expired
    }
}
