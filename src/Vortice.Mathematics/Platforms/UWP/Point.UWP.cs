// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

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
        public static implicit operator Windows.Foundation.Point(Point point)
        {
            return new Windows.Foundation.Point(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsPoint"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(WindowsPoint point)
        {
            return new Point((float)point.X, (float)point.Y);
        }
    }
}
