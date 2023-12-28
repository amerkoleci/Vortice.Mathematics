// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using NUnit.Framework;

namespace Vortice.Mathematics.Tests;

[TestFixture(TestOf = typeof(Color))]
public partial class ColorTests
{
    private const float Epsilon = 0.01f;

    [TestCase]
    public void DefaultChecks()
    {
        Color color = new Color();
        Assert.That(color.R, Is.EqualTo(0));
        Assert.That(color.G, Is.EqualTo(0));
        Assert.That(color.B, Is.EqualTo(0));
        Assert.That(color.A, Is.EqualTo(0));
        Assert.That(color.PackedValue, Is.EqualTo(0u));
    }

    [TestCase]
    public void CreationTests()
    {
        Color color = new(127, 127, 127, 127);
        Assert.That(color.R, Is.EqualTo(127));
        Assert.That(color.G, Is.EqualTo(127));
        Assert.That(color.B, Is.EqualTo(127));
        Assert.That(color.A, Is.EqualTo(127));

        color = new(0.5f);
        Assert.That(color.R, Is.EqualTo(127));
        Assert.That(color.G, Is.EqualTo(127));
        Assert.That(color.B, Is.EqualTo(127));
        Assert.That(color.A, Is.EqualTo(127));

        color = new(127, 0, 234, 255);
        Assert.That(color.R, Is.EqualTo(127));
        Assert.That(color.G, Is.EqualTo(0));
        Assert.That(color.B, Is.EqualTo(234));
        Assert.That(color.A, Is.EqualTo(255));

        color = new(0.65f, 0.0f, 0.73f, 1.0f);
        Assert.That(color.R, Is.EqualTo(165));
        Assert.That(color.G, Is.EqualTo(0));
        Assert.That(color.B, Is.EqualTo(186));
        Assert.That(color.A, Is.EqualTo(255));
    }

    [TestCase]
    //[Ignore("Handle float comparison")]
    public void ConversionTests()
    {
        float red = 0.6f;
        float green = 0.1f;
        float blue = 0.7f;
        float alpha = 0.5f;
        Color color = new(red, green, blue, alpha);


        Vector4 vector4 = color.ToVector4();
        Assert.That(vector4.X, Is.EqualTo(red).Within(Epsilon));
        Assert.That(vector4.Y, Is.EqualTo(green).Within(Epsilon));
        Assert.That(vector4.Z, Is.EqualTo(blue).Within(Epsilon));
        Assert.That(vector4.W, Is.EqualTo(alpha).Within(Epsilon));

        Color4 color4 = color.ToColor4();
        Assert.That(color4.R, Is.EqualTo(red).Within(Epsilon));
        Assert.That(color4.G, Is.EqualTo(green).Within(Epsilon));
        Assert.That(color4.B, Is.EqualTo(blue).Within(Epsilon));
        Assert.That(color4.A, Is.EqualTo(alpha).Within(Epsilon));
    }
}
