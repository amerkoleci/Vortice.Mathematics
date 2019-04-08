// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct SizeI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="SizeI"/> to <see cref="Android.Util.Size"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Util.Size(SizeI size)
        {
            return new Android.Util.Size(size.Width, size.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Util.Size"/> to <see cref="SizeI"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SizeI(Android.Util.Size size)
        {
            return new SizeI(size.Width, size.Height);
        }
    }
}
