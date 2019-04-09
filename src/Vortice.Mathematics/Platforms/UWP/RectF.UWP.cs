// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using WindowsRect = Windows.Foundation.Rect;

namespace Vortice.Mathematics
{
    public partial struct RectF
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="RectF"/> to <see cref="WindowsRect"/>.
        /// </summary>
        /// <param name="rect">The rect value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WindowsRect(RectF rect) =>
            new WindowsRect(rect.Left, rect.Top, rect.Width, rect.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsRect"/> to <see cref="RectF"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectF(WindowsRect rect) =>
            new RectF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
    }
}
