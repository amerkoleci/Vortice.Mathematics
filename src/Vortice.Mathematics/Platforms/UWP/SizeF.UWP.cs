// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using WindowsSize = Windows.Foundation.Size;

namespace Vortice.Mathematics
{
    public partial struct SizeF
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="SizeF"/> to <see cref="WindowsSize"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WindowsSize(SizeF size) =>
            new WindowsSize(size.Width, size.Height);


        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsSize"/> to <see cref="SizeF"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SizeF(WindowsSize size) =>
            new SizeF((float)size.Width, (float)size.Height);
    }
}
