// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using NUnit.Framework;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics.Tests.PackedVectors;

[TestFixture(TestOf = typeof(Byte2Normalized))]
public partial class Byte2NormalizedTests
{
    private const float Epsilon = 0.01f;

    [TestCase]
    public void DefaultChecks()
    {
        Byte2Normalized vector = new();
        Assert.That(vector.X, Is.EqualTo(0));
        Assert.That(vector.Y, Is.EqualTo(0));
        Assert.That(vector.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Byte2Normalized vector = new((sbyte)125, (sbyte)-125);
        Assert.That(vector.X, Is.EqualTo(125));
        Assert.That(vector.Y, Is.EqualTo(-125));
        Assert.That(vector.PackedValue, Is.EqualTo(33661));

        vector = new(0.5f, -0.5f);
        Assert.That(vector.X, Is.EqualTo(64.0));
        Assert.That(vector.Y, Is.EqualTo(-64.0f));

        Vector2 vector2 = vector.ToVector2();

        Assert.That(vector2.X, Is.EqualTo(0.5f).Within(Epsilon));
        Assert.That(vector2.Y, Is.EqualTo(-0.5f).Within(Epsilon));
    }
}
