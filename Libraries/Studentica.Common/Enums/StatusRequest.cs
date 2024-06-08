using Studentica.Common.Attributes;

namespace Studentica.Common.Enums
{
    public enum StatusRequest
    {
        [StringValue("В обработке")]
        INPROCESS,
        [StringValue("Отменена")]
        CANCEL,
        [StringValue("Принята")]
        ACCEPTED
    }
}
