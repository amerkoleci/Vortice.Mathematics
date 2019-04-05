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
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WindowsColor(Color value)
        {
            return WindowsColor.FromArgb(value.A, value.R, value.G, value.B);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsColor"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Color(WindowsColor value)
        {
            return new Color(value.R, value.G, value.B, value.A);
        }
    }
}
