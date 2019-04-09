// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct RectF
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="RectF"/> to <see cref="System.Drawing.RectangleF"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.RectangleF(RectF rect) =>
            new System.Drawing.RectangleF(rect.Left, rect.Top, rect.Width, rect.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.RectangleF"/> to <see cref="RectF"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectF(System.Drawing.RectangleF rect) =>
            new RectF(rect.X, rect.Y, rect.Width, rect.Height);
    }
}
