// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    public partial struct RectI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="RectI"/> to <see cref="CoreGraphics.CGRect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator CoreGraphics.CGRect(RectI rect)
        {
            return new CoreGraphics.CGRect((nfloat)rect.Left, (nfloat)rect.Top, (nfloat)rect.Width, (nfloat)rect.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CoreGraphics.CGRect"/> to <see cref="RectI"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectI(CoreGraphics.CGRect rect)
        {
            if (rect.Left > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Left));

            if (rect.Top > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Top));

            if (rect.Right > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Right));

            if (rect.Bottom > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Bottom));

            return new RectI((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);
        }
    }
}
