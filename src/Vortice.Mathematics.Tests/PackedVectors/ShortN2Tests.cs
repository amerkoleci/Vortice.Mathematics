// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics.Tests.PackedVectors;

[TestClass]
[TestCategory("ShortN2")]
public partial class ShortN2Tests
{
    [TestMethod]
    public void DefaultChecks()
    {
        ShortN2 vector = new ShortN2();
        Assert.AreEqual(vector.X, 0);
        Assert.AreEqual(vector.Y, 0);
        Assert.AreEqual(vector.PackedValue, 0u);
    }

    [TestMethod]
    public void CreationTests()
    {
        ShortN2 vector = new(250, 450);
        Assert.AreEqual(vector.X, 250);
        Assert.AreEqual(vector.Y, 450);
        Assert.AreEqual(vector.PackedValue, 29491450u);

        vector = new(0.5f, 0.3f);
        Assert.AreEqual(vector.X, 16384);
        Assert.AreEqual(vector.Y, 9830);
        Assert.AreEqual(vector.PackedValue, 644235264u);

        Vector2 vector2 = vector.ToVector2();
        Assert.IsTrue(MathHelper.CompareEqual(MathF.Round(vector2.X, 1), 0.5f));
        Assert.IsTrue(MathHelper.CompareEqual(MathF.Round(vector2.Y, 1), 0.3f));
        //Assert.IsTrue(Vector2Utilities.NearEqual(vector2, new Vector2(0.5f, 0.3f), MathHelper.NearZeroEpsilon));
    }
}
