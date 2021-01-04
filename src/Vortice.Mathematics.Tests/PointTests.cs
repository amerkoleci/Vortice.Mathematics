// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Xunit;

namespace Vortice.Mathematics.Tests
{
    public unsafe class PointTests
    {
        [Fact]
        public void DefaultConstructorTest()
        {
            Assert.Equal(Point.Empty, new Point());
        }

        [Fact]
        public void Point_SizeOf()
        {
            Assert.Equal(sizeof(Point), 8);
        }
    }
}
