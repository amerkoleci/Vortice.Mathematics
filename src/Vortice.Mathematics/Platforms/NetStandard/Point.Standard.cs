// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Point
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="System.Drawing.PointF"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.PointF(Point point)
        {
            return new System.Drawing.PointF(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.PointF"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(System.Drawing.PointF point)
        {
            return new Point(point.X, point.Y);
        }
    }
}
