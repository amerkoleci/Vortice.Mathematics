// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using NUnit.Framework;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics.Tests.PackedVectors;

[TestFixture(TestOf = typeof(Byte4))]
public partial class Byte4Tests
{
    [TestCase]
    public void DefaultChecks()
    {
        Byte4 vector = new();
        Assert.That(vector.X, Is.EqualTo(0));
        Assert.That(vector.Y, Is.EqualTo(0));
        Assert.That(vector.Z, Is.EqualTo(0));
        Assert.That(vector.W, Is.EqualTo(0));
        Assert.That(vector.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Byte4 vector = new((sbyte)125, (sbyte)-125, (sbyte)100, (sbyte)-5);
        Assert.That(vector.X, Is.EqualTo(125));
        Assert.That(vector.Y, Is.EqualTo(-125));
        Assert.That(vector.Z, Is.EqualTo(100));
        Assert.That(vector.W, Is.EqualTo(-5));
        Assert.That(vector.PackedValue, Is.EqualTo(4217668477));

        vector = new(125.0f, -125.0f, 100.0f, -5.0f);
        Assert.That(vector.X, Is.EqualTo(125));
        Assert.That(vector.Y, Is.EqualTo(-125));
        Assert.That(vector.Z, Is.EqualTo(100));
        Assert.That(vector.W, Is.EqualTo(-5));

        Vector4 vector4 = vector.ToVector4();
        Assert.That(vector4.X, Is.EqualTo(125.0f));
        Assert.That(vector4.Y, Is.EqualTo(-125.0f));
        Assert.That(vector4.Z, Is.EqualTo(100.0f));
        Assert.That(vector4.W, Is.EqualTo(-5.0f));
    }
}
