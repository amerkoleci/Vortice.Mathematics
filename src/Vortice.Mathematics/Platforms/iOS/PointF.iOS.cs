// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    public partial struct PointF
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="PointF"/> to <see cref="CoreGraphics.CGPoint"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator CoreGraphics.CGPoint(PointF point)
        {
            return new CoreGraphics.CGPoint(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CoreGraphics.CGPoint"/> to <see cref="PointF"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PointF(CoreGraphics.CGPoint point)
        {
            return new PointF((float)point.X, (float)point.Y);
        }
    }
}
