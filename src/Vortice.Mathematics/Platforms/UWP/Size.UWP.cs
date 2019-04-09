// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using WindowsSize = Windows.Foundation.Size;

namespace Vortice.Mathematics
{
    public partial struct Size
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Size"/> to <see cref="WindowsSize"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WindowsSize(Size size) =>
            new WindowsSize(size.Width, size.Height);


        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsSize"/> to <see cref="Size"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Size(WindowsSize size)
        {
            if (size.Width > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(size.Width));

            if (size.Height > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(size.Height));

            return new Size((int)size.Width, (int)size.Height);
        }
    }
}
