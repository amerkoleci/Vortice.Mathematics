// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vortice.Mathematics.Tests;

[TestClass]
[TestCategory("Color4")]
public partial class Color4Tests
{
    [TestMethod]
    public void DefaultChecks()
    {
        Color4 color = new();
        Assert.AreEqual(color.R, 0.0f);
        Assert.AreEqual(color.G, 0.0f);
        Assert.AreEqual(color.B, 0.0f);
        Assert.AreEqual(color.A, 0.0f);
    }

    [TestMethod]
    public void CreationTests()
    {
        Color4 color = new(0.5f);
        Assert.AreEqual(color.R, 0.5f);
        Assert.AreEqual(color.G, 0.5f);
        Assert.AreEqual(color.B, 0.5f);
        Assert.AreEqual(color.A, 0.5f);

        color = new(1.0f, 0.0f, 1.0f, 1.0f);
        Assert.AreEqual(color.R, 1.0f);
        Assert.AreEqual(color.G, 0.0f);
        Assert.AreEqual(color.B, 1.0f);
        Assert.AreEqual(color.A, 1.0f);
    }
}
