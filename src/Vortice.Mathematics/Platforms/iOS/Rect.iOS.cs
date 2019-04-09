// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    public partial struct Rect
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Rect"/> to <see cref="CoreGraphics.CGRect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator CoreGraphics.CGRect(Rect rect) =>
            new CoreGraphics.CGRect((nfloat)rect.X, (nfloat)rect.Y, (nfloat)rect.Width, (nfloat)rect.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="CoreGraphics.CGRect"/> to <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rect(CoreGraphics.CGRect rect)
        {
            if (rect.Left > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Left));

            if (rect.Top > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Top));

            if (rect.Width > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Width));

            if (rect.Height > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Height));

            return new Rect((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }
    }
}
