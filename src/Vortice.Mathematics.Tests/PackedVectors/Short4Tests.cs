// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using NUnit.Framework;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics.Tests.PackedVectors;

[TestFixture(TestOf = typeof(Short4))]
public partial class Short4Tests
{
    [TestCase]
    public void DefaultChecks()
    {
        Short4 vector = new();
        Assert.That(vector.X, Is.EqualTo(0));
        Assert.That(vector.Y, Is.EqualTo(0));
        Assert.That(vector.Z, Is.EqualTo(0));
        Assert.That(vector.W, Is.EqualTo(0));
        Assert.That(vector.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Short4 vector = new(250, 450, 320, 240);
        Assert.That(vector.X, Is.EqualTo(250));
        Assert.That(vector.Y, Is.EqualTo(450));
        Assert.That(vector.Z, Is.EqualTo(320));
        Assert.That(vector.W, Is.EqualTo(240));
        Assert.That(vector.PackedValue, Is.EqualTo(67555368829583610u));

        vector = new(125.0f, 255.5f, 325.7f, 645.2f);
        Assert.That(vector.X, Is.EqualTo(125));
        Assert.That(vector.Y, Is.EqualTo(256));
        Assert.That(vector.Z, Is.EqualTo(326));
        Assert.That(vector.W, Is.EqualTo(645));

        Vector4 vector4 = vector.ToVector4();
        Assert.That(vector4.X, Is.EqualTo(125.0f));
        Assert.That(vector4.Y, Is.EqualTo(256.0f));
        Assert.That(vector4.Z, Is.EqualTo(326.0f));
        Assert.That(vector4.W, Is.EqualTo(645.0f));
    }
}
