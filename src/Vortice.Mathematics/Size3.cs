// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Stores an ordered pair of integer numbers describing the width, height and depth of a rectangle.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly struct Size3 : IEquatable<Size3>
{
    /// <summary>
    /// A <see cref="Size3"/> with all of its components set to zero.
    /// </summary>
    public static readonly Size3 Empty = default;

    /// <summary>
    /// A special valued <see cref="Size"/>.
    /// </summary>
    public static readonly Size3 WholeSize = new Size3(~0, ~0, ~0);

    /// <summary>
    /// Initializes a new instance of <see cref="Size3"/> structure.
    /// </summary>
    /// <param name="width">The width component of the extent.</param>
    /// <param name="height">The height component of the extent.</param>
    /// <param name="depth">The depth component of the extent.</param>
    public Size3(int width, int height, int depth)
    {
        Width = width;
        Height = height;
        Depth = depth;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Size3"/> structure.
    /// </summary>
    /// <param name="size">The width and height component of the extent.</param>
    /// <param name="depth">The depth component of the extent.</param>
    public Size3(in SizeI size, int depth)
    {
        Width = size.Width;
        Height = size.Height;
        Depth = depth;
    }

    /// <summary>
    /// The width component of the extent.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// The height component of the extent.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// The depth component of the extent.
    /// </summary>
    public int Depth { get; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Size3"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => this == Empty;

    /// <summary>
    /// Deconstructs this size into three integers.
    /// </summary>
    /// <param name="width">The out value for the width.</param>
    /// <param name="height">The out value for the height.</param>
    /// <param name="depth">The out value for the depth.</param>
    public void Deconstruct(out int width, out int height, out int depth)
    {
        width = Width;
        height = Height;
        depth = Depth;
    }

    /// <summary>
    /// Compares two <see cref="Size3"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Size3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Size3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Size3 left, Size3 right) => left.Width == right.Width && left.Height == right.Height && left.Depth == right.Depth;

    /// <summary>
    /// Compares two <see cref="Size3"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Size3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Size3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Size3 left, Size3 right) => (left.Width != right.Width) || (left.Height != right.Height) || (left.Depth != right.Depth);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Size3 other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Size3 other) => this == other;

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        {
            hashCode.Add(Width);
            hashCode.Add(Height);
            hashCode.Add(Depth);
        }
        return hashCode.ToHashCode();
    }

    /// <inheritdoc/>
    public override readonly string ToString() => $"{{{nameof(Width)}={Width},{nameof(Height)}={Height},{nameof(Depth)}={Depth}}}";
}
