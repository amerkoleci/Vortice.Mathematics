// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    public partial struct RectF
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="RectF"/> to <see cref="CoreGraphics.CGRect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator CoreGraphics.CGRect(RectF rect) =>
            new CoreGraphics.CGRect((nfloat)rect.X, (nfloat)rect.Y, (nfloat)rect.Width, (nfloat)rect.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="CoreGraphics.CGRect"/> to <see cref="RectF"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectF(CoreGraphics.CGRect rect) =>
            new RectF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
    }
}
