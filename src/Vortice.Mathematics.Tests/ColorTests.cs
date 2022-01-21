// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vortice.Mathematics.Tests;

[TestClass]
[TestCategory("Color")]
public partial class ColorTests
{
    [TestMethod]
    public void DefaultChecks()
    {
        Color color = new Color();
        Assert.AreEqual(color.R, 0);
        Assert.AreEqual(color.G, 0);
        Assert.AreEqual(color.B, 0);
        Assert.AreEqual(color.A, 0);
        Assert.AreEqual(color.PackedValue, 0u);
    }

    [TestMethod]
    public void CreationTests()
    {
        Color color = new(127);
        Assert.AreEqual(color.R, 127);
        Assert.AreEqual(color.G, 127);
        Assert.AreEqual(color.B, 127);
        Assert.AreEqual(color.A, 127);

        color = new(0.65f);
        Assert.AreEqual(color.R, 166);
        Assert.AreEqual(color.G, 166);
        Assert.AreEqual(color.B, 166);
        Assert.AreEqual(color.A, 166);

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
