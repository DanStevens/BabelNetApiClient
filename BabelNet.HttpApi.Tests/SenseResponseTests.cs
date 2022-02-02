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
    public void Sense_ShouldBeSet()
    {
        _item.Sense.Should().BeSameAs(_sense);
        _item.Sense.Should().BeSameAs(_item.Properties);
    }
}