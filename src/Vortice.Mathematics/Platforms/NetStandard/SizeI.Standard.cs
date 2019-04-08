// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct SizeI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="SizeI"/> to <see cref="System.Drawing.Size"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Size(SizeI size)
        {
            return new System.Drawing.Size(size.Width, size.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Size"/> to <see cref="SizeI"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SizeI(System.Drawing.Size size)
        {
            return new SizeI(size.Width, size.Height);
        }
    }
}
