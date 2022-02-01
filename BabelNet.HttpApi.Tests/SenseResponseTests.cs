using System;
using FluentAssertions;
using NUnit.Framework;

namespace BabelNet.HttpApi.Tests;

[TestFixture]
public class SenseResponseTests
{
    private readonly Sense _sense;
    private readonly SenseResponse _item;

    public SenseResponseTests()
    {
        _sense = new ();
        _item = new()
        {
            Type = SenseType.BabelSense,
            Properties = _sense
        };
    }


    [Test]
    public void PropertiesShouldBeDelegatedToUnderlyingSense()
    {
        _sense.Lemma.Should().BeNull();
        _item.Lemma.Should().BeNull();

        _sense.Lemma = new object();

        _item.Lemma.Should().BeSameAs(_sense.Lemma);
    }

    [Test]
    public void ShouldBeCastableToSense()
    {
        var sense = (Sense)_item;
        sense.Should().BeSameAs(_item.Properties);
    }

    [Test]
    public void ShouldBeNotCastableToWordNetSense()
    {
        Assert.Throws<InvalidCastException>(() => _ = (WordNetSense) _item);
    }

    [Test]
    public void ShouldBeCastableToISense()
    {
        var sense = (ISense)_item;
        sense.Should().BeSameAs(_item);
    }

    [Test]
    public void ShouldNotBeCastableToIWordNetSense()
    {
        Assert.Throws<InvalidCastException>(() => _ = (IWordNetSense)_item);
    }

    [Test]
    public void ToSenseType_ShouldReturnPropertiesAsISense_WhenTSenseParamIsISense()
    {
        var sense = _item.ToSenseType<ISense>();
        sense.Should().BeSameAs(_item.Properties);
    }

    [Test]
    public void ToSenseType_ShouldThrowInvalidOperation_WhenTSenseParamIsIWordNetSense()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => _item.ToSenseType<IWordNetSense>());
        ex.Message.Should().Be($"Cannot convert to type {typeof(IWordNetSense)}; " +
                               $"check {nameof(SenseResponse.Type)} property of {typeof(SenseResponse)}");
    }
}