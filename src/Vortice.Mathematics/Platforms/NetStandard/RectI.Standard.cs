// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct RectI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="RectI"/> to <see cref="System.Drawing.Rectangle"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Rectangle(RectI rect)
        {
            return new System.Drawing.Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Rectangle"/> to <see cref="PointI"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectI(System.Drawing.Rectangle rect)
        {
            return new RectI(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
    }
}
