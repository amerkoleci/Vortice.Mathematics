// Copyright © Amer Koleci and Contributors.
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
        Assert.AreEqual(vector.X, 0);
        Assert.AreEqual(vector.Y, 0);
        Assert.AreEqual(vector.Z, 0);
        Assert.AreEqual(vector.W, 0);
        Assert.AreEqual(vector.PackedValue, 0u);
    }

    [TestCase]
    public void CreationTests()
    {
        Short4 vector = new(250, 450, 320, 240);
        Assert.AreEqual(vector.X, 250);
        Assert.AreEqual(vector.Y, 450);
        Assert.AreEqual(vector.Z, 320);
        Assert.AreEqual(vector.W, 240);
        Assert.AreEqual(vector.PackedValue, 67555368829583610u);

        vector = new(125.0f, 255.5f, 325.7f, 645.2f);
        Assert.AreEqual(vector.X, 125);
        Assert.AreEqual(vector.Y, 256);
        Assert.AreEqual(vector.Z, 326);
        Assert.AreEqual(vector.W, 645);

        Vector4 vector4 = vector.ToVector4();
        Assert.AreEqual(vector4.X, 125.0f);
        Assert.AreEqual(vector4.Y, 256.0f);
        Assert.AreEqual(vector4.Z, 326.0f);
        Assert.AreEqual(vector4.W, 645.0f);
    }
}
