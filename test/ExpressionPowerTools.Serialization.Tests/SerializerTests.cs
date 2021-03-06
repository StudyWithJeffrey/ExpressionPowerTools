﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using ExpressionPowerTools.Core.Dependencies;
using ExpressionPowerTools.Core.Extensions;
using ExpressionPowerTools.Core.Signatures;
using ExpressionPowerTools.Serialization.Extensions;
using ExpressionPowerTools.Serialization.Serializers;
using ExpressionPowerTools.Serialization.Signatures;
using ExpressionPowerTools.Serialization.Tests.TestHelpers;
using Xunit;

namespace ExpressionPowerTools.Serialization.Tests
{
    public class SerializerTests
    {
        private readonly Lazy<IRulesConfiguration> rulesConfig =
            ServiceHost.GetLazyService<IRulesConfiguration>();

        private void Reset()
        {
            ServiceHost.GetService<IMemberAdapter>().Reset();
        }

        public static TPropertyType Property<TPropertyType>(object entity, string name) =>
            entity == null ? default :
            (TPropertyType)entity.GetType().GetProperty(name).GetValue(entity);

        [Fact]
        public void GivenSerializeWhenCalledWithNullExpressionThenShouldThrowArgumentNull()
        {
            Expression expression = null;
            Assert.Throws<ArgumentNullException>(() =>
                Serializer.Serialize(expression));
        }

        [Fact]
        public void GivenSerializeWhenCalledWithNullQueryThenShouldThrowArgumentNull()
        {
            IQueryable query = null;
            Assert.Throws<ArgumentNullException>(() =>
                Serializer.Serialize(query));
        }

        public static IEnumerable<object[]> GetSerializers()
        {
            foreach (var type in typeof(IBaseSerializer).Assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface &&
                    typeof(IBaseSerializer).IsAssignableFrom(t)))
            {
                yield return new object[]
                {
                    Activator
                    .CreateInstance(
                        type,
                        new[] { (object)TestSerializer.ExpressionSerializer })
                    as IBaseSerializer
                };
            }
        }

        [Theory]
        [MemberData(nameof(GetSerializers))]
        public void GivenSerializerWhenSerializeCalledWithNullThenShouldReturnNull(IBaseSerializer serializer)
        {
            Assert.Null(serializer.Serialize(null, null));
        }

        [Fact]
        public void WhenDeserializeCalledWithEmptyJsonThenShouldReturnNull()
        {
            Assert.Null(Serializer.Deserialize("{}"));
        }

