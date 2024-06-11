// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;

namespace Vortice.Mathematics.Tests;

[TestFixture(TestOf = typeof(Color4))]
public partial class Color4Tests
{
    [TestCase]
    public void DefaultChecks()
    {
        Color4 color = new();
        Assert.That(color.R, Is.EqualTo(0.0f));
        Assert.That(color.G, Is.EqualTo(0.0f));
        Assert.That(color.B, Is.EqualTo(0.0f));
        Assert.That(color.A, Is.EqualTo(0.0f));
    }

    [TestCase]
    public void CreationTests()
    {
        Color4 color = new(0.5f);
        Assert.That(color.R, Is.EqualTo(0.5f));
        Assert.That(color.G, Is.EqualTo(0.5f));
        Assert.That(color.B, Is.EqualTo(0.5f));
        Assert.That(color.A, Is.EqualTo(0.5f));

        color = new(1.0f, 0.0f, 1.0f, 1.0f);
        Assert.That(color.R, Is.EqualTo(1.0f));
        Assert.That(color.G, Is.EqualTo(0.0f));
        Assert.That(color.B, Is.EqualTo(1.0f));
        Assert.That(color.A, Is.EqualTo(1.0f));
    }

    [TestCase]
    public void EqualTests()
    {
        Color4 first = new(0.5f);
        Color4 second = new(0.5f);
        Assert.That(first, Is.EqualTo(second));
        Assert.That(first == second, Is.True);
    }

    [TestCase]
    public void NotEqualTests()
    {
        Color4 first = new(0.5f);
        Color4 second = new(0.9f);
        Assert.That(first, Is.Not.EqualTo(second));
        Assert.That(first != second, Is.True);
    }
}
