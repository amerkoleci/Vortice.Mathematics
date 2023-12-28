// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using NUnit.Framework;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics.Tests.PackedVectors;

[TestFixture(TestOf = typeof(Byte4Normalized))]
public partial class Byte4NormalizedTests
{
    private const float Epsilon = 0.01f;

    [TestCase]
    public void DefaultChecks()
    {
        Byte4Normalized vector = new();
        Assert.That(vector.X, Is.EqualTo(0));
        Assert.That(vector.Y, Is.EqualTo(0));
        Assert.That(vector.Z, Is.EqualTo(0));
        Assert.That(vector.W, Is.EqualTo(0));
        Assert.That(vector.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Byte4Normalized vector = new((sbyte)125, (sbyte)-125, (sbyte)100, (sbyte)-5);
        Assert.That(vector.X, Is.EqualTo(125));
        Assert.That(vector.Y, Is.EqualTo(-125));
        Assert.That(vector.Z, Is.EqualTo(100));
        Assert.That(vector.W, Is.EqualTo(-5));
        Assert.That(vector.PackedValue, Is.EqualTo(4217668477));

        vector = new(0.5f, -0.5f, 0.7f, -0.3f);
        Assert.That(vector.X, Is.EqualTo(63.0f));
        Assert.That(vector.Y, Is.EqualTo(-63.0f));
        Assert.That(vector.Z, Is.EqualTo(88.0f));
        Assert.That(vector.W, Is.EqualTo(-38.0f));

        Vector4 vector4 = vector.ToVector4();

        Assert.That(vector4.X, Is.EqualTo(0.5f).Within(Epsilon));
        Assert.That(vector4.Y, Is.EqualTo(-0.5f).Within(Epsilon));
        Assert.That(vector4.Z, Is.EqualTo(0.7f).Within(Epsilon));
        Assert.That(vector4.W, Is.EqualTo(-0.3f).Within(Epsilon));
    }
}
