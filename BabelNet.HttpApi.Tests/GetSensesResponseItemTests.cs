using System;
using FluentAssertions;
using NUnit.Framework;

namespace BabelNet.HttpApi.Tests;

[TestFixture]
public class GetSensesResponseItemTests
{
    [Test]
    public void BabelSenseResponseItem_ShouldBeCastableToSense()
    {
        var item = new GetSensesResponseItem
        {
            Type = SenseType.BabelSense,
            Properties = new Sense()
        };
        var sense = (Sense)item;
        sense.Should().BeSameAs(item.Properties);
    }

    [Test]
    public void BabelSenseResponseItem_ShouldBeNotCastableToWordNetSense()
    {
        var item = new GetSensesResponseItem
        {
            Type = SenseType.BabelSense,
            Properties = new Sense()
        };
        Assert.Throws<InvalidCastException>(() => _ = (WordNetSense) item);
    }

    [Test]
    public void WordNetSenseResponseItem_ShouldBeCastableToSense()
    {
        var item = new GetSensesResponseItem
        {
            Type = SenseType.WordNetSense,
            Properties = new WordNetSense()
        };
        var sense = (Sense)item;
        sense.Should().BeSameAs(item.Properties);
    }

    [Test]
    public void WordNetSenseResponseItem_ShouldBeCastableToWordNetSense()
    {
        var item = new GetSensesResponseItem
        {
            Type = SenseType.WordNetSense,
            Properties = new WordNetSense()
        };
        var sense = (WordNetSense)item;
        sense.Should().BeSameAs(item.Properties);
    }

    [Test]
    public void ToSenseType_ShouldReturnPropertiesAsISense_WhenResponseItemIsTypeBabelSenseAndTSenseParamIsISense()
    {
        var item = new GetSensesResponseItem
        {
            Type = SenseType.BabelSense,
            Properties = new Sense(),
        };
        var sense = item.ToSenseType<ISense>();
        sense.Should().BeSameAs(item.Properties);
    }

    [Test]
    public void ToSenseType_ShouldReturnPropertiesAsIWordNetSense_WhenResponseItemIsTypeWordNetSenseAndTSenseParamIsIWordNetSense()
    {
        var item = new GetSensesResponseItem
        {
            Type = SenseType.WordNetSense,
            Properties = new WordNetSense(),
        };
        var wnSense = item.ToSenseType<IWordNetSense>();
        wnSense.Should().BeSameAs(item.Properties);
    }

    [Test]
    public void ToSenseType_ShouldThrowInvalidOperation_WhenResponseItemIsTypeBabelSenseAndTSenseParamIsIWordNetSense()
    {
        var item = new GetSensesResponseItem
        {
            Type = SenseType.BabelSense,
            Properties = new Sense(),
        };
        var ex = Assert.Throws<InvalidOperationException>(() => item.ToSenseType<IWordNetSense>());
        ex.Message.Should().Be($"Cannot convert to type {typeof(IWordNetSense)}; " +
                               $"check {nameof(GetSensesResponseItem.Type)} property of {typeof(GetSensesResponseItem)}");
    }
}