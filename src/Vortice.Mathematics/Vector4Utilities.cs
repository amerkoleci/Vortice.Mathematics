// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

/// <summary>
/// Provides a set of methods to supplement or replace <see cref="Vector4" />.
/// </summary>
public static class Vector4Utilities
{
    public static Vector4 UByteMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
        }
    }

    public static Vector4 ShortMin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(-32767.0f, -32767.0f, -32767.0f, -32767.0f);
        }
    }

    public static Vector4 ShortMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(32767.0f, 32767.0f, 32767.0f, 32767.0f);
        }
    }

    public static Vector4 UShortMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(65535.0f, 65535.0f, 65535.0f, 65535.0f);
        }
    }

    /// <summary>
    /// Saturates each component of a vector to the range 0.0f to 1.0f.
    /// </summary>
    /// <param name="value">Vector to saturate.</param>
    /// <returns>Returns a vector, each of whose components are saturated.</returns>
    public static Vector4 Saturate(in Vector4 value)
    {
        return Vector4.Clamp(value, Vector4.Zero, Vector4.One);
    }

    /// <summary>
    /// Rounds each component of a vector to the nearest even integer.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Vector4 Round(in Vector4 value)
    {
        return new Vector4(
            MathF.Round(value.X),
            MathF.Round(value.Y),
            MathF.Round(value.Z),
            MathF.Round(value.W)
            );
    }
}
