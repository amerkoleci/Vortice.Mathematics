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
        public static implicit operator CoreGraphics.CGRect(Rect rect)
        {
            return new CoreGraphics.CGRect((nfloat)rect.Left, (nfloat)rect.Top, (nfloat)rect.Width, (nfloat)rect.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CoreGraphics.CGRect"/> to <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rect(CoreGraphics.CGRect rect)
        {
            return new Rect((float)rect.Left, (float)rect.Top, (float)rect.Right, (float)rect.Bottom);
        }
    }
}
