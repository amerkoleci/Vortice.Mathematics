// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    public partial struct Point
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="Android.Graphics.PointF"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Graphics.PointF(Point point)
        {
            return new Android.Graphics.PointF(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Graphics.PointF"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(Android.Graphics.PointF point)
        {
            return new Point(point.X, point.Y);
        }
    }
}
