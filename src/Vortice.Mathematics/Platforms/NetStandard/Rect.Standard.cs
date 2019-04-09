// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Rect
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Rect"/> to <see cref="System.Drawing.Rectangle"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Rectangle(Rect rect) =>
            new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Rectangle"/> to <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rect(System.Drawing.Rectangle rect) =>
            new Rect(rect.X, rect.Y, rect.Width, rect.Height);
    }
}
