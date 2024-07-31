using System.Runtime.Serialization;

namespace Orders.Core.Entities
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Received")]
        Received,
        [EnumMember(Value = "Failed")]
        Failed
    }
}