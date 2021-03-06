﻿using System;
using System.Text.Json;
using ExpressionPowerTools.Serialization.Configuration;
using ExpressionPowerTools.Serialization.Signatures;
using Xunit;

namespace ExpressionPowerTools.Serialization.Tests
{
    public class ConfigurationBuilderTests
    {
        [Fact]
        public void GivenConfigurationBuilderWhenConfigureCalledThenShouldSetDefaultOptions()
        {
            var state = new ConfigurationBuilder().Configure();
            Assert.True(state.CompressTypes);
            Assert.NotNull(state.Options);
            Assert.True(state.Options.IgnoreNullValues);
            Assert.True(state.Options.IgnoreReadOnlyProperties);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenConfigurationBuilderWhenCompressTypesCalledThenShouldSetFlag(bool setting)
        {
            var state = new ConfigurationBuilder().CompressTypes(setting).Configure();
            Assert.Equal(setting, state.CompressTypes);
        }

        [Fact]
        public void GivenConfigurationBuilderWhenJsonOptionsSetThenShouldConfigureOptions()
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = false
            };
            var state = new ConfigurationBuilder().WithJsonSerializerOptions(options).Configure();
            Assert.Same(options, state.Options);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenConfigurationBuilderThenOptionsCanBeChainedConfigurable(bool setting)
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = false
            };

            var state = new ConfigurationBuilder()
                .WithJsonSerializerOptions(options)
                .CompressTypes(setting)
                .Configure();

            Assert.Same(options, state.Options);
            Assert.Equal(setting, state.CompressTypes);
        }

        [Fact]
        public void GivenConfigureCalledWhenConfiguringThenShouldThrowInvalidOperation()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            var state = builder.Configure();
            Assert.Throws<InvalidOperationException>(
                () => builder.CompressTypes(true));
        }
    }
}
