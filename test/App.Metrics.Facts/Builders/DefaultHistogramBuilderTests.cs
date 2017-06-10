﻿// <copyright file="DefaultHistogramBuilderTests.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using App.Metrics.Abstractions.ReservoirSampling;
using App.Metrics.Facts.Fixtures;
using App.Metrics.Histogram.Abstractions;
using App.Metrics.ReservoirSampling.Uniform;
using FluentAssertions;
using Moq;
using Xunit;

namespace App.Metrics.Facts.Builders
{
    public class DefaultHistogramBuilderTests : IClassFixture<MetricCoreTestFixture>
    {
        private readonly IBuildHistogramMetrics _builder;

        public DefaultHistogramBuilderTests(MetricCoreTestFixture fixture) { _builder = fixture.Builder.Histogram; }

        [Fact]
        public void Can_build_with_reservoir()
        {
            var reservoirMock = new Mock<IReservoir>();
            reservoirMock.Setup(r => r.Update(It.IsAny<long>()));
            reservoirMock.Setup(r => r.GetSnapshot()).Returns(() => new UniformSnapshot(100, 100.0, new long[100]));
            reservoirMock.Setup(r => r.Reset());

            var histogram = _builder.Build(() => reservoirMock.Object);

            histogram.Should().NotBeNull();
        }
    }
}