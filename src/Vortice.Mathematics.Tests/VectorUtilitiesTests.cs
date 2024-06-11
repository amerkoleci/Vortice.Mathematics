// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.Intrinsics;
using NUnit.Framework;

namespace Vortice.Mathematics.Tests;

[TestFixture(TestOf = typeof(VectorUtilities))]
public partial class VectorUtilitiesTests
{
    [TestCase]
    public void SaturateTests()
    {
        Vector128<float> value = VectorUtilities.Saturate(Vector128.Create(0.158f, 125.3456f, -MathF.PI, 1.4563788f));
        Assert.That(value.GetElement(0), Is.EqualTo(0.158f));
        Assert.That(value.GetElement(1), Is.EqualTo(1.0f));
        Assert.That(value.GetElement(2), Is.EqualTo(0.0f));
        Assert.That(value.GetElement(3), Is.EqualTo(1.0f));
    }

    [TestCase]
    public void TruncateTests()
    {
        Vector128<float> value = VectorUtilities.Truncate(Vector128.Create(0.158f, 125.3456f, -MathF.PI, 1.4563788f));
        Assert.That(value.GetElement(0), Is.EqualTo(0.0f));
        Assert.That(value.GetElement(1), Is.EqualTo(125.0f));
        Assert.That(value.GetElement(2), Is.EqualTo(-3.0f));
        Assert.That(value.GetElement(3), Is.EqualTo(1.0f));

        // Software fallback
        value = Vector128.Create(
            MathF.Truncate(0.158f),
            MathF.Truncate(125.3456f),
            MathF.Truncate(-MathF.PI),
            MathF.Truncate(1.4563788f)
        );
        Assert.That(value.GetElement(0), Is.EqualTo(0.0f));
        Assert.That(value.GetElement(1), Is.EqualTo(125.0f));
        Assert.That(value.GetElement(2), Is.EqualTo(-3.0f));
        Assert.That(value.GetElement(3), Is.EqualTo(1.0f));
    }
}
