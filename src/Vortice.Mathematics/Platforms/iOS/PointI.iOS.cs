// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    public partial struct PointI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="PointI"/> to <see cref="CoreGraphics.CGPoint"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator CoreGraphics.CGPoint(PointI point)
        {
            return new CoreGraphics.CGPoint(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CoreGraphics.CGPoint"/> to <see cref="PointI"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PointI(CoreGraphics.CGPoint point)
        {
            if (point.X > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(point.X));

            if (point.Y > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(point.Y));

            return new PointI((int)point.X, (int)point.Y);
        }
    }
}
