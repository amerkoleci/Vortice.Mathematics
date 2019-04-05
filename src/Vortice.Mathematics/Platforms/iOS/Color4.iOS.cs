// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Color4
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Color"/> to <see cref="UIKit.UIColor"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator UIKit.UIColor(Color4 color)
        {
            return new UIKit.UIColor(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UIKit.UIColor"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Color4(UIKit.UIColor color)
        {
            color.GetRGBA(out var red, out var green, out var blue, out var alpha);
            return new Color4((float)red, (float)green, (float)blue, (float)alpha);
        }
    }
}
