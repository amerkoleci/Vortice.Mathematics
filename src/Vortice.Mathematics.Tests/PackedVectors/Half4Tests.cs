// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using NUnit.Framework;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics.Tests.PackedVectors;

[TestFixture(TestOf = typeof(Half4))]
public partial class Half4Tests
{
    private const float Epsilon = 0.01f;

    [TestCase]
    public void DefaultChecks()
    {
        Half4 vector = new();
        Assert.That((float)vector.X, Is.EqualTo(0.0f));
        Assert.That((float)vector.Y, Is.EqualTo(0.0f));
        Assert.That((float)vector.Z, Is.EqualTo(0.0f));
        Assert.That((float)vector.W, Is.EqualTo(0.0f));
        Assert.That(vector.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Half4 vector = new(0.5f, -0.5f, 0.3f, -0.7f);
        Assert.That((float)vector.X, Is.EqualTo(0.5f).Within(Epsilon));
        Assert.That((float)vector.Y, Is.EqualTo(-0.5f).Within(Epsilon));
        Assert.That((float)vector.Z, Is.EqualTo(0.3f).Within(Epsilon));
        Assert.That((float)vector.W, Is.EqualTo(-0.7f).Within(Epsilon));

        Vector4 vector4 = vector.ToVector4();

        Assert.That(vector4.X, Is.EqualTo(0.5f).Within(Epsilon));
        Assert.That(vector4.Y, Is.EqualTo(-0.5f).Within(Epsilon));
        Assert.That(vector4.Z, Is.EqualTo(0.3f).Within(Epsilon));
        Assert.That(vector4.W, Is.EqualTo(-0.7f).Within(Epsilon));
    }
}
