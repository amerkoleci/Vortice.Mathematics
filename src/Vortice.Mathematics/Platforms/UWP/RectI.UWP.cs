// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using WindowsRect = Windows.Foundation.Rect;

namespace Vortice.Mathematics
{
    public partial struct RectI
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="RectI"/> to <see cref="WindowsRect"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WindowsRect(RectI rect)
        {
            return new WindowsRect(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="WindowsRect"/> to <see cref="RectI"/>.
        /// </summary>
        /// <param name="rect">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectI(WindowsRect rect)
        {
            if (rect.X > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.X));

            if (rect.Y > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Y));

            if (rect.Right > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Right));

            if (rect.Bottom > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rect.Bottom));

            return new RectI((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);
        }
    }
}