        [Fact]
        public void WhenDeserializeQueryCalledWithNullHostThenShouldThrowArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Serializer.DeserializeQuery(null, "{}"));
        }

        [Fact]
        public void WhenDeserializeQueryForTypeCalledWithNullHostThenShouldThrowArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Serializer.DeserializeQuery<TestableThing>("{}", null));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void WhenDeserializeQueryCalledWithEmptyOrNullJsonThenShouldThrowArgument(string json)
        {
            Assert.Throws<ArgumentException>(() =>
                Serializer.DeserializeQuery((IQueryable)TestableThing.MakeQuery(), json));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void WhenDeserializeQueryForTypeCalledWithEmptyOrNullJsonThenShouldThrowArgument(string json)
        {
            Assert.Throws<ArgumentException>(() =>
                Serializer.DeserializeQuery(TestableThing.MakeQuery(), json));
        }

        public enum Queries
        {
            Skip1Take1,
            OrderByCreatedThenByDescendingId,
            WhereIdContainsAA,
            CustomGenericProperty,
            IdProjection,
            IdAnonymousType,
            IdOnly,
            Filtered
        }

        public static IEnumerable<object[]> GetQueryMatrix()
        {
            yield return new object[]
            {
                TestableThing.MakeQuery().Skip(1).Take(1),
                Queries.Skip1Take1
            };

            yield return new object[]
            {
                TestableThing.MakeQuery().OrderBy(t => t.Created).ThenByDescending(t => t.Id),
                Queries.OrderByCreatedThenByDescendingId
            };

            yield return new object[]
            {
                TestableThing.MakeQuery().Where(t => t.Id.Contains("aa")),
                Queries.WhereIdContainsAA
            };

            yield return new object[]
            {
                TestableThing.MakeQuery().Where(t => Property<int>(t, nameof(TestableThing.Value)) > 500),
                Queries.CustomGenericProperty
            };

            yield return new object[]
            {
                TestableThing.MakeQuery().Select(t => t.Id),
                Queries.IdProjection
            };

            yield return new object[]
            {
                TestableThing.MakeQuery().Select(t => new { t.Id }),
                Queries.IdAnonymousType
            };

            yield return new object[]
            {
                TestableThing.MakeQuery().OrderBy(t => t.Id).Select(t => new TestableThing(t.Id)),
                Queries.IdOnly
            };

            yield return new object[]
            {
                TestableThing.MakeQuery().Where(t => t.Id.Contains("aa") && (t.IsActive || t.Value < int.MaxValue/2)),
                Queries.Filtered
            };
        }

        public static IEnumerable<object[]> GetTypedQueryMatrix()
        {
            foreach(object[] pair in GetQueryMatrix())
            {
                if ((Queries)pair[1] == Queries.IdProjection ||
                    (Queries)pair[1] == Queries.IdAnonymousType)
                {
                    continue;
                }

                yield return pair;
            }
        }

        public bool ValidateQuery(IList<TestableThing> list, Queries type)
        {
            switch(type)
            {
                case Queries.Skip1Take1:
                    return list.Count() == 1;
                case Queries.OrderByCreatedThenByDescendingId:
                    var ordered = list.OrderBy(t => t.Created).ThenByDescending(t => t.Id)
                        .ToList();
                    for (var idx = 0; idx < list.Count; idx += 1)
                    {
                        if (ordered[idx].Id != list[idx].Id)
                        {
                            return false;
                        }
                    }
                    return true;
                case Queries.WhereIdContainsAA:
                    return list.All(t => t.Id.Contains("aa"));
                case Queries.IdOnly:
                    return list.All(t => t.IsActive && t.Value == int.MinValue);
                case Queries.Filtered:
                    return list.All(t => t.Id.Contains("aa") && (t.IsActive || t.Value < int.MaxValue / 2));
            }
            return false;
        }

        [Theory]
        [MemberData(nameof(GetQueryMatrix))]
        public void GivenQueryWhenSerializeCalledThenShouldDeserialize(
            IQueryable query,
            Queries type)
        {            
            var json = Serializer.Serialize(query);

            // make sure we're not just pulling from the cache
            Reset();

            rulesConfig.Value.RuleForType<TestableThing>()
                .RuleForType<SerializerTests>();

            IQueryable queryHost = TestableThing.MakeQuery(100);

            var newQuery = Serializer.DeserializeQuery(queryHost, json);
            // can't do equivalency check for anonymous types
            if (!query.AsEnumerableExpression().OfType<NewExpression>().Any(t => t.Type.IsAnonymousType()))
            {
                Assert.True(query.IsEquivalentTo(newQuery));
            }
            var testRun = newQuery.AsObjectArray(); // ensure it runs
            Assert.NotNull(testRun);
            if (newQuery is IQueryable<TestableThing> thingQuery)
            {
                var list = thingQuery.ToList();
                ValidateQuery(list, type);
            }
        }

        [Theory]
        [MemberData(nameof(GetTypedQueryMatrix))]
        public void GivenQueryWhenSerializeCalledThenShouldDeserializeForType(
            IQueryable<TestableThing> query,
            Queries type)
        {
            var json = Serializer.Serialize(query);

            Reset();

            rulesConfig.Value.RuleForType<TestableThing>()
                .RuleForType<SerializerTests>();

            var queryHost = TestableThing.MakeQuery(100);
            var newQuery = Serializer.DeserializeQuery(json, queryHost);
            Assert.True(query.IsEquivalentTo(newQuery));
            ValidateQuery(newQuery.ToList(), type);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenDeserializeWhenCalledWithNullOrEmptyStringThenShouldThrowArgument(string json)
        {
            Assert.Throws<ArgumentException>(() =>
                Serializer.Deserialize(json));
        }

        public static IEnumerable<object[]> GetConstantExpressions =
            ConstantSerializerTests.GetConstantExpressions();

        [Theory]
        [MemberData(nameof(GetConstantExpressions))]
        public void GivenExpressionWhenSerializedThenShouldDeserialize(ConstantExpression constant)
        {
            var json = Serializer.Serialize(constant);

            Reset();

            var target = Serializer.Deserialize<ConstantExpression>(json);

            Assert.True(constant.IsEquivalentTo(target));
        }

        [Fact]
        public void GivenAnArrayWithExpressionsWhenSerializedThenShouldDeserialize()
        {
            var array = Expression.NewArrayInit(
                typeof(int),
                Expression.Constant(1),
                Expression.Constant(2),
                Expression.Constant(3));
            var json = Serializer.Serialize(array);

            Reset();

            var target = Serializer.Deserialize<NewArrayExpression>(json);
            Assert.NotNull(target);
            Assert.Equal(typeof(int[]), target.Type);
            Assert.Equal(
                array.Expressions.OfType<ConstantExpression>().Select(c => c.Value),
                target.Expressions.OfType<ConstantExpression>().Select(c => c.Value));
            Assert.True(array.IsEquivalentTo(target));
        }

        public static IEnumerable<object[]> GetParameterExpressions() =>
            ParameterSerializerTests.GetParameterExpressions();

        [Theory]
        [MemberData(nameof(GetParameterExpressions))]
        public void GivenParameterExpressionWhenSerializedThenShouldDeserialize(ParameterExpression parameter)
        {
            var json = Serializer.Serialize(parameter);

            Reset();

            var target = Serializer.Deserialize<ParameterExpression>(json);
            Assert.Equal(parameter.Type, target.Type);
            Assert.Equal(parameter.Name, target.Name);
        }

        public static IEnumerable<object[]> GetUnaryExpressions() =>
            UnarySerializerTests.GetUnaryExpressions();

        [Theory]
        [MemberData(nameof(GetUnaryExpressions))]
        public void GivenUnaryExpressionWhenSerializedThenShouldDeserialize(UnaryExpression unary)
        {
            var json = Serializer.Serialize(unary);

            Reset();

            var target = Serializer.Deserialize<UnaryExpression>(json);
            Assert.Equal(unary.Type, target.Type);
            Assert.Equal(unary.Operand?.NodeType, target.Operand?.NodeType);

            if (unary.Method != null)
            {
                Assert.Equal(unary.Method, target.Method);
            }

            Assert.True(unary.IsEquivalentTo(target));
        }

        public static IEnumerable<object[]> GetLambdaExpressions() =>
            LambdaSerializerTests.GetLambdaExpressions();

        [Theory]
        [MemberData(nameof(GetLambdaExpressions))]
        public void GivenLambdaExpressionWhenSerializedThenShouldDeserialize(LambdaExpression lambda)
        {
            var json = Serializer.Serialize(lambda);

            Reset();

            var target = Serializer.Deserialize<LambdaExpression>(json);
            Assert.True(lambda.IsEquivalentTo(target));
        }

        public static IEnumerable<object[]> GetInvocationExpressions() =>
            InvocationSerializerTests.GetInvocationExpressionMatrix();

        [Theory]
        [MemberData(nameof(GetInvocationExpressions))]
        public void GivenInvocationExpressionWhenSerializedThenShouldDeserialize(InvocationExpression invocation)
        {
            var json = Serializer.Serialize(invocation);

            Reset();

            var target = Serializer.Deserialize<InvocationExpression>(json);
            Assert.Equal(invocation.Type, target.Type);
            Assert.True(invocation.IsEquivalentTo(target));
        }

        public static IEnumerable<object[]> GetMethodCallExpressions() =>
            MethodSerializerTests.GetMethodCallMatrix();

        [Theory]
        [MemberData(nameof(GetMethodCallExpressions))]
        public void GivenMethodCallExpressionWhenSerializedThenShouldDeserialize(
            MethodCallExpression method)
        {
            var json = Serializer.Serialize(method);

            Reset();

            var target = Serializer.Deserialize<MethodCallExpression>(json);
            Assert.True(method.IsEquivalentTo(target));
        }

        public static IEnumerable<object[]> GetMemberMatrix() =>
            MemberSerializerTests.GetMemberMatrix();

        [Theory]
        [MemberData(nameof(GetMemberMatrix))]
        public void GivenMemberExpressionWhenSerializedThenShouldDeserialize(
            MemberExpression member)
        {
            var json = Serializer.Serialize(member);

            Reset();

            var target = Serializer.Deserialize<MemberExpression>(json);
            Assert.True(member.IsEquivalentTo(target));
        }

        public static IEnumerable<object[]> GetNewMatrix() =>
            CtorSerializerTests.GetCtorMatrix();

        [Theory]
        [MemberData(nameof(GetNewMatrix))]
        public void GivenNewExpressionWhenSerializedThenShouldDeserialize(
            ConstructorInfo info,
            Expression[] args,
            MemberInfo[] members)
        {
            var ctor = CtorSerializerTests.MakeNew(info, args, members);
            var json = Serializer.Serialize(ctor);

            Reset();

            rulesConfig.Value.RuleForConstructor(selector => selector.ByMemberInfo(info));
            var target = Serializer.Deserialize<NewExpression>(json);
            if (info.DeclaringType.IsAnonymousType())
            {
                Assert.Equal(typeof(AnonInitializer), target.Type);
            }
            else
            {
                Assert.True(ctor.IsEquivalentTo(target));
            }
        }

        public static IEnumerable<object[]> GetBinaryExpressions =
            BinarySerializerTests.GetBinaryExpressions();

        [Theory]
        [MemberData(nameof(GetBinaryExpressions))]
        public void GivenBinaryExpressionWhenSerializedThenShouldDeserialize(
            BinaryExpression binary)
        {
            if (binary.Method != null)
            {
                rulesConfig.Value.RuleForMethod(selector => selector.ByMemberInfo(binary.Method));
            }
            var json = Serializer.Serialize(binary);

            Reset();

            var target = Serializer.Deserialize<BinaryExpression>(json);
            Assert.True(binary.IsEquivalentTo(target));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenDefaultConfigurationWhenNoConfigProvidedThenShouldUseDefault(bool configOverride)
        {
            var query = TestableThing.MakeQuery().Where(t => t.Id.Contains("aa") && (t.IsActive || t.Value < int.MaxValue / 2));

            Serializer.ConfigureDefaults(config => config.WithJsonSerializerOptions(
                new JsonSerializerOptions
                {
                    IgnoreNullValues = false,
                    IgnoreReadOnlyProperties = true
                }).Configure());

            if (configOverride)
            {
                var json = Serializer.Serialize(query, config => config.CompressTypes(false).Configure());
                Assert.DoesNotContain("null", json);
                Assert.DoesNotContain("^", json);

                Reset();

                var expr = Serializer.DeserializeQuery<TestableThing>(json, config: config => config.CompressTypes(false).Configure());
                Assert.True(query.IsEquivalentTo(expr));
            }
            else
            {
                var json = Serializer.Serialize(query);
                Assert.Contains("null", json);
                // Assert.Contains("^", json);
            }

            Serializer.ConfigureDefaults(config => config.Configure());
        }

        [Fact]
        public void GivenRulesConfigThenReplacesExistingRules()
        {
            var method = GetType().GetMethod(nameof(GivenRulesConfigThenReplacesExistingRules));
            var expr = Expression.Call(this.AsConstantExpression(), method);
            var json = Serializer.Serialize(expr);

            Reset();

            Serializer.ConfigureRules(
                rules => rules.RuleForMethod(selector => selector.ByResolver<MethodInfo, SerializerTests>(
                test => test.GivenRulesConfigThenReplacesExistingRules())));
            var target = Serializer.Deserialize<MethodCallExpression>(json);
            Assert.NotNull(target);
            Serializer.ConfigureRules();
            Assert.Throws<UnauthorizedAccessException>(() =>
                Serializer.Deserialize<MethodCallExpression>(json));
        }

        [Fact]
        public void GivenConfigureRulesThenShouldConfigureDefaults()
        {
            Expression<Func<string, bool>> expr = str => str.Contains("aa");
            var json = Serializer.Serialize(expr);

            Reset();

            Serializer.ConfigureRules();
            var target = Serializer.Deserialize<LambdaExpression>(json);
            Assert.NotNull(target);
        }

        [Fact]
        public void GivenConfigureRulesNoDefaultsThenShouldConfigureNoDefaultRule()
        {
            Expression<Func<string, bool>> expr = str => str.Contains("aa");
            var json = Serializer.Serialize(expr);

            Reset();

            Serializer.ConfigureRules(noDefaults: true);
            Assert.Throws<UnauthorizedAccessException>(
                () => Serializer.Deserialize<LambdaExpression>(json));
            Serializer.ConfigureRules();

        }
    }
}
