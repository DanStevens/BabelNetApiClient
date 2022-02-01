using System;
using FluentAssertions;
using NUnit.Framework;

namespace BabelNet.HttpApi.Tests;

[TestFixture]
public class WordNetSenseResponseTests
{
    private readonly WordNetSense _wordNetSense;
    private readonly WordNetSenseResponse _item;

    public WordNetSenseResponseTests()
    {
        _wordNetSense = new();
        _item = new(_wordNetSense);
    }

    [Test]
    public void Type_ShouldBeWordNetSense()
    {
        _item.Type.Should().Be(SenseType.WordNetSense);
    }

    [Test]
    public void PropertiesShouldBeDelegatedToUnderlyingSense()
    {
        _wordNetSense.Lemma.Should().BeNull();
        _item.Lemma.Should().BeNull();
        _wordNetSense.Lemma = new object();
        _item.Lemma.Should().BeSameAs(_wordNetSense.Lemma);

        _wordNetSense.WordNetOffset.Should().BeNull();
        _item.WordNetOffset.Should().BeNull();
        _wordNetSense.WordNetOffset = "ABC123";
        _item.WordNetOffset.Should().BeSameAs(_wordNetSense.WordNetOffset);
    }

    [Test]
    public void ShouldBeCastableToSense()
    {
        var sense = (Sense)_item;
        sense.Should().BeSameAs(_item.Sense);
    }

    [Test]
    public void ShouldBeCastableToWordNetSense()
    {
        var sense = (WordNetSense)_item;
        sense.Should().BeSameAs(_item.Sense);
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
        var sense = (IWordNetSense)_item;
        sense.Should().BeSameAs(_item);
    }

    [Test]
    public void ToSenseType_ShouldReturnPropertiesAsISense_WhenTSenseParamIsISense()
    {
        var sense = _item.ToSenseType<ISense>();
        sense.Should().NotBeNull();
        sense.Should().BeSameAs(_wordNetSense);
    }

    [Test]
    public void ToSenseType_ShouldThrowInvalidOperation_WhenTSenseParamIsIWordNetSense()
    {
        var sense = _item.ToSenseType<IWordNetSense>();
        sense.Should().NotBeNull();
        sense.Should().BeSameAs(_wordNetSense);
    }
}