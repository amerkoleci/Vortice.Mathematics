// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Rect
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Rect"/> to <see cref="System.Drawing.RectangleF"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.RectangleF(Rect rect)
        {
            return new System.Drawing.RectangleF(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.RectangleF"/> to <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rect(System.Drawing.RectangleF rect)
        {
            return new Rect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
    }
}
