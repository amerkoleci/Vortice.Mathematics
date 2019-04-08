// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct Size
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Size"/> to <see cref="Android.Util.SizeF"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Util.SizeF(Size size)
        {
            return new Android.Util.SizeF(size.Width, size.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Util.SizeF"/> to <see cref="Size"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Size(Android.Util.SizeF size)
        {
            return new Size(size.Width, size.Height);
        }
    }
}
