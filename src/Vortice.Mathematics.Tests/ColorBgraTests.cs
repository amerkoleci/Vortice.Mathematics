// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;

namespace Vortice.Mathematics.Tests;

[TestFixture(TestOf = typeof(ColorBgra))]
public partial class ColorBgraTests
{
    [TestCase]
    public void DefaultChecks()
    {
        ColorBgra color = new ColorBgra();
        Assert.AreEqual(color.R, 0);
        Assert.AreEqual(color.G, 0);
        Assert.AreEqual(color.B, 0);
        Assert.AreEqual(color.A, 0);
        Assert.AreEqual(color.PackedValue, 0u);
    }

    [TestCase]
    public void CreationTests()
    {
        ColorBgra color = new(127);
        Assert.AreEqual(color.R, 127);
        Assert.AreEqual(color.G, 127);
        Assert.AreEqual(color.B, 127);
        Assert.AreEqual(color.A, 127);

        color = new(0.5f);
        Assert.AreEqual(color.R, 128);
        Assert.AreEqual(color.G, 128);
        Assert.AreEqual(color.B, 128);
        Assert.AreEqual(color.A, 128);

        color = new(127, 0, 234, 255);
        Assert.AreEqual(color.R, 127);
        Assert.AreEqual(color.G, 0);
        Assert.AreEqual(color.B, 234);
        Assert.AreEqual(color.A, 255);

        color = new(0.65f, 0.0f, 0.73f, 1.0f);
        Assert.AreEqual(color.R, 166);
        Assert.AreEqual(color.G, 0);
        Assert.AreEqual(color.B, 186);
        Assert.AreEqual(color.A, 255);
    }
}
