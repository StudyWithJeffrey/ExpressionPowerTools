﻿using System.Linq.Expressions;
using System.Text.Json;
using ExpressionPowerTools.Serialization.Serializers;

namespace ExpressionPowerTools.Serialization.Tests.TestHelpers
{
    public static class TestSerializer 
    {
        public static ExpressionSerializer ExpressionSerializer { get; } =
            new ExpressionSerializer();
        
        public static JsonElement GetSerializedFragment<TSerializer, TExpression>(
            TExpression expression,
            JsonSerializerOptions options = null)
            where TExpression : Expression
            where TSerializer : SerializableExpression
        {
            var json = JsonSerializer.Serialize(ExpressionSerializer.Serialize(expression, options) as TSerializer);
            return JsonDocument.Parse(json).RootElement;
        }        
    }
}
