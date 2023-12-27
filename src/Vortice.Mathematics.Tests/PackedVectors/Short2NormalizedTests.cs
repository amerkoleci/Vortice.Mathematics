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
        Assert.That(vector.X, Is.EqualTo(0));
        Assert.That(vector.Y, Is.EqualTo(0));
        Assert.That(vector.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Short2Normalized vector = new(250, 450);
        Assert.That(vector.X, Is.EqualTo(250));
        Assert.That(vector.Y, Is.EqualTo(450));
        Assert.That(vector.PackedValue, Is.EqualTo(29491450u));

        vector = new(0.5f, 0.3f);
        Assert.That(vector.X, Is.EqualTo(16384));
        Assert.That(vector.Y, Is.EqualTo(9830));
        Assert.That(vector.PackedValue, Is.EqualTo(644235264u));

        Vector2 vector2 = vector.ToVector2();
        Assert.That(MathHelper.CompareEqual(MathF.Round(vector2.X, 1), 0.5f), Is.True);
        Assert.That(MathHelper.CompareEqual(MathF.Round(vector2.Y, 1), 0.3f), Is.True);
        //Assert.IsTrue(Vector2Utilities.NearEqual(vector2, new Vector2(0.5f, 0.3f), MathHelper.NearZeroEpsilon));
    }
}
