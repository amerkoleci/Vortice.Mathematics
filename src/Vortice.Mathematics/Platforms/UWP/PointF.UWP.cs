// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using WindowsPoint = Windows.Foundation.Point;

namespace Vortice.Mathematics
{
    public partial struct PointF
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="PointF"/> to <see cref="WindowsPoint"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Windows.Foundation.Point(PointF point)
        {
            return new Windows.Foundation.Point(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsPoint"/> to <see cref="PointF"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PointF(WindowsPoint point)
        {
            return new PointF((float)point.X, (float)point.Y);
        }
    }
}
