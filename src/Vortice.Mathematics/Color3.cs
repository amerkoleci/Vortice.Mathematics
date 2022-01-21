// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Vortice.Mathematics;

/// <summary>
/// Represents a floating-point RGB color.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly struct Color3 : IEquatable<Color3>, IFormattable
{
    public static readonly Color3 AliceBlue = new Color3(0.941176534f, 0.972549081f, 1.0f);
    public static readonly Color3 AntiqueWhite = new Color3(0.980392218f, 0.921568692f, 0.843137324f);
    public static readonly Color3 Aqua = new Color3(0.0f, 1.0f, 1.0f);
    public static readonly Color3 Aquamarine = new Color3(0.498039246f, 1.0f, 0.831372619f);
    public static readonly Color3 Azure = new Color3(0.941176534f, 1.0f, 1.0f);
    public static readonly Color3 Beige = new Color3(0.960784376f, 0.960784376f, 0.862745166f);
    public static readonly Color3 Bisque = new Color3(1.0f, 0.894117713f, 0.768627524f);
    public static readonly Color3 Black = new Color3(0.0f, 0.0f, 0.0f);
    public static readonly Color3 BlanchedAlmond = new Color3(1.0f, 0.921568692f, 0.803921640f);
    public static readonly Color3 Blue = new Color3(0.0f, 0.0f, 1.0f);
    public static readonly Color3 BlueViolet = new Color3(0.541176498f, 0.168627456f, 0.886274576f);
    public static readonly Color3 Brown = new Color3(0.647058845f, 0.164705887f, 0.164705887f);
    public static readonly Color3 BurlyWood = new Color3(0.870588303f, 0.721568644f, 0.529411793f);
    public static readonly Color3 CadetBlue = new Color3(0.372549027f, 0.619607866f, 0.627451003f);
    public static readonly Color3 Chartreuse = new Color3(0.498039246f, 1.0f, 0.0f);
    public static readonly Color3 Chocolate = new Color3(0.823529482f, 0.411764741f, 0.117647067f);
    public static readonly Color3 Coral = new Color3(1.0f, 0.498039246f, 0.313725501f);
    public static readonly Color3 CornflowerBlue = new Color3(0.392156899f, 0.584313750f, 0.929411829f);
    public static readonly Color3 Cornsilk = new Color3(1.0f, 0.972549081f, 0.862745166f);
    public static readonly Color3 Crimson = new Color3(0.862745166f, 0.078431375f, 0.235294133f);
    public static readonly Color3 Cyan = new Color3(0.0f, 1.0f, 1.0f);
    public static readonly Color3 DarkBlue = new Color3(0.0f, 0.0f, 0.545098066f);
    public static readonly Color3 DarkCyan = new Color3(0.0f, 0.545098066f, 0.545098066f);
    public static readonly Color3 DarkGoldenrod = new Color3(0.721568644f, 0.525490224f, 0.043137256f);
    public static readonly Color3 DarkGray = new Color3(0.662745118f, 0.662745118f, 0.662745118f);
    public static readonly Color3 DarkGreen = new Color3(0.0f, 0.392156899f, 0.0f);
    public static readonly Color3 DarkKhaki = new Color3(0.741176486f, 0.717647076f, 0.419607878f);
    public static readonly Color3 DarkMagenta = new Color3(0.545098066f, 0.0f, 0.545098066f);
    public static readonly Color3 DarkOliveGreen = new Color3(0.333333343f, 0.419607878f, 0.184313729f);
    public static readonly Color3 DarkOrange = new Color3(1.0f, 0.549019635f, 0.0f);
    public static readonly Color3 DarkOrchid = new Color3(0.600000024f, 0.196078449f, 0.800000072f);
    public static readonly Color3 DarkRed = new Color3(0.545098066f, 0.0f, 0.0f);
    public static readonly Color3 DarkSalmon = new Color3(0.913725555f, 0.588235319f, 0.478431404f);
    public static readonly Color3 DarkSeaGreen = new Color3(0.560784340f, 0.737254918f, 0.545098066f);
    public static readonly Color3 DarkSlateBlue = new Color3(0.282352954f, 0.239215702f, 0.545098066f);
    public static readonly Color3 DarkSlateGray = new Color3(0.184313729f, 0.309803933f, 0.309803933f);
    public static readonly Color3 DarkTurquoise = new Color3(0.0f, 0.807843208f, 0.819607913f);
    public static readonly Color3 DarkViolet = new Color3(0.580392182f, 0.0f, 0.827451050f);
    public static readonly Color3 DeepPink = new Color3(1.0f, 0.078431375f, 0.576470613f);
    public static readonly Color3 DeepSkyBlue = new Color3(0.0f, 0.749019623f, 1.0f);
    public static readonly Color3 DimGray = new Color3(0.411764741f, 0.411764741f, 0.411764741f);
    public static readonly Color3 DodgerBlue = new Color3(0.117647067f, 0.564705908f, 1.0f);
    public static readonly Color3 Firebrick = new Color3(0.698039234f, 0.133333340f, 0.133333340f);
    public static readonly Color3 FloralWhite = new Color3(1.0f, 0.980392218f, 0.941176534f);
    public static readonly Color3 ForestGreen = new Color3(0.133333340f, 0.545098066f, 0.133333340f);
    public static readonly Color3 Fuchsia = new Color3(1.0f, 0.0f, 1.0f);
    public static readonly Color3 Gainsboro = new Color3(0.862745166f, 0.862745166f, 0.862745166f);
    public static readonly Color3 GhostWhite = new Color3(0.972549081f, 0.972549081f, 1.0f);
    public static readonly Color3 Gold = new Color3(1.0f, 0.843137324f, 0.0f);
    public static readonly Color3 Goldenrod = new Color3(0.854902029f, 0.647058845f, 0.125490203f);
    public static readonly Color3 Gray = new Color3(0.501960814f, 0.501960814f, 0.501960814f);
    public static readonly Color3 Green = new Color3(0.0f, 0.501960814f, 0.0f);
    public static readonly Color3 GreenYellow = new Color3(0.678431392f, 1.0f, 0.184313729f);
    public static readonly Color3 Honeydew = new Color3(0.941176534f, 1.0f, 0.941176534f);
    public static readonly Color3 HotPink = new Color3(1.0f, 0.411764741f, 0.705882370f);
    public static readonly Color3 IndianRed = new Color3(0.803921640f, 0.360784322f, 0.360784322f);
    public static readonly Color3 Indigo = new Color3(0.294117659f, 0.0f, 0.509803951f);
    public static readonly Color3 Ivory = new Color3(1.0f, 1.0f, 0.941176534f);
    public static readonly Color3 Khaki = new Color3(0.941176534f, 0.901960850f, 0.549019635f);
    public static readonly Color3 Lavender = new Color3(0.901960850f, 0.901960850f, 0.980392218f);
    public static readonly Color3 LavenderBlush = new Color3(1.0f, 0.941176534f, 0.960784376f);
    public static readonly Color3 LawnGreen = new Color3(0.486274540f, 0.988235354f, 0.0f);
    public static readonly Color3 LemonChiffon = new Color3(1.0f, 0.980392218f, 0.803921640f);
    public static readonly Color3 LightBlue = new Color3(0.678431392f, 0.847058892f, 0.901960850f);
    public static readonly Color3 LightCoral = new Color3(0.941176534f, 0.501960814f, 0.501960814f);
    public static readonly Color3 LightCyan = new Color3(0.878431439f, 1.0f, 1.0f);
    public static readonly Color3 LightGoldenrodYellow = new Color3(0.980392218f, 0.980392218f, 0.823529482f);
    public static readonly Color3 LightGreen = new Color3(0.564705908f, 0.933333397f, 0.564705908f);
    public static readonly Color3 LightGray = new Color3(0.827451050f, 0.827451050f, 0.827451050f);
    public static readonly Color3 LightPink = new Color3(1.0f, 0.713725507f, 0.756862819f);
    public static readonly Color3 LightSalmon = new Color3(1.0f, 0.627451003f, 0.478431404f);
    public static readonly Color3 LightSeaGreen = new Color3(0.125490203f, 0.698039234f, 0.666666687f);
    public static readonly Color3 LightSkyBlue = new Color3(0.529411793f, 0.807843208f, 0.980392218f);
    public static readonly Color3 LightSlateGray = new Color3(0.466666698f, 0.533333361f, 0.600000024f);
    public static readonly Color3 LightSteelBlue = new Color3(0.690196097f, 0.768627524f, 0.870588303f);
    public static readonly Color3 LightYellow = new Color3(1.0f, 1.0f, 0.878431439f);
    public static readonly Color3 Lime = new Color3(0.0f, 1.0f, 0.0f);
    public static readonly Color3 LimeGreen = new Color3(0.196078449f, 0.803921640f, 0.196078449f);
    public static readonly Color3 Linen = new Color3(0.980392218f, 0.941176534f, 0.901960850f);
    public static readonly Color3 Magenta = new Color3(1.0f, 0.0f, 1.0f);
    public static readonly Color3 Maroon = new Color3(0.501960814f, 0.0f, 0.0f);
    public static readonly Color3 MediumAquamarine = new Color3(0.400000036f, 0.803921640f, 0.666666687f);
    public static readonly Color3 MediumBlue = new Color3(0.0f, 0.0f, 0.803921640f);
    public static readonly Color3 MediumOrchid = new Color3(0.729411781f, 0.333333343f, 0.827451050f);
    public static readonly Color3 MediumPurple = new Color3(0.576470613f, 0.439215720f, 0.858823597f);
    public static readonly Color3 MediumSeaGreen = new Color3(0.235294133f, 0.701960802f, 0.443137288f);
    public static readonly Color3 MediumSlateBlue = new Color3(0.482352972f, 0.407843173f, 0.933333397f);
    public static readonly Color3 MediumSpringGreen = new Color3(0.0f, 0.980392218f, 0.603921592f);
    public static readonly Color3 MediumTurquoise = new Color3(0.282352954f, 0.819607913f, 0.800000072f);
    public static readonly Color3 MediumVioletRed = new Color3(0.780392230f, 0.082352944f, 0.521568656f);
    public static readonly Color3 MidnightBlue = new Color3(0.098039225f, 0.098039225f, 0.439215720f);
    public static readonly Color3 MintCream = new Color3(0.960784376f, 1.0f, 0.980392218f);
    public static readonly Color3 MistyRose = new Color3(1.0f, 0.894117713f, 0.882353008f);
    public static readonly Color3 Moccasin = new Color3(1.0f, 0.894117713f, 0.709803939f);
    public static readonly Color3 NavajoWhite = new Color3(1.0f, 0.870588303f, 0.678431392f);
    public static readonly Color3 Navy = new Color3(0.0f, 0.0f, 0.501960814f);
    public static readonly Color3 OldLace = new Color3(0.992156923f, 0.960784376f, 0.901960850f);
    public static readonly Color3 Olive = new Color3(0.501960814f, 0.501960814f, 0.0f);
    public static readonly Color3 OliveDrab = new Color3(0.419607878f, 0.556862772f, 0.137254909f);
    public static readonly Color3 Orange = new Color3(1.0f, 0.647058845f, 0.0f);
    public static readonly Color3 OrangeRed = new Color3(1.0f, 0.270588249f, 0.0f);
    public static readonly Color3 Orchid = new Color3(0.854902029f, 0.439215720f, 0.839215755f);
    public static readonly Color3 PaleGoldenrod = new Color3(0.933333397f, 0.909803987f, 0.666666687f);
    public static readonly Color3 PaleGreen = new Color3(0.596078455f, 0.984313786f, 0.596078455f);
    public static readonly Color3 PaleTurquoise = new Color3(0.686274529f, 0.933333397f, 0.933333397f);
    public static readonly Color3 PaleVioletRed = new Color3(0.858823597f, 0.439215720f, 0.576470613f);
    public static readonly Color3 PapayaWhip = new Color3(1.0f, 0.937254965f, 0.835294187f);
    public static readonly Color3 PeachPuff = new Color3(1.0f, 0.854902029f, 0.725490212f);
    public static readonly Color3 Peru = new Color3(0.803921640f, 0.521568656f, 0.247058839f);
    public static readonly Color3 Pink = new Color3(1.0f, 0.752941251f, 0.796078503f);
    public static readonly Color3 Plum = new Color3(0.866666734f, 0.627451003f, 0.866666734f);
    public static readonly Color3 PowderBlue = new Color3(0.690196097f, 0.878431439f, 0.901960850f);
    public static readonly Color3 Purple = new Color3(0.501960814f, 0.0f, 0.501960814f);
    public static readonly Color3 Red = new Color3(1.0f, 0.0f, 0.0f);
    public static readonly Color3 RosyBrown = new Color3(0.737254918f, 0.560784340f, 0.560784340f);
    public static readonly Color3 RoyalBlue = new Color3(0.254901975f, 0.411764741f, 0.882353008f);
    public static readonly Color3 SaddleBrown = new Color3(0.545098066f, 0.270588249f, 0.074509807f);
    public static readonly Color3 Salmon = new Color3(0.980392218f, 0.501960814f, 0.447058856f);
    public static readonly Color3 SandyBrown = new Color3(0.956862807f, 0.643137276f, 0.376470625f);
    public static readonly Color3 SeaGreen = new Color3(0.180392161f, 0.545098066f, 0.341176480f);
    public static readonly Color3 SeaShell = new Color3(1.0f, 0.960784376f, 0.933333397f);
    public static readonly Color3 Sienna = new Color3(0.627451003f, 0.321568638f, 0.176470593f);
    public static readonly Color3 Silver = new Color3(0.752941251f, 0.752941251f, 0.752941251f);
    public static readonly Color3 SkyBlue = new Color3(0.529411793f, 0.807843208f, 0.921568692f);
    public static readonly Color3 SlateBlue = new Color3(0.415686309f, 0.352941185f, 0.803921640f);
    public static readonly Color3 SlateGray = new Color3(0.439215720f, 0.501960814f, 0.564705908f);
    public static readonly Color3 Snow = new Color3(1.0f, 0.980392218f, 0.980392218f);
    public static readonly Color3 SpringGreen = new Color3(0.0f, 1.0f, 0.498039246f);
    public static readonly Color3 SteelBlue = new Color3(0.274509817f, 0.509803951f, 0.705882370f);
    public static readonly Color3 Tan = new Color3(0.823529482f, 0.705882370f, 0.549019635f);
    public static readonly Color3 Teal = new Color3(0.0f, 0.501960814f, 0.501960814f);
    public static readonly Color3 Thistle = new Color3(0.847058892f, 0.749019623f, 0.847058892f);
    public static readonly Color3 Tomato = new Color3(1.0f, 0.388235331f, 0.278431386f);
    public static readonly Color3 Turquoise = new Color3(0.250980407f, 0.878431439f, 0.815686345f);
    public static readonly Color3 Violet = new Color3(0.933333397f, 0.509803951f, 0.933333397f);
    public static readonly Color3 Wheat = new Color3(0.960784376f, 0.870588303f, 0.701960802f);
    public static readonly Color3 White = new Color3(1.0f, 1.0f, 1.0f);
    public static readonly Color3 WhiteSmoke = new Color3(0.960784376f, 0.960784376f, 0.960784376f);
    public static readonly Color3 Yellow = new Color3(1.0f, 1.0f, 0.0f);
    public static readonly Color3 YellowGreen = new Color3(0.603921592f, 0.803921640f, 0.196078449f);

    /// <summary>
    /// Initializes a new instance of the <see cref="Color3"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Color3(float value)
    {
        R = G = B = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color3"/> struct.
    /// </summary>
    /// <param name="red">The red component of the color.</param>
    /// <param name="green">The green component of the color.</param>
    /// <param name="blue">The blue component of the color.</param>
    public Color3(float red, float green, float blue)
    {
        R = red;
        G = green;
        B = blue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color3"/> struct.
    /// </summary>
    /// <param name="value">The red, green, and blue components of the color.</param>
    public Color3(Vector3 value)
    {
        R = value.X;
        G = value.Y;
        B = value.Z;
    }

    /// <summary>
    /// Red component of the color.
    /// </summary>
    public float R { get; }

    /// <summary>
    /// Green component of the color.
    /// </summary>
    public float G { get; }

    /// <summary>
    /// Blue component of the color.
    /// </summary>
    public float B { get; }

    public void Deconstruct(out float r, out float g, out float b)
    {
        r = R;
        g = G;
        b = B;
    }

    /// <summary>
    /// Lerps between color source and destination by a supplied amount
    /// </summary>
    /// <param name="start"><see cref="Color3"/> is the initial color</param>
    /// <param name="end"><see cref="Color3"/> is the target color</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    public static Color3 Lerp(in Color3 start, in Color3 end, float amount)
    {
        return new Color3(
            MathHelper.Lerp(start.R, end.R, amount),
            MathHelper.Lerp(start.G, end.G, amount),
            MathHelper.Lerp(start.B, end.B, amount)
            );
    }

    /// <summary>
    /// Lerps between color source and destination by a supplied amount
    /// </summary>
    /// <param name="start"><see cref="Color3"/> is the initial color</param>
    /// <param name="end"><see cref="Color3"/> is the target color</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    /// <param name="result">return <see cref="Color3"/> of the lerp value</param>
    public static void Lerp(in Color3 start, in Color3 end, float amount, out Color3 result)
    {
        result = new Color3(
            MathHelper.Lerp(start.R, end.R, amount),
            MathHelper.Lerp(start.G, end.G, amount),
            MathHelper.Lerp(start.B, end.B, amount)
            );
    }

    /// <summary>
    /// Converts the color into a three component vector.
    /// </summary>
    /// <returns>A three component vector containing the red, green, and blue components of the color.</returns>
    public Vector3 ToVector3() => new Vector3(R, G, B);

    /// <summary>
    /// Creates an array containing the elements of the color.
    /// </summary>
    /// <returns>A four-element array containing the components of the color.</returns>
    public float[] ToArray() => new[] { R, G, B };

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Color3 value && Equals(ref value);

    /// <summary>
    /// Determines whether the specified <see cref="Color3"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Color3"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Color3 other) => Equals(ref other);

    /// <summary>
    /// Determines whether the specified <see cref="Color3"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Color3"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ref Color3 other)
    {
        return R.Equals(other.R)
            && G.Equals(other.G)
            && B.Equals(other.B);
    }

    /// <summary>
    /// Compares two <see cref="Color3"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Color3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Color3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Color3 left, Color3 right) => left.Equals(ref right);

    /// <summary>
    /// Compares two <see cref="Color3"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Color3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Color3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Color3 left, Color3 right) => !left.Equals(ref right);

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        {
            hashCode.Add(R);
            hashCode.Add(G);
            hashCode.Add(B);
        }
        return hashCode.ToHashCode();
    }

    /// <inheritdoc/>
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        string? separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

        return new StringBuilder()
            .Append('<')
            .Append(R.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(G.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(B.ToString(format, formatProvider))
            .Append('>')
            .ToString();
    }
}
