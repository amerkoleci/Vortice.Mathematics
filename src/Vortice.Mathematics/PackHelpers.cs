// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Mathematics;

public static unsafe class PackHelpers
{
    public static byte ToByte(int value)
    {
        return (byte)(value < 0 ? 0 : value > 255 ? 255 : value);
    }

    public static byte ToByte(float component)
    {
        int value = (int)(component * 255.0f);
        return (byte)(value < 0 ? 0 : value > 255 ? 255 : value);
    }
}
