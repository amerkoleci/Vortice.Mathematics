// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Point
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="System.Drawing.Point"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Point(Point value)
        {
            return new System.Drawing.Point(value.X, value.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Point"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(System.Drawing.Point value)
        {
            return new Point(value.X, value.Y);
        }
    }
}
