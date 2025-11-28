// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;

namespace Vortice.Mathematics;

/// <summary>
/// Represents a floating-point RGBA color.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly struct Color4 : IEquatable<Color4>, IFormattable
{
    private const float EPSILON = 0.001f;

    private readonly Vector128<float> _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Color4(float value)
    {
        _value = Vector128.Create(value, value, value, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="red">The red component of the color.</param>
    /// <param name="green">The green component of the color.</param>
    /// <param name="blue">The blue component of the color.</param>
    /// <param name="alpha">The alpha component of the color.</param>
    public Color4(float red, float green, float blue, float alpha = 1.0f)
    {
        _value = Vector128.Create(red, green, blue, alpha);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="value">The red, green, blue, and alpha components of the color.</param>
    public Color4(in Vector4 value)
    {
        _value = value.AsVector128();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="value">The red, green, and blue components of the color.</param>
    /// <param name="alpha">The alpha component of the color.</param>
    public Color4(in Vector3 value, float alpha)
    {
        _value = Vector128.Create(value.X, value.Y, value.Z, alpha);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="value">The red, green, and blue components of the color.</param>
    /// <param name="alpha">The alpha component of the color.</param>
    public Color4(in Color3 value, float alpha = 1.0f)
    {
        _value = Vector128.Create(value.R, value.G, value.B, alpha);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="rgba">A packed integer containing all four color components in RGBA order.</param>
    public Color4(uint rgba)
    {
        _value = Vector128.Create((rgba & 255) / 255.0f, ((rgba >> 8) & 255) / 255.0f, ((rgba >> 16) & 255) / 255.0f, ((rgba >> 24) & 255) / 255.0f);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="rgba">A packed integer containing all four color components in RGBA order.</param>
    public Color4(int rgba)
    {
        _value = Vector128.Create((rgba & 255) / 255.0f, ((rgba >> 8) & 255) / 255.0f, ((rgba >> 16) & 255) / 255.0f, ((rgba >> 24) & 255) / 255.0f);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="color"><see cref="Color"/> used to initialize the color.</param>
    public Color4(in Color color)
    {
        _value = Vector128.Create(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="color"><see cref="ColorBgra"/> used to initialize the color.</param>
    public Color4(in ColorBgra color)
    {
        _value = Vector128.Create(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Color4(ReadOnlySpan<float> values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 4 uint values.");
        }

        this = Unsafe.ReadUnaligned<Color4>(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4" /> struct.
    /// </summary>
    /// <param name="value">The value of the vector.</param>
    public Color4(Vector128<float> value)
    {
        _value = value;
    }

    /// <summary>
    /// Red component of the color.
    /// </summary>
    public readonly float R
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value.GetX();
    }

    /// <summary>
    /// Green component of the color.
    /// </summary>
    public readonly float G
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value.GetY();
    }

    /// <summary>
    /// Blue component of the color.
    /// </summary>
    public readonly float B
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value.GetZ();
    }

    /// <summary>
    /// Alpha component of the color.
    /// </summary>
    public readonly float A
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value.GetW();
    }

    public readonly float this[int index] => GetElement(this, index);

    /// <summary>
    /// Return sum of RGB components.
    /// </summary>
    public readonly float SumRGB => R + G + B;

    /// <summary>
    /// Return average value of the RGB channels.
    /// </summary>
    public readonly float Average => (R + G + B) / 3.0f;

    /// <summary>
    /// Return the 'grayscale' representation of RGB values, as used by JPEG and PAL/NTSC among others.
    /// </summary>
    public readonly float Luma => R * 0.299f + G * 0.587f + B * 0.114f;

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public uint ToBgra()
    {
        uint a = (uint)(A * 255.0f) & 255;
        uint r = (uint)(R * 255.0f) & 255;
        uint g = (uint)(G * 255.0f) & 255;
        uint b = (uint)(B * 255.0f) & 255;

        uint value = b;
        value |= g << 8;
        value |= r << 16;
        value |= a << 24;

        return value;
    }

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public void ToBgra(out byte r, out byte g, out byte b, out byte a)
    {
        b = (byte)(B * 255.0f);
        g = (byte)(G * 255.0f);
        r = (byte)(R * 255.0f);
        a = (byte)(A * 255.0f);
    }

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public uint ToRgba()
    {
        uint r = (uint)(R * 255.0f) & 255;
        uint g = (uint)(G * 255.0f) & 255;
        uint b = (uint)(B * 255.0f) & 255;
        uint a = (uint)(A * 255.0f) & 255;

        uint value = r;
        value |= g << 8;
        value |= b << 16;
        value |= a << 24;

        return value;
    }

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public void ToRgba(out byte r, out byte g, out byte b, out byte a)
    {
        r = (byte)(R * 255.0f);
        g = (byte)(G * 255.0f);
        b = (byte)(B * 255.0f);
        a = (byte)(A * 255.0f);
    }

    /// <summary>
    /// Determines the negative RGB color value of a color.
    /// </summary>
    /// <param name="color">The color value</param>
    /// <returns>The negative color</returns>
    public static Color4 Negative(in Color4 color)
    {
        return new Color4(1.0f - color.R, 1.0f - color.G, 1.0f - color.B, color.A);
    }

    /// <summary>
    /// Blends two colors by multiplying corresponding components together.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    public static Color4 Modulate(in Color4 color1, in Color4 color2)
    {
        return Multiply(color1, color2);
    }

    /// <summary>
    /// Restricts a value to be within a specified range.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    public static Color4 Clamp(in Color4 value, in Color4 min, in Color4 max)
    {
        float alpha = value.A;
        alpha = (alpha > max.A) ? max.A : alpha;
        alpha = (alpha < min.A) ? min.A : alpha;

        float red = value.R;
        red = (red > max.R) ? max.R : red;
        red = (red < min.R) ? min.R : red;

        float green = value.G;
        green = (green > max.G) ? max.G : green;
        green = (green < min.G) ? min.G : green;

        float blue = value.B;
        blue = (blue > max.B) ? max.B : blue;
        blue = (blue < min.B) ? min.B : blue;

        return new Color4(red, green, blue, alpha);
    }

    /// <summary>
    /// Lerps between color source and destination by a supplied amount
    /// </summary>
    /// <param name="start"><see cref="Color4"/> is the initial color</param>
    /// <param name="end"><see cref="Color4"/> is the target color</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    public static Color4 Lerp(in Color4 start, in Color4 end, float amount)
    {
        return new(
            MathHelper.Lerp(start.R, end.R, amount),
            MathHelper.Lerp(start.G, end.G, amount),
            MathHelper.Lerp(start.B, end.B, amount),
            MathHelper.Lerp(start.A, end.A, amount)
            );
    }

    /// <summary>
    /// Performs a cubic interpolation between two colors.
    /// </summary>
    /// <param name="start">Start color.</param>
    /// <param name="end">End color.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    /// <returns>When the method completes, contains the cubic interpolation of the two colors.</returns>
    public static Color4 SmoothStep(in Color4 start, in Color4 end, float amount)
    {
        amount = MathHelper.SmoothStep(amount);
        return Lerp(start, end, amount);
    }

    /// <summary>
    /// Returns a color containing the smallest components of the specified colors.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>When the method completes, contains an new color composed of the largest components of the source colors.</returns>
    public static Color4 Max(in Color4 left, in Color4 right)
    {
        return new(
            (left.R > right.R) ? left.R : right.G,
            (left.G > right.G) ? left.G : right.G,
            (left.B > right.B) ? left.B : right.B,
            (left.A > right.A) ? left.A : right.A
            );
    }

    /// <summary>
    /// Returns a color containing the smallest components of the specified colors.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>When the method completes, contains an new color composed of the smallest components of the source colors.</returns>
    public static Color4 Min(in Color4 left, in Color4 right)
    {
        return new(
            (left.R < right.R) ? left.R : right.R,
            (left.G < right.G) ? left.G : right.G,
            (left.B < right.B) ? left.B : right.B,
            (left.A < right.A) ? left.A : right.A
            );
    }

    /// <summary>
    /// Computes the premultiplied value of the provided color.
    /// </summary>
    /// <param name="value">The non-premultiplied value.</param>
    /// <returns>The premultiplied result.</returns>
    public static Color4 Premultiply(in Color4 value)
    {
        return new(
            value.R * value.A,
            value.G * value.A,
            value.B * value.A,
            value.A
            );
    }

    /// <summary>
    /// Return the colorfulness relative to the brightness of a similarly illuminated white.
    /// </summary>
    /// <returns></returns>
    public float Chroma()
    {
        Bounds(out float min, out float max, true);

        return max - min;
    }

    /// <summary>
    /// Return hue mapped to range [0, 1.0).
    /// </summary>
    /// <returns></returns>
    public float Hue()
    {
        Bounds(out float min, out float max, true);
        return Hue(min, max);
    }

    /// <summary>
    /// Return saturation as defined for HSL.
    /// </summary>
    /// <returns></returns>
    public float SaturationHSL()
    {
        Bounds(out float min, out float max, true);
        return SaturationHSL(min, max);
    }

    /// <summary>
    /// Return saturation as defined for HSV.
    /// </summary>
    /// <returns></returns>
    public float SaturationHSV()
    {
        Bounds(out float min, out float max, true);
        return SaturationHSV(min, max);
    }

    /// <summary>
    /// Converts this color from sRGB space to linear space.
    /// </summary>
    /// <returns>A <see cref="Color4"/> in linear space.</returns>
    public Color4 ToLinear()
    {
        return new(
            MathHelper.SRgbToLinear(R),
            MathHelper.SRgbToLinear(G),
            MathHelper.SRgbToLinear(B),
            A);
    }

    /// <summary>
    /// Converts this color from linear space to sRGB space.
    /// </summary>
    /// <returns>A <see cref="Color4"/> in sRGB space.</returns>
    public Color4 ToSRgb()
    {
        return new(
            MathHelper.LinearToSRgb(R),
            MathHelper.LinearToSRgb(G),
            MathHelper.LinearToSRgb(B),
            A);
    }

    /// <summary>
    /// Stores the values of least and greatest RGB component at specified pointer addresses, optionally clipping those values to [0, 1] range.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="clipped"></param>
    public void Bounds(out float min, out float max, bool clipped = false)
    {
        if (R > G)
        {
            if (B > B) // r > g > b
            {
                max = R;
                min = B;
            }
            else // r > g && g <= b
            {
                max = R > B ? R : B;
                min = G;
            }
        }
        else
        {
            if (B > G) // r <= g < b
            {
                max = B;
                min = R;
            }
            else // r <= g && b <= g
            {
                max = G;
                min = R < B ? R : B;
            }
        }

        if (clipped)
        {
            max = max > 1.0f ? 1.0f : (max < 0.0f ? 0.0f : max);
            min = min > 1.0f ? 1.0f : (min < 0.0f ? 0.0f : min);
        }
    }

    /// <summary>
    /// Return HSL color-space representation as a Vector3; the RGB values are clipped before conversion but not changed in the process.
    /// </summary>
    /// <returns></returns>
    public Vector3 ToHSL()
    {
        Bounds(out float min, out float max, true);

        float h = Hue(min, max);
        float s = SaturationHSL(min, max);
        float l = (max + min) * 0.5f;

        return new Vector3(h, s, l);
    }

    /// <summary>
    /// Create new <see cref="Color4"/> from specified HSL values and alpha.
    /// </summary>
    public static Color FromHSL(float h, float s, float l, float a = 1.0f)
    {
        float c;
        if (l < 0.5f)
            c = (1.0f + (2.0f * l - 1.0f)) * s;
        else
            c = (1.0f - (2.0f * l - 1.0f)) * s;

        float m = l - 0.5f * c;

        FromHCM(h, c, m, out float r, out float g, out float b);
        return new(r, g, b, a);
    }

    /// <summary>
    /// Create new <see cref="Color4"/> from specified HSV values and alpha.
    /// </summary>
    public static Color FromHSV(float h, float s, float v, float a = 1.0f)
    {
        float c = v * s;
        float m = v - c;

        FromHCM(h, c, m, out float r, out float g, out float b);
        return new(r, g, b, a);
    }

    /// <summary>
    /// Return HSV color-space representation as a Vector3;
    /// the RGB values are clipped before conversion but not changed in the process.
    /// </summary>
    /// <returns></returns>
    public Vector3 ToHSV()
    {
        Bounds(out float min, out float max, true);

        float h = Hue(min, max);
        float s = SaturationHSV(min, max);
        float v = max;

        return new Vector3(h, s, v);
    }

    /// <summary>
    /// /// Return hue value given greatest and least RGB component, value-wise.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private float Hue(float min, float max)
    {
        float chroma = max - min;

        // If chroma equals zero, hue is undefined
        if (chroma <= MathHelper.ZeroTolerance)
            return 0.0f;

        // Calculate and return hue
        if (MathHelper.CompareEqual(G, max))
            return (B + 2.0f * chroma - R) / (6.0f * chroma);
        else if (MathHelper.CompareEqual(B, max))
            return (4.0f * chroma - G + R) / (6.0f * chroma);
        else
        {
            float r = (G - B) / (6.0f * chroma);
            return (r < 0.0f) ? 1.0f + r : ((r >= 1.0f) ? r - 1.0f : r);
        }
    }

    /// <summary>
    /// Return saturation (HSV) given greatest and least RGB component, value-wise.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private static float SaturationHSV(float min, float max)
    {
        // Avoid div-by-zero: result undefined
        if (max <= MathHelper.ZeroTolerance)
            return 0.0f;

        // Saturation equals chroma:value ratio
        return 1.0f - (min / max);
    }

    /// <summary>
    /// Return saturation (HSL) given greatest and least RGB component, value-wise.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private static float SaturationHSL(float min, float max)
    {
        // Avoid div-by-zero: result undefined
        if (max <= MathHelper.ZeroTolerance || min >= 1.0f - MathHelper.ZeroTolerance)
            return 0.0f;

        // Chroma = max - min, lightness = (max + min) * 0.5
        float hl = (max + min);
        if (hl <= 1.0f)
            return (max - min) / hl;
        else
            return (min - max) / (hl - 2.0f);
    }

    /// <summary>
    /// Calculate and set RGB values. Convenience function used by FromHSV and FromHSL to avoid code duplication.
    /// </summary>
    private static void FromHCM(float h, float c, float m, out float r, out float g, out float b)
    {
        if (h < 0.0f || h >= 1.0f)
            h -= MathF.Floor(h);

        float hs = h * 6.0f;
        float x = c * (1.0f - MathF.Abs(MathF.IEEERemainder(hs, 2.0f) - 1.0f));

        // Reconstruct r', g', b' from hue
        r = 0.0f;
        g = 0.0f;
        b = 0.0f;
        if (hs < 2.0f)
        {
            b = 0.0f;
            if (hs < 1.0f)
            {
                g = x;
                r = c;
            }
            else
            {
                g = c;
                r = x;
            }
        }
        else if (hs < 4.0f)
        {
            r = 0.0f;
            if (hs < 3.0f)
            {
                g = c;
                b = x;
            }
            else
            {
                g = x;
                b = c;
            }
        }
        else
        {
            g = 0.0f;
            if (hs < 5.0f)
            {
                r = x;
                b = c;
            }
            else
            {
                r = c;
                b = x;
            }
        }

        r += m;
        g += m;
        b += m;
    }

    public void Deconstruct(out float red, out float green, out float blue, out float alpha)
    {
        red = R;
        green = G;
        blue = B;
        alpha = A;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(float[] array)
    {
        CopyTo(array, 0);
    }

    public readonly void CopyTo(float[] array, int index)
    {
        if (array is null)
        {
            throw new NullReferenceException(nameof(array));
        }

        if ((index < 0) || (index >= array.Length))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if ((array.Length - index) < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        array[index] = R;
        array[index + 1] = G;
        array[index + 2] = B;
        array[index + 3] = A;
    }

    /// <summary>Copies the vector to the given <see cref="Span{T}" />.The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination span.</exception>
    public readonly void CopyTo(Span<float> destination)
    {
        if (destination.Length < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(destination));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(destination)), this);
    }

    /// <summary>Attempts to copy the vector to the given <see cref="Span{Int32}" />. The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <returns><see langword="true" /> if the source vector was successfully copied to <paramref name="destination" />. <see langword="false" /> if <paramref name="destination" /> is not large enough to hold the source vector.</returns>
    public readonly bool TryCopyTo(Span<float> destination)
    {
        if (destination.Length < 4)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    /// <summary>
    /// Returns a new color whose values are the product of each pair of elements in two specified colors.
    /// </summary>
    /// <param name="left">The first color.</param>
    /// <param name="right">The second color.</param>
    /// <returns>The element-wise product vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 Multiply(Color4 left, Color4 right)
    {
        Vector128<float> result = Vector128.Multiply(left._value, right._value);
        return new Color4(result);
    }

    /// <summary>Multiplies a color by a specified scalar.</summary>
    /// <param name="left">The color to multiply.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 Multiply(Color4 left, float right)
    {
        Vector128<float> result = Vector128.Multiply(left._value, right);
        return new Color4(result);
    }

    /// <summary>Multiplies a scalar value by a specified color.</summary>
    /// <param name="left">The scaled value.</param>
    /// <param name="right">The color.</param>
    /// <returns>The scaled color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 Multiply(float left, Color4 right)
    {
        Vector128<float> result = Vector128.Multiply(right._value, left);
        return new Color4(result);
    }

    /// <summary>
    /// Converts the color to <see cref="Vector3"/>.
    /// </summary>
    /// <returns>An instance of <see cref="Vector3"/> with R, G, B component.</returns>
    public Vector3 ToVector3() => new(R, G, B);

    /// <summary>
    /// Converts the color to <see cref="Vector4"/>
    /// </summary>
    /// <returns>An instance of <see cref="Vector4"/> with R, G, B, A component.</returns>
    public Vector4 ToVector4() => new(R, G, B, A);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Color4"/> to <see cref="Vector3"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector3(in Color4 value) => new(value.R, value.G, value.B);

    /// <summary>
    /// Performs an implicit conversion from <see cref="Color4"/> to <see cref="Vector4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Vector4(in Color4 value) => new(value.R, value.G, value.B, value.A);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector3"/> to <see cref="Color4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Color4(in Vector3 value) => new(value.X, value.Y, value.Z, 1.0f);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="Color4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Color4(in Vector4 value) => new(value.X, value.Y, value.Z, value.W);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Color4 other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Color4 other) => Vector128.EqualsAll(_value, other._value);

    /// <summary>
    /// Adds two colors.
    /// </summary>
    /// <param name="left">The first color to add.</param>
    /// <param name="right">The second color to add.</param>
    /// <returns>The sum of the two colors.</returns>
    public static Color4 operator +(Color4 left, Color4 right)
    {
        return new(left.R + right.R, left.G + right.G, left.B + right.B, left.A + right.A);
    }

    /// <summary>
    /// Assert a color (return it unchanged).
    /// </summary>
    /// <param name="value">The color to assert (unchanged).</param>
    /// <returns>The asserted (unchanged) color.</returns>
    public static Color4 operator +(Color4 value) => value;

    /// <summary>
    /// Subtracts two colors.
    /// </summary>
    /// <param name="left">The first color to subtract.</param>
    /// <param name="right">The second color to subtract.</param>
    /// <returns>The difference of the two colors.</returns>
    public static Color4 operator -(Color4 left, Color4 right)
    {
        return new Color4(left.R - right.R, left.G - right.G, left.B - right.B, left.A - right.A);
    }

    /// <summary>
    /// Negates a color.
    /// </summary>
    /// <param name="value">The color to negate.</param>
    /// <returns>A negated color.</returns>
    public static Color4 operator -(Color4 value)
    {
        return new Color4(-value.R, -value.G, -value.B, -value.A);
    }

    /// <summary>Computes the product of a color and a float.</summary>
    /// <param name="left">The vector to multiply by <paramref name="right" />.</param>
    /// <param name="right">The float which is used to multiply <paramref name="left" />.</param>
    /// <returns>The product of <paramref name="left" /> multipled by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 operator *(float left, Color4 right)
    {
        return Multiply(left, right);
    }

    /// <summary>Computes the product of a color and a float.</summary>
    /// <param name="left">The vector to multiply by <paramref name="right" />.</param>
    /// <param name="right">The float which is used to multiply <paramref name="left" />.</param>
    /// <returns>The product of <paramref name="left" /> multipled by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 operator *(Color4 left, float right)
    {
        Vector128<float> result = Vector128.Multiply(left._value, right);
        return new Color4(result);
    }

    /// <summary>Computes the product of two colors.</summary>
    /// <param name="left">The color to multiply by <paramref name="right" />.</param>
    /// <param name="right">The color which is used to multiply <paramref name="left" />.</param>
    /// <returns>The product of <paramref name="left" /> multipled by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 operator *(Color4 left, Color4 right)
    {
        Vector128<float> result = Vector128.Multiply(left._value, right._value);
        return new Color4(result);
    }

    /// <summary>
    /// Compares two <see cref="Color4"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Color4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Color4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Color4 left, Color4 right) => Vector128.EqualsAll(left._value, right._value);

    /// <summary>
    /// Compares two <see cref="Color4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Color4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Color4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Color4 left, Color4 right) => !Vector128.EqualsAll(left._value, right._value);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(R, G, B, A);

    /// <inheritdoc/>
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        string? separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

        return new StringBuilder(9 + separator.Length * 3)
            .Append('<')
            .Append(R.ToString(format, formatProvider))
            .Append(separator)
            .Append(' ')
            .Append(G.ToString(format, formatProvider))
            .Append(separator)
            .Append(' ')
            .Append(B.ToString(format, formatProvider))
            .Append(separator)
            .Append(' ')
            .Append(A.ToString(format, formatProvider))
            .Append('>')
            .ToString();
    }

    internal const int Count = 4;

    internal static float GetElement(Color4 vector, int index)
    {
        if (index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return GetElementUnsafe(ref vector, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float GetElementUnsafe(ref Color4 vector, int index)
    {
        Debug.Assert(index is >= 0 and < Count);

        return Unsafe.Add(ref Unsafe.As<Color4, float>(ref vector), index);
    }
}
