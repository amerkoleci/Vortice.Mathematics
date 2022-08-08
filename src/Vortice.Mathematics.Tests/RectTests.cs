// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;

namespace Vortice.Mathematics.Tests;

[TestFixture(TestOf = typeof(Rect))]
public partial class RectTests
{
    [TestCase]
    public void DefaultChecks()
    {
        Rect rect = new();
        Assert.AreEqual(rect.X, 0);
        Assert.AreEqual(rect.Y, 0);
        Assert.AreEqual(rect.Width, 0);
        Assert.AreEqual(rect.Height, 0);
    }

    [TestCase]
    public void ToStringTest()
    {
        Rect rect = new(10, 20, 150, 250);
        string str = rect.ToString();
    }
}
