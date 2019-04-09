// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct SizeF
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="SizeF"/> to <see cref="System.Drawing.SizeF"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.SizeF(SizeF size) =>
            new System.Drawing.SizeF(size.Width, size.Height);


        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.SizeF"/> to <see cref="SizeF"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SizeF(System.Drawing.SizeF size) =>
            new SizeF(size.Width, size.Height);
    }
}
