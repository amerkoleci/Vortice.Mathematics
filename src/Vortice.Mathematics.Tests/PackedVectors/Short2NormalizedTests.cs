// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using NUnit.Framework;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics.Tests.PackedVectors;

[TestFixture(TestOf = typeof(Short2Normalized))]
public partial class Short2NormalizedTests
{
    [TestCase]
    public void DefaultChecks()
    {
        Short2Normalized vector = new();
        Assert.AreEqual(vector.X, 0);
        Assert.AreEqual(vector.Y, 0);
        Assert.AreEqual(vector.PackedValue, 0u);
    }

    [TestCase]
    public void CreationTests()
    {
        Short2Normalized vector = new(250, 450);
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
