// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;

namespace Vortice.Mathematics.Tests;

[TestFixture(TestOf = typeof(MathHelper))]
public partial class MathHelperTests
{
    [TestCase]
    public void IsSupported()
    {
        Assert.AreEqual(MathHelper.Max(3, 5), 5);
    }
}
