// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Color
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Color"/> to <see cref="UIKit.UIColor"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator UIKit.UIColor(Color color)
        {
            return new UIKit.UIColor(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UIKit.UIColor"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Color(UIKit.UIColor color)
        {
            color.GetRGBA(out var red, out var green, out var blue, out var alpha);
            return new Color((float)red, (float)green, (float)blue, (float)alpha);
        }
    }
}
