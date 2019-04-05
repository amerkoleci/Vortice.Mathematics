// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Color
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Color"/> to <see cref="Android.Graphics.Color"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Graphics.Color(Color color)
        {
            return new Android.Graphics.Color(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Graphics.Color"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Color(Android.Graphics.Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }
    }
}
