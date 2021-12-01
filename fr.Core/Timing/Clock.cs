using System;

namespace fr.Core.Timing
{
    public static class Clock
    {
        private static IClockProvider _provider;

        public static IClockProvider Provider
        {
            get => _provider;
            set => _provider = value ?? throw new ApplicationException("Can not set Clock to null!");
        }

        public static DateTime Now => Provider.Now;

        public static DateTimeKind Kind => Provider.Kind;

        static Clock()
        {
            Provider = new UtcClockProvider();
        }

        public static DateTime? NormalizeNullable(DateTime? dateTime) => Provider.NormalizeNullable(dateTime);

        public static DateTime Normalize(DateTime dateTime) => Provider.Normalize(dateTime);

        public static object NormalizeObject(object @object) => Provider.NormalizeObject(@object);
    }
}
