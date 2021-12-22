// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vortice.Mathematics.Tests;

[TestClass]
[TestCategory("Initialization")]
public partial class MathHelperTests
{
    [TestMethod]
    public void IsSupported()
    {
        Assert.AreEqual(MathHelper.Max(3, 5), 5);
    }
}
