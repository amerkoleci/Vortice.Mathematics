// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

[DebuggerDisplay("Width={Width}, Height={Height}")]
public struct PointI : IEquatable<PointI>, IFormattable
{
    /// <summary>
    /// A <see cref="Point"/> with all of its components set to zero.
    /// </summary>
    public static readonly Point Empty = default;

    /// <summary>
    /// Initializes a new instance of <see cref="PointI"/> structure.
    /// </summary>
    /// <param name="x">The x coordinate of the point.</param>
    /// <param name="y">The y coordinate of the point.</param>
    public PointI(int x, int y)
    {
        X = x;
        Y = y;
    }

    public PointI(SizeI size)
    {
        X = size.Width;
        Y = size.Height;
    }

    public PointI(Vector2 vector)
    {
        X = (int)vector.X;
        Y = (int)vector.Y;
    }

    /// <summary>
    /// The x-coordinate of the Point.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// The y-coordinate of the Point.
    /// </summary>
    public int Y { get; set; }

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    /// <summary>
    /// Compares two <see cref="PointI"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="PointI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="PointI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(PointI left, PointI right) => left.X == right.X && left.Y == right.Y;

    /// <summary>
    /// Compares two <see cref="PointI"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="PointI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="PointI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(PointI left, PointI right) => (left.X != right.X) || (left.Y != right.Y);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is PointI other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(PointI other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(PointI)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)} }}";
}
