// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using WindowsRect = Windows.Foundation.Rect;

namespace Vortice.Mathematics
{
    public partial struct Rect
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Rect"/> to <see cref="WindowsRect"/>.
        /// </summary>
        /// <param name="rect">The rect value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WindowsRect(Rect rect)
        {
            return new WindowsRect(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsRect"/> to <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rect(WindowsRect rect)
        {
            return new Rect((float)rect.Left, (float)rect.Top, (float)rect.Right, (float)rect.Bottom);
        }
    }
}
