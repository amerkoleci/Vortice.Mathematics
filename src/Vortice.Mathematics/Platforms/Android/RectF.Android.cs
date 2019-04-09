// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct RectF
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="RectF"/> to <see cref="Android.Graphics.RectF"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Graphics.RectF(RectF rect) =>
            new Android.Graphics.RectF(rect.Left, rect.Top, rect.Right, rect.Bottom);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Graphics.RectF"/> to <see cref="RectF"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectF(Android.Graphics.RectF rect) =>
            new RectF(rect.Left, rect.Top, rect.Width(), rect.Height());
    }
}
