// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a floating-point RGBA color.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Color4 : IEquatable<Color4>, IFormattable
    {
        public static readonly Color4 AliceBlue = new Color4(0.941176534f, 0.972549081f, 1.0f, 1.0f);
        public static readonly Color4 AntiqueWhite = new Color4(0.980392218f, 0.921568692f, 0.843137324f, 1.0f);
        public static readonly Color4 Aqua = new Color4(0.0f, 1.0f, 1.0f, 1.0f);
        public static readonly Color4 Aquamarine = new Color4(0.498039246f, 1.0f, 0.831372619f, 1.0f);
        public static readonly Color4 Azure = new Color4(0.941176534f, 1.0f, 1.0f, 1.0f);
        public static readonly Color4 Beige = new Color4(0.960784376f, 0.960784376f, 0.862745166f, 1.0f);
        public static readonly Color4 Bisque = new Color4(1.0f, 0.894117713f, 0.768627524f, 1.0f);
        public static readonly Color4 Black = new Color4(0.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Color4 BlanchedAlmond = new Color4(1.0f, 0.921568692f, 0.803921640f, 1.0f);
        public static readonly Color4 Blue = new Color4(0.0f, 0.0f, 1.0f, 1.0f);
        public static readonly Color4 BlueViolet = new Color4(0.541176498f, 0.168627456f, 0.886274576f, 1.0f);
        public static readonly Color4 Brown = new Color4(0.647058845f, 0.164705887f, 0.164705887f, 1.0f);
        public static readonly Color4 BurlyWood = new Color4(0.870588303f, 0.721568644f, 0.529411793f, 1.0f);
        public static readonly Color4 CadetBlue = new Color4(0.372549027f, 0.619607866f, 0.627451003f, 1.0f);
        public static readonly Color4 Chartreuse = new Color4(0.498039246f, 1.0f, 0.0f, 1.0f);
        public static readonly Color4 Chocolate = new Color4(0.823529482f, 0.411764741f, 0.117647067f, 1.0f);
        public static readonly Color4 Coral = new Color4(1.0f, 0.498039246f, 0.313725501f, 1.0f);
        public static readonly Color4 CornflowerBlue = new Color4(0.392156899f, 0.584313750f, 0.929411829f, 1.0f);
        public static readonly Color4 Cornsilk = new Color4(1.0f, 0.972549081f, 0.862745166f, 1.0f);
        public static readonly Color4 Crimson = new Color4(0.862745166f, 0.078431375f, 0.235294133f, 1.0f);
        public static readonly Color4 Cyan = new Color4(0.0f, 1.0f, 1.0f, 1.0f);
        public static readonly Color4 DarkBlue = new Color4(0.0f, 0.0f, 0.545098066f, 1.0f);
        public static readonly Color4 DarkCyan = new Color4(0.0f, 0.545098066f, 0.545098066f, 1.0f);
        public static readonly Color4 DarkGoldenrod = new Color4(0.721568644f, 0.525490224f, 0.043137256f, 1.0f);
        public static readonly Color4 DarkGray = new Color4(0.662745118f, 0.662745118f, 0.662745118f, 1.0f);
        public static readonly Color4 DarkGreen = new Color4(0.0f, 0.392156899f, 0.0f, 1.0f);
        public static readonly Color4 DarkKhaki = new Color4(0.741176486f, 0.717647076f, 0.419607878f, 1.0f);
        public static readonly Color4 DarkMagenta = new Color4(0.545098066f, 0.0f, 0.545098066f, 1.0f);
        public static readonly Color4 DarkOliveGreen = new Color4(0.333333343f, 0.419607878f, 0.184313729f, 1.0f);
        public static readonly Color4 DarkOrange = new Color4(1.0f, 0.549019635f, 0.0f, 1.0f);
        public static readonly Color4 DarkOrchid = new Color4(0.600000024f, 0.196078449f, 0.800000072f, 1.0f);
        public static readonly Color4 DarkRed = new Color4(0.545098066f, 0.0f, 0.0f, 1.0f);
        public static readonly Color4 DarkSalmon = new Color4(0.913725555f, 0.588235319f, 0.478431404f, 1.0f);
        public static readonly Color4 DarkSeaGreen = new Color4(0.560784340f, 0.737254918f, 0.545098066f, 1.0f);
        public static readonly Color4 DarkSlateBlue = new Color4(0.282352954f, 0.239215702f, 0.545098066f, 1.0f);
        public static readonly Color4 DarkSlateGray = new Color4(0.184313729f, 0.309803933f, 0.309803933f, 1.0f);
        public static readonly Color4 DarkTurquoise = new Color4(0.0f, 0.807843208f, 0.819607913f, 1.0f);
        public static readonly Color4 DarkViolet = new Color4(0.580392182f, 0.0f, 0.827451050f, 1.0f);
        public static readonly Color4 DeepPink = new Color4(1.0f, 0.078431375f, 0.576470613f, 1.0f);
        public static readonly Color4 DeepSkyBlue = new Color4(0.0f, 0.749019623f, 1.0f, 1.0f);
        public static readonly Color4 DimGray = new Color4(0.411764741f, 0.411764741f, 0.411764741f, 1.0f);
        public static readonly Color4 DodgerBlue = new Color4(0.117647067f, 0.564705908f, 1.0f, 1.0f);
        public static readonly Color4 Firebrick = new Color4(0.698039234f, 0.133333340f, 0.133333340f, 1.0f);
        public static readonly Color4 FloralWhite = new Color4(1.0f, 0.980392218f, 0.941176534f, 1.0f);
        public static readonly Color4 ForestGreen = new Color4(0.133333340f, 0.545098066f, 0.133333340f, 1.0f);
        public static readonly Color4 Fuchsia = new Color4(1.0f, 0.0f, 1.0f, 1.0f);
        public static readonly Color4 Gainsboro = new Color4(0.862745166f, 0.862745166f, 0.862745166f, 1.0f);
        public static readonly Color4 GhostWhite = new Color4(0.972549081f, 0.972549081f, 1.0f, 1.0f);
        public static readonly Color4 Gold = new Color4(1.0f, 0.843137324f, 0.0f, 1.0f);
        public static readonly Color4 Goldenrod = new Color4(0.854902029f, 0.647058845f, 0.125490203f, 1.0f);
        public static readonly Color4 Gray = new Color4(0.501960814f, 0.501960814f, 0.501960814f, 1.0f);
        public static readonly Color4 Green = new Color4(0.0f, 0.501960814f, 0.0f, 1.0f);
        public static readonly Color4 GreenYellow = new Color4(0.678431392f, 1.0f, 0.184313729f, 1.0f);
        public static readonly Color4 Honeydew = new Color4(0.941176534f, 1.0f, 0.941176534f, 1.0f);
        public static readonly Color4 HotPink = new Color4(1.0f, 0.411764741f, 0.705882370f, 1.0f);
        public static readonly Color4 IndianRed = new Color4(0.803921640f, 0.360784322f, 0.360784322f, 1.0f);
        public static readonly Color4 Indigo = new Color4(0.294117659f, 0.0f, 0.509803951f, 1.0f);
        public static readonly Color4 Ivory = new Color4(1.0f, 1.0f, 0.941176534f, 1.0f);
        public static readonly Color4 Khaki = new Color4(0.941176534f, 0.901960850f, 0.549019635f, 1.0f);
        public static readonly Color4 Lavender = new Color4(0.901960850f, 0.901960850f, 0.980392218f, 1.0f);
        public static readonly Color4 LavenderBlush = new Color4(1.0f, 0.941176534f, 0.960784376f, 1.0f);
        public static readonly Color4 LawnGreen = new Color4(0.486274540f, 0.988235354f, 0.0f, 1.0f);
        public static readonly Color4 LemonChiffon = new Color4(1.0f, 0.980392218f, 0.803921640f, 1.0f);
        public static readonly Color4 LightBlue = new Color4(0.678431392f, 0.847058892f, 0.901960850f, 1.0f);
        public static readonly Color4 LightCoral = new Color4(0.941176534f, 0.501960814f, 0.501960814f, 1.0f);
        public static readonly Color4 LightCyan = new Color4(0.878431439f, 1.0f, 1.0f, 1.0f);
        public static readonly Color4 LightGoldenrodYellow = new Color4(0.980392218f, 0.980392218f, 0.823529482f, 1.0f);
        public static readonly Color4 LightGreen = new Color4(0.564705908f, 0.933333397f, 0.564705908f, 1.0f);
        public static readonly Color4 LightGray = new Color4(0.827451050f, 0.827451050f, 0.827451050f, 1.0f);
        public static readonly Color4 LightPink = new Color4(1.0f, 0.713725507f, 0.756862819f, 1.0f);
        public static readonly Color4 LightSalmon = new Color4(1.0f, 0.627451003f, 0.478431404f, 1.0f);
        public static readonly Color4 LightSeaGreen = new Color4(0.125490203f, 0.698039234f, 0.666666687f, 1.0f);
        public static readonly Color4 LightSkyBlue = new Color4(0.529411793f, 0.807843208f, 0.980392218f, 1.0f);
        public static readonly Color4 LightSlateGray = new Color4(0.466666698f, 0.533333361f, 0.600000024f, 1.0f);
        public static readonly Color4 LightSteelBlue = new Color4(0.690196097f, 0.768627524f, 0.870588303f, 1.0f);
        public static readonly Color4 LightYellow = new Color4(1.0f, 1.0f, 0.878431439f, 1.0f);
        public static readonly Color4 Lime = new Color4(0.0f, 1.0f, 0.0f, 1.0f);
        public static readonly Color4 LimeGreen = new Color4(0.196078449f, 0.803921640f, 0.196078449f, 1.0f);
        public static readonly Color4 Linen = new Color4(0.980392218f, 0.941176534f, 0.901960850f, 1.0f);
        public static readonly Color4 Magenta = new Color4(1.0f, 0.0f, 1.0f, 1.0f);
        public static readonly Color4 Maroon = new Color4(0.501960814f, 0.0f, 0.0f, 1.0f);
        public static readonly Color4 MediumAquamarine = new Color4(0.400000036f, 0.803921640f, 0.666666687f, 1.0f);
        public static readonly Color4 MediumBlue = new Color4(0.0f, 0.0f, 0.803921640f, 1.0f);
        public static readonly Color4 MediumOrchid = new Color4(0.729411781f, 0.333333343f, 0.827451050f, 1.0f);
        public static readonly Color4 MediumPurple = new Color4(0.576470613f, 0.439215720f, 0.858823597f, 1.0f);
        public static readonly Color4 MediumSeaGreen = new Color4(0.235294133f, 0.701960802f, 0.443137288f, 1.0f);
        public static readonly Color4 MediumSlateBlue = new Color4(0.482352972f, 0.407843173f, 0.933333397f, 1.0f);
        public static readonly Color4 MediumSpringGreen = new Color4(0.0f, 0.980392218f, 0.603921592f, 1.0f);
        public static readonly Color4 MediumTurquoise = new Color4(0.282352954f, 0.819607913f, 0.800000072f, 1.0f);
        public static readonly Color4 MediumVioletRed = new Color4(0.780392230f, 0.082352944f, 0.521568656f, 1.0f);
        public static readonly Color4 MidnightBlue = new Color4(0.098039225f, 0.098039225f, 0.439215720f, 1.0f);
        public static readonly Color4 MintCream = new Color4(0.960784376f, 1.0f, 0.980392218f, 1.0f);
        public static readonly Color4 MistyRose = new Color4(1.0f, 0.894117713f, 0.882353008f, 1.0f);
        public static readonly Color4 Moccasin = new Color4(1.0f, 0.894117713f, 0.709803939f, 1.0f);
        public static readonly Color4 NavajoWhite = new Color4(1.0f, 0.870588303f, 0.678431392f, 1.0f);
        public static readonly Color4 Navy = new Color4(0.0f, 0.0f, 0.501960814f, 1.0f);
        public static readonly Color4 OldLace = new Color4(0.992156923f, 0.960784376f, 0.901960850f, 1.0f);
        public static readonly Color4 Olive = new Color4(0.501960814f, 0.501960814f, 0.0f, 1.0f);
        public static readonly Color4 OliveDrab = new Color4(0.419607878f, 0.556862772f, 0.137254909f, 1.0f);
        public static readonly Color4 Orange = new Color4(1.0f, 0.647058845f, 0.0f, 1.0f);
        public static readonly Color4 OrangeRed = new Color4(1.0f, 0.270588249f, 0.0f, 1.0f);
        public static readonly Color4 Orchid = new Color4(0.854902029f, 0.439215720f, 0.839215755f, 1.0f);
        public static readonly Color4 PaleGoldenrod = new Color4(0.933333397f, 0.909803987f, 0.666666687f, 1.0f);
        public static readonly Color4 PaleGreen = new Color4(0.596078455f, 0.984313786f, 0.596078455f, 1.0f);
        public static readonly Color4 PaleTurquoise = new Color4(0.686274529f, 0.933333397f, 0.933333397f, 1.0f);
        public static readonly Color4 PaleVioletRed = new Color4(0.858823597f, 0.439215720f, 0.576470613f, 1.0f);
        public static readonly Color4 PapayaWhip = new Color4(1.0f, 0.937254965f, 0.835294187f, 1.0f);
        public static readonly Color4 PeachPuff = new Color4(1.0f, 0.854902029f, 0.725490212f, 1.0f);
        public static readonly Color4 Peru = new Color4(0.803921640f, 0.521568656f, 0.247058839f, 1.0f);
        public static readonly Color4 Pink = new Color4(1.0f, 0.752941251f, 0.796078503f, 1.0f);
        public static readonly Color4 Plum = new Color4(0.866666734f, 0.627451003f, 0.866666734f, 1.0f);
        public static readonly Color4 PowderBlue = new Color4(0.690196097f, 0.878431439f, 0.901960850f, 1.0f);
        public static readonly Color4 Purple = new Color4(0.501960814f, 0.0f, 0.501960814f, 1.0f);
        public static readonly Color4 Red = new Color4(1.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Color4 RosyBrown = new Color4(0.737254918f, 0.560784340f, 0.560784340f, 1.0f);
        public static readonly Color4 RoyalBlue = new Color4(0.254901975f, 0.411764741f, 0.882353008f, 1.0f);
        public static readonly Color4 SaddleBrown = new Color4(0.545098066f, 0.270588249f, 0.074509807f, 1.0f);
        public static readonly Color4 Salmon = new Color4(0.980392218f, 0.501960814f, 0.447058856f, 1.0f);
        public static readonly Color4 SandyBrown = new Color4(0.956862807f, 0.643137276f, 0.376470625f, 1.0f);
        public static readonly Color4 SeaGreen = new Color4(0.180392161f, 0.545098066f, 0.341176480f, 1.0f);
        public static readonly Color4 SeaShell = new Color4(1.0f, 0.960784376f, 0.933333397f, 1.0f);
        public static readonly Color4 Sienna = new Color4(0.627451003f, 0.321568638f, 0.176470593f, 1.0f);
        public static readonly Color4 Silver = new Color4(0.752941251f, 0.752941251f, 0.752941251f, 1.0f);
        public static readonly Color4 SkyBlue = new Color4(0.529411793f, 0.807843208f, 0.921568692f, 1.0f);
        public static readonly Color4 SlateBlue = new Color4(0.415686309f, 0.352941185f, 0.803921640f, 1.0f);
        public static readonly Color4 SlateGray = new Color4(0.439215720f, 0.501960814f, 0.564705908f, 1.0f);
        public static readonly Color4 Snow = new Color4(1.0f, 0.980392218f, 0.980392218f, 1.0f);
        public static readonly Color4 SpringGreen = new Color4(0.0f, 1.0f, 0.498039246f, 1.0f);
        public static readonly Color4 SteelBlue = new Color4(0.274509817f, 0.509803951f, 0.705882370f, 1.0f);
        public static readonly Color4 Tan = new Color4(0.823529482f, 0.705882370f, 0.549019635f, 1.0f);
        public static readonly Color4 Teal = new Color4(0.0f, 0.501960814f, 0.501960814f, 1.0f);
        public static readonly Color4 Thistle = new Color4(0.847058892f, 0.749019623f, 0.847058892f, 1.0f);
        public static readonly Color4 Tomato = new Color4(1.0f, 0.388235331f, 0.278431386f, 1.0f);
        public static readonly Color4 Transparent = new Color4(0.0f, 0.0f, 0.0f, 0.0f);
        public static readonly Color4 Turquoise = new Color4(0.250980407f, 0.878431439f, 0.815686345f, 1.0f);
        public static readonly Color4 Violet = new Color4(0.933333397f, 0.509803951f, 0.933333397f, 1.0f);
        public static readonly Color4 Wheat = new Color4(0.960784376f, 0.870588303f, 0.701960802f, 1.0f);
        public static readonly Color4 White = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        public static readonly Color4 WhiteSmoke = new Color4(0.960784376f, 0.960784376f, 0.960784376f, 1.0f);
        public static readonly Color4 Yellow = new Color4(1.0f, 1.0f, 0.0f, 1.0f);
        public static readonly Color4 YellowGreen = new Color4(0.603921592f, 0.803921640f, 0.196078449f, 1.0f);

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Color4(float value)
        {
            A = R = G = B = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        public Color4(float red, float green, float blue)
        {
            R = red;
            G = green;
            B = blue;
            A = 1.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(float red, float green, float blue, float alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The red, green, blue, and alpha components of the color.</param>
        public Color4(Vector4 value)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
            A = value.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The red, green, and blue components of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(Vector3 value, float alpha)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
            A = alpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The red, green, and blue components of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(Color3 value, float alpha)
        {
            R = value.R;
            G = value.G;
            B = value.B;
            A = alpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="color"><see cref="Color"/> used to initialize the color.</param>
        public Color4(Color color)
        {
            R = color.R / 255.0f;
            G = color.G / 255.0f;
            B = color.B / 255.0f;
            A = color.A / 255.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="color"><see cref="System.Drawing.Color"/> used to initialize the color.</param>
        public Color4(System.Drawing.Color color)
        {
            R = color.R / 255.0f;
            G = color.G / 255.0f;
            B = color.B / 255.0f;
            A = color.A / 255.0f;
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

        /// <summary>
        /// Alpha component of the color.
        /// </summary>
        public float A { get; }

        public void Deconstruct(out float r, out float g, out float b, out float a)
        {
            r = R;
            g = G;
            b = B;
            a = A;
        }

        /// <summary>
        /// Converts the color into a packed integer.
        /// </summary>
        /// <returns>A packed integer containing all four color components.</returns>
        public void ToBgra(out byte r, out byte g, out byte b, out byte a)
        {
            r = (byte)(R * 255.0f);
            g = (byte)(G * 255.0f);
            b = (byte)(B * 255.0f);
            a = (byte)(A * 255.0f);
        }

        /// <summary>
        /// Converts the color to <see cref="Vector3"/>.
        /// </summary>
        /// <returns>An instance of <see cref="Vector3"/> with R, G, B component.</returns>
        public Vector3 ToVector3() => new Vector3(R, G, B);

        /// <summary>
        /// Converts the color to <see cref="Vector4"/>
        /// </summary>
        /// <returns>An instance of <see cref="Vector4"/> with R, G, B, A component.</returns>
        public Vector4 ToVector4() => new Vector4(R, G, B, A);

        /// <summary>
        /// Creates an array containing the elements of the color.
        /// </summary>
        /// <returns>A four-element array containing the components of the color.</returns>
        public float[] ToArray() => new[] { R, G, B, A };

        /// <summary>
        /// Performs an explicit conversion from <see cref="Color4"/> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector3(Color4 value) => new Vector3(value.R, value.G, value.B);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Color4"/> to <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector4(Color4 value) => new Vector4(value.R, value.G, value.B, value.A);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Vector3"/> to <see cref="Color4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color4(Vector3 value) => new Color4(value.X, value.Y, value.Z, 1.0f);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="Color4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color4(Vector4 value) => new Color4(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Performs an explicit conversion from <see cref="System.Drawing.Color"/> to <see cref="Color4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color4(System.Drawing.Color value) => new Color4(value);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="Color4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator System.Drawing.Color(Color4 value)
        {
            value.ToBgra(out byte red, out byte green, out byte blue, out byte alpha);
            return System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is Color4 value && Equals(value);

        /// <summary>
        /// Determines whether the specified <see cref="Color4"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Color4"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Color4 other)
        {
            return R.Equals(other.R)
                && G.Equals(other.G)
                && B.Equals(other.B)
                && A.Equals(other.A);
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
        public static bool operator ==(Color4 left, Color4 right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="Color4"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Color4"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Color4"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Color4 left, Color4 right) => !left.Equals(right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(R);
                hashCode.Add(G);
                hashCode.Add(B);
                hashCode.Add(A);
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
                .Append(B.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(A.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
