using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace fr.Core.Expressions
{
    public static class ExpressionBuilder
    {
        private static readonly Dictionary<string, List<string>> OperationList = new()
        {
            { ExpressionConstants.String, new List<string> { "eq", "ne", "st", "ct", "ew" } },
            { ExpressionConstants.Number, new List<string> { "eq", "ne", "lt", "le", "gt", "ge" } },
            { ExpressionConstants.Boolean, new List<string> { "eq", "ne" } },
            { ExpressionConstants.Id, new List<string> { "eq", "ne" } }
        };

        private static Expression Bind(Expression left, Expression right)
            => left == null ? right : Expression.And(left, right);

        public static Expression<Func<TEntity, bool>> Build<TEntity>(this ExpressionRule rules) where TEntity : class
        {
            Expression left = null;

            var properties = typeof(TEntity).GetProperties()
                                            .Join(rules.Where(r => r.Value != null), r => r.Name, r => r.Key, (p, _) => p);

            var parameter = Expression.Parameter(typeof(TEntity));

            foreach (var prop in properties)
            {
                var rule = rules.First(r => r.Key.ToLower() == prop.Name.ToLower());
                var propCode = prop.PropertyType.GetNumericOrOther();
                var @operation = OperationList[propCode].FirstOrDefault(r =>
                {
                    var code = $"{r} ".ToLower();
                    return rule.Value.ToLower().StartsWith(code);
                });

                object value = string.Empty;
                if (string.IsNullOrEmpty(@operation))
                {
                    @operation = "eq";
                    value = rule.Value;
                }
                else
                {
                    value = rule.Value.Remove(0, 3);
                }

                if (!string.Equals(propCode, ExpressionConstants.String))
                {
                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);
                    value = converter.ConvertFrom(value.ToString());
                }

                var property = Expression.Property(parameter, prop.Name);
                var right = GetExpression(@operation, property, Expression.Constant(value));

                left = Bind(left, right);

                Console.WriteLine($"{prop.Name}: '{@operation}'; value: {value}");
                Console.WriteLine($"Selector: {right?.ToString() ?? "null" }");
            }

            var expression = Expression.Lambda<Func<TEntity, bool>>(left, parameter);

            Console.WriteLine($"Expression: {expression?.ToString() ?? "null"}");

            return expression;
        }

        private static string GetNumericOrOther(this Type type) => Type.GetTypeCode(type) switch
        {
            TypeCode.Byte => ExpressionConstants.Number,
            TypeCode.SByte => ExpressionConstants.Number,
            TypeCode.UInt16 => ExpressionConstants.Number,
            TypeCode.UInt32 => ExpressionConstants.Number,
            TypeCode.UInt64 => ExpressionConstants.Number,
            TypeCode.Int16 => ExpressionConstants.Number,
            TypeCode.Int32 => ExpressionConstants.Number,
            TypeCode.Int64 => ExpressionConstants.Number,
            TypeCode.Decimal => ExpressionConstants.Number,
            TypeCode.Double => ExpressionConstants.Number,
            TypeCode.Single => ExpressionConstants.Number,
            TypeCode.String => ExpressionConstants.String,
            TypeCode.Boolean => ExpressionConstants.Boolean,
            _ when type.Name == nameof(Guid) => ExpressionConstants.Id,
            _ => throw new ArgumentOutOfRangeException(type.FullName)
        };

        private static Expression GetExpression(string @operator, Expression property, Expression toCompare) => @operator switch
        {
            "eq" => Expression.Equal(property, toCompare),
            "ne" => Expression.NotEqual(property, toCompare),
            "st" => Expression.Call(property.ToLower(), ExpressionConstants.MethodStringStartsWith, toCompare.ToLower()),
            "ct" => Expression.Call(property.ToLower(), ExpressionConstants.MethodStringContains, toCompare.ToLower()),
            "ew" => Expression.Call(property.ToLower(), ExpressionConstants.MethodStringEndsWith, toCompare.ToLower()),
            "lt" => Expression.LessThan(property, toCompare),
            "le" => Expression.LessThanOrEqual(property, toCompare),
            "gt" => Expression.GreaterThan(property, toCompare),
            "ge" => Expression.GreaterThanOrEqual(property, toCompare),
            _ => throw new ArgumentOutOfRangeException(@operator)
        };

        private static Expression ToLower(this Expression prop)
            => Expression.Call(prop, "ToLower", Array.Empty<Type>());
    }
}