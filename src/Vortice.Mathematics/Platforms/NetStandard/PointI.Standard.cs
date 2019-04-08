// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct PointI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="PointI"/> to <see cref="System.Drawing.Point"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Point(PointI point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Point"/> to <see cref="PointI"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PointI(System.Drawing.Point point)
        {
            return new PointI(point.X, point.Y);
        }
    }
}
