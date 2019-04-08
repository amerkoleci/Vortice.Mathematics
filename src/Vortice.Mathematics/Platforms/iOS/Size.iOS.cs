// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    public partial struct Size
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Size"/> to <see cref="CoreGraphics.CGSize"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator CoreGraphics.CGSize(Size size)
        {
            return new CoreGraphics.CGSize((nfloat)size.Width, (nfloat)size.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CoreGraphics.CGSize"/> to <see cref="Size"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Size(CoreGraphics.CGSize size)
        {
            return new Size((float)size.Width, (float)size.Height);
        }
    }
}
