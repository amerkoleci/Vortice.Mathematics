// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using NUnit.Framework;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics.Tests.PackedVectors;

[TestFixture(TestOf = typeof(Short2))]
public partial class Short2Tests
{
    [TestCase]
    public void DefaultChecks()
    {
        Short2 vector = new();
        Assert.That(vector.X, Is.EqualTo(0));
        Assert.That(vector.Y, Is.EqualTo(0));
        Assert.That(vector.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Short2 vector = new(250, 450);
        Assert.That(vector.X, Is.EqualTo(250));
        Assert.That(vector.Y, Is.EqualTo(450));
        Assert.That(vector.PackedValue, Is.EqualTo(29491450u));

        vector = new(125.0f, 255.0f);
        Assert.That(vector.X, Is.EqualTo(125));
        Assert.That(vector.Y, Is.EqualTo(255));

        Vector2 vector2 = vector.ToVector2();
        Assert.That(vector2.X, Is.EqualTo(125.0f));
        Assert.That(vector2.Y, Is.EqualTo(255.0f));
    }
}
