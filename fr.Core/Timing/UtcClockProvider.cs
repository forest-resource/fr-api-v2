using System;

namespace fr.Core.Timing
{
    public class UtcClockProvider : IClockProvider
    {
        public DateTime Now => DateTime.UtcNow;

        public DateTimeKind Kind => DateTimeKind.Utc;

        public DateTime? NormalizeNullable(DateTime? dateTime) => !dateTime.HasValue
            ? null
            : dateTime.Value.Kind switch
            {
                DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc),
                DateTimeKind.Local => dateTime.Value.ToUniversalTime(),
                DateTimeKind.Utc => dateTime,
                _ => dateTime,
            };

        public DateTime Normalize(DateTime dateTime) => dateTime.Kind switch
        {
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc),
            DateTimeKind.Local => dateTime.ToUniversalTime(),
            DateTimeKind.Utc => dateTime,
            _ => dateTime,
        };

        public object NormalizeObject(object @object) => @object is DateTime d ? Normalize(d) : @object;
    }
}