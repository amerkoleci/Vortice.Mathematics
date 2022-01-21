// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

[DebuggerDisplay("Width={Width}, Height={Height}")]
public struct SizeI : IEquatable<SizeI>, IFormattable
{
    /// <summary>
    /// A <see cref="SizeI"/> with all of its components set to zero.
    /// </summary>
    public static readonly SizeI Empty = default;

    public SizeI(int size = 0)
    {
        Width = size;
        Height = size;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="SizeI"/> structure.
    /// </summary>
    /// <param name="width">The width component of the size.</param>
    /// <param name="height">The height component of the size.</param>
    public SizeI(int width, int height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// The width of the size.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The height of the size.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="SizeI"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => Width == 0 && Height == 0;

    public void Deconstruct(out int width, out int height)
    {
        width = Width;
        height = Height;
    }

    /// <summary>
    /// Compares two <see cref="SizeI"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="SizeI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="SizeI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(SizeI left, SizeI right) => left.Width == right.Width && left.Height == right.Height;

    /// <summary>
    /// Compares two <see cref="SizeI"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="SizeI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="SizeI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(SizeI left, SizeI right) => (left.Width != right.Width) || (left.Height != right.Height);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is SizeI other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(SizeI other)
    {
        return Width.Equals(other.Width) && Height.Equals(other.Height);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Width, Height);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(SizeI)} {{ {nameof(Width)} = {Width.ToString(format, formatProvider)}, {nameof(Height)} = {Height.ToString(format, formatProvider)} }}";
}
