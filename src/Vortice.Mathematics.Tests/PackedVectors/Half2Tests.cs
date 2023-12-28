// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using NUnit.Framework;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics.Tests.PackedVectors;

[TestFixture(TestOf = typeof(Half2))]
public partial class Half2Tests
{
    private const float Epsilon = 0.01f;

    [TestCase]
    public void DefaultChecks()
    {
        Half2 vector = new();
        Assert.That((float)vector.X, Is.EqualTo(0.0f));
        Assert.That((float)vector.Y, Is.EqualTo(0.0f));
        Assert.That(vector.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Half2 vector = new(0.5f, -0.5f);
        Assert.That((float)vector.X, Is.EqualTo(0.5f));
        Assert.That((float)vector.Y, Is.EqualTo(-0.5f));

        Vector2 vector2 = vector.ToVector2();

        Assert.That(vector2.X, Is.EqualTo(0.5f).Within(Epsilon));
        Assert.That(vector2.Y, Is.EqualTo(-0.5f).Within(Epsilon));
    }
}
