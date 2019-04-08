// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Rect
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Rect"/> to <see cref="Android.Graphics.RectF"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Graphics.RectF(Rect rect)
        {
            return new Android.Graphics.RectF(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Graphics.RectF"/> to <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rect(Android.Graphics.RectF rect)
        {
            return new Rect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
    }
}
