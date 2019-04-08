// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using WindowsColor = Windows.UI.Color;

namespace Vortice.Mathematics
{
    public partial struct Color
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Color"/> to <see cref="WindowsColor"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WindowsColor(Color color)
        {
            return WindowsColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsColor"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Color(WindowsColor color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }
    }
}
