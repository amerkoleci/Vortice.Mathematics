// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;

#if SUPPORTS_MATHF
[assembly: TypeForwardedTo(typeof(System.MathF))]
#else

namespace System
{
    internal static class MathF
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float f) => (float)Math.Sqrt(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Ceiling(float f) => (float)Math.Ceiling(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(float f) => (float)Math.Round(f);
    }
}
#endif
