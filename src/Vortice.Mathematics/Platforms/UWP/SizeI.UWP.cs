// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using WindowsSize = Windows.Foundation.Size;

namespace Vortice.Mathematics
{
    public partial struct SizeI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="RectI"/> to <see cref="WindowsSize"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WindowsSize(SizeI size)
        {
            return new WindowsSize(size.Width, size.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsSize"/> to <see cref="RectI"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SizeI(WindowsSize size)
        {
            if (size.Width > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(size.Width));

            if (size.Height > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(size.Height));

            return new SizeI((int)size.Width, (int)size.Height);
        }
    }
}
