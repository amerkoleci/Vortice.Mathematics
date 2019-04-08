// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct PointI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="Android.Graphics.Point"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Graphics.Point(PointI point)
        {
            return new Android.Graphics.Point(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Graphics.Point"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PointI(Android.Graphics.Point point)
        {
            return new PointI(point.X, point.Y);
        }
    }
}
