using System;
using System.Linq;
using System.Collections.Generic;
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
        _item = new(_sense);
    }

    [Test]
    public void Type_ShouldBeBabelSense()
    {
        _item.Type.Should().Be(SenseType.BabelSense);
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
        sense.Should().BeSameAs(_item.Sense);
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
        sense.Should().BeSameAs(_item.Sense);
    }

    [Test]
    public void ToSenseType_ShouldThrowInvalidOperation_WhenTSenseParamIsIWordNetSense()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => _item.ToSenseType<IWordNetSense>());
        ex.Message.Should().Be($"Cannot convert to type {typeof(IWordNetSense)}; " +
                               $"check {nameof(SenseResponse.Type)} property of {typeof(SenseResponse)}");
    }

    [Test]
    public void Cast_ShouldWork()
    {
        ICollection<ISense> senses = new List<SenseResponse>
        {
            new WordNetSenseResponse(new WordNetSense()),
            new WordNetSenseResponse(new WordNetSense()),
            new WordNetSenseResponse(new WordNetSense()),
        }.Cast<ISense>().ToList();

        IEnumerable<IWordNetSense> wnSenses = senses.Cast<IWordNetSense>();
        wnSenses.Should().NotBeNull();
        wnSenses.Count().Should().Be(senses.Count);
    }

    [Test]
    public void OfType_ShouldWork()
    {
        ICollection<ISense> senses = new List<SenseResponse>
        {
            new WordNetSenseResponse(new WordNetSense()),
            new WordNetSenseResponse(new WordNetSense()),
            new WordNetSenseResponse(new WordNetSense()),
            new SenseResponse(new Sense()),
            new SenseResponse(new Sense())
        }.Cast<ISense>().ToList();

        IEnumerable<IWordNetSense> wnSenses = senses.OfType<IWordNetSense>();
        wnSenses.Should().NotBeNull();
        wnSenses.Count().Should().Be(3);
    }
}