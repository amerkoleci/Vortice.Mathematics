// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using WindowsPoint = Windows.Foundation.Point;

namespace Vortice.Mathematics
{
    public partial struct Point
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="WindowsPoint"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WindowsPoint(Point point)
        {
            return new WindowsPoint(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsPoint"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(WindowsPoint point)
        {
            if (point.X > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(point.X));

            if (point.Y > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(point.Y));

            return new Point((int)point.X, (int)point.Y);
        }
    }
}
