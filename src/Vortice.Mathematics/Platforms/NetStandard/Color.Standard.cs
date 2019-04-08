// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Color
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Color"/> to <see cref="System.Drawing.Color"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Color(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Color"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Color(System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }
    }
}
