// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    public partial struct SizeI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="SizeI"/> to <see cref="CoreGraphics.CGSize"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator CoreGraphics.CGSize(SizeI size)
        {
            return new CoreGraphics.CGSize((nfloat)size.Width, (nfloat)size.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CoreGraphics.CGSize"/> to <see cref="SizeI"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SizeI(CoreGraphics.CGSize size)
        {
            if (size.Width > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(size.Width));

            if (size.Height > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(size.Height));

            return new SizeI((int)size.Width, (int)size.Height);
        }
    }
}
