// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;

namespace Vortice.Mathematics.Tests;

[TestFixture(TestOf = typeof(Color))]
public partial class ColorTests
{
    [TestCase]
    public void DefaultChecks()
    {
        Color color = new Color();
        Assert.That(color.R, Is.EqualTo(0));
        Assert.That(color.G, Is.EqualTo(0));
        Assert.That(color.B, Is.EqualTo(0));
        Assert.That(color.A, Is.EqualTo(0));
        Assert.That(color.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Color color = new(127);
        Assert.That(color.R, Is.EqualTo(127));
        Assert.That(color.G, Is.EqualTo(127));
        Assert.That(color.B, Is.EqualTo(127));
        Assert.That(color.A, Is.EqualTo(127));

        color = new(0.5f);
        Assert.That(color.R, Is.EqualTo(128));
        Assert.That(color.G, Is.EqualTo(128));
        Assert.That(color.B, Is.EqualTo(128));
        Assert.That(color.A, Is.EqualTo(128));

        color = new(127, 0, 234, 255);
        Assert.That(color.R, Is.EqualTo(127));
        Assert.That(color.G, Is.EqualTo(0));
        Assert.That(color.B, Is.EqualTo(234));
        Assert.That(color.A, Is.EqualTo(255));

        color = new(0.65f, 0.0f, 0.73f, 1.0f);
        Assert.That(color.R, Is.EqualTo(166));
        Assert.That(color.G, Is.EqualTo(0));
        Assert.That(color.B, Is.EqualTo(186));
        Assert.That(color.A, Is.EqualTo(255));
    }
}
