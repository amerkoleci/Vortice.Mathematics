// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Rect
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Rect"/> to <see cref="Android.Graphics.Rect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Graphics.Rect(Rect rect) =>
            new Android.Graphics.Rect(rect.X, rect.X, rect.Right, rect.Bottom);


        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Graphics.Rect"/> to <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rect(Android.Graphics.Rect rect) =>
            new Rect(rect.Left, rect.Top, rect.Width(), rect.Height());
    }
}
