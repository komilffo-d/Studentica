using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Studentica.Database.Postgre.Converters
{
    public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
    {
        public DateTimeOffsetConverter()
            : base(
                d => d.ToUniversalTime(),
                d => d.ToLocalTime())
        {
        }
    }
}
