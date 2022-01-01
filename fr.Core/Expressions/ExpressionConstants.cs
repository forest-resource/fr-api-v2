using System.Reflection;

namespace fr.Core.Expressions
{
    public static class ExpressionConstants
    {
        public static MethodInfo MethodStringContains
            = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
        public static MethodInfo MethodStringStartsWith
            = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
        public static MethodInfo MethodStringEndsWith
            = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });

        public static string Number { get => nameof(Number).ToLower(); }
        public static string String { get => nameof(String).ToLower(); }
        public static string Boolean { get => nameof(Boolean).ToLower(); }
        public static string Id { get => nameof(Id).ToLower(); }
    }
}
