
using System;

namespace fr.Core.Timing
{
    public class LocalClockProvider : IClockProvider
    {
        public DateTime Now => DateTime.Now;

        public DateTimeKind Kind => DateTimeKind.Local;

        public DateTime Normalize(DateTime dateTime) => dateTime.Kind switch
        {
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime, DateTimeKind.Local),
            DateTimeKind.Local => dateTime,
            DateTimeKind.Utc => dateTime.ToLocalTime(),
            _ => dateTime
        };

        public DateTime? NormalizeNullable(DateTime? dateTime) => !dateTime.HasValue
            ? null
            : dateTime.Value.Kind switch
            {
                DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Local),
                DateTimeKind.Local => dateTime,
                DateTimeKind.Utc => dateTime.Value.ToLocalTime(),
                _ => dateTime
            };

        public object NormalizeObject(object @object) => @object is DateTime d ? Normalize(d) : @object;
    }
}
