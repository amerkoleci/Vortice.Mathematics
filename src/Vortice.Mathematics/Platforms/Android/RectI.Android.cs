// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct RectI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="RectI"/> to <see cref="Android.Graphics.Rect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Graphics.Rect(RectI rect)
        {
            return new Android.Graphics.Rect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Graphics.Rect"/> to <see cref="RectI"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectI(Android.Graphics.Rect rect)
        {
            return new RectI(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
    }
}
