// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    public partial struct SizeF
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="SizeF"/> to <see cref="Android.Util.SizeF"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Android.Util.SizeF(SizeF size) =>
            new Android.Util.SizeF(size.Width, size.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Android.Util.SizeF"/> to <see cref="SizeF"/>.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SizeF(Android.Util.SizeF size) =>
            new SizeF(size.Width, size.Height);
    }
}
