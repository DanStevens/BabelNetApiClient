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
    public void Sense_ShouldBeSet()
    {
        _item.Sense.Should().BeSameAs(_wordNetSense);
        _item.Sense.Should().BeSameAs(_item.Properties);
    }

    [Test]
    public void Sense_ShouldBeTheSameAfterCastingToSenseResponse()
    {
        _item.Sense.Should().BeSameAs(_wordNetSense);
        var senseResponse = (SenseResponse)_item;
        senseResponse.Properties.Should().BeNull();
        senseResponse.Sense.Should().BeSameAs(_wordNetSense);
    }
}