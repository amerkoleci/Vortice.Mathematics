// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    public partial struct Point
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="CoreGraphics.CGPoint"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator CoreGraphics.CGPoint(Point point)
        {
            return new CoreGraphics.CGPoint(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CoreGraphics.CGPoint"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(CoreGraphics.CGPoint point)
        {
            return new Point((float)point.X, (float)point.Y);
        }
    }
}
