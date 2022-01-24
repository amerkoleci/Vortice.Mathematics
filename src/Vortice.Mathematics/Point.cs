// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

[DebuggerDisplay("Width={Width}, Height={Height}")]
public struct Point : IEquatable<Point>, IFormattable
{
    /// <summary>
    /// A <see cref="Point"/> with all of its components set to zero.
    /// </summary>
    public static readonly Point Empty = default;

    /// <summary>
    /// Initializes a new instance of <see cref="Point"/> structure.
    /// </summary>
    /// <param name="x">The x coordinate of the point.</param>
    /// <param name="y">The y coordinate of the point.</param>
    public Point(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Point(Size size)
    {
        X = size.Width;
        Y = size.Height;
    }

    public Point(Vector2 vector)
    {
        X = vector.X;
        Y = vector.Y;
    }

    /// <summary>
    /// Constructs a vector from the given <see cref="ReadOnlySpan{Single}" />. The span must contain at least 2 elements.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Point(ReadOnlySpan<float> values)
    {
        if (values.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be two and only two input values for Point.");
        }

        this = Unsafe.ReadUnaligned<Vector2>(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// The x-coordinate of the Point.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// The y-coordinate of the Point.
    /// </summary>
    public float Y { get; set; }

    public void Deconstruct(out float x, out float y)
    {
        x = X;
        y = Y;
    }

    public void Offset(float offsetX, float offsetY)
    {
        X += offsetX;
        Y += offsetY;
    }

    public void Offset(Point offset)
    {
        X += offset.X;
        Y += offset.Y;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly float Length()
    {
        float lengthSquared = LengthSquared();
        return MathF.Sqrt(lengthSquared);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly float LengthSquared()
    {
        return (X* X) + (Y * Y);
    }

    public static float Distance(Point point1, Point point2)
    {
        float dx = point1.X - point2.X;
        float dy = point1.Y - point2.Y;
        float ls = dx * dx + dy * dy;
        return MathF.Sqrt(ls);
    }

    public static float DistanceSquared(Point point1, Point point2)
    {
        float dx = point1.X - point2.X;
        float dy = point1.Y - point2.Y;
        return dx * dx + dy * dy;
    }

    /// <summary>Returns a Point whose elements are the minimum of each of the pairs of elements in two specified points.</summary>
    /// <param name="value1">The first Point.</param>
    /// <param name="value2">The second Point.</param>
    /// <returns>The minimized vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Min(Point value1, Point value2)
    {
        return new Point(
            (value1.X < value2.X) ? value1.X : value2.X,
            (value1.Y < value2.Y) ? value1.Y : value2.Y
        );
    }

    /// <summary>Returns a Point whose elements are the maximum of each of the pairs of elements in two specified points.</summary>
    /// <param name="value1">The first Point.</param>
    /// <param name="value2">The second Point.</param>
    /// <returns>The maximized vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Max(Point value1, Point value2)
    {
        return new Point(
            (value1.X > value2.X) ? value1.X : value2.X,
            (value1.Y > value2.Y) ? value1.Y : value2.Y
        );
    }

    /// <summary>Restricts a point between a minimum and a maximum value.</summary>
    /// <param name="value">The point to restrict.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The restricted vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Clamp(Point value, Point min, Point max)
    {
        return Min(Max(value, min), max);
    }

    /// <summary>Performs a linear interpolation between two points based on the given weighting.</summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
    /// <returns>The interpolated point.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Lerp(Point value1, Point value2, float amount)
    {
        return (value1 * (1.0f - amount)) + (value2 * amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Dot(Point value1, Point value2)
    {
        return (value1.X * value2.X)
             + (value1.Y * value2.Y);
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Vector2"/> to <see cref="Point"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>

    public static implicit operator Point(Vector2 value) => new(value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Point"/> to <see cref="Size"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>

    public static explicit operator Size(Point value) => new(value.X, value.Y);

    public static Point operator +(Point point, SizeI size)
    {
        return new(point.X + size.Width, point.Y + size.Height);
    }

    public static Point operator +(Point point, Size size)
    {
        return new(point.X + size.Width, point.Y + size.Height);
    }

    public static Point operator +(Point point1, PointI point2)
    {
        return new(point1.X + point2.X, point1.Y + point2.Y);
    }

    public static Point operator +(Point point1, Point point2)
    {
        return new(point1.X + point2.X, point1.Y + point2.Y);
    }

    public static Point operator -(Point point, SizeI size)
    {
        return new(point.X - (float)size.Width, point.Y - (float)size.Height);
    }

    public static Point operator -(Point point, Size size)
    {
        return new(point.X - size.Width, point.Y - size.Height);
    }

    public static Point operator -(Point point1, PointI point2)
    {
        return new(point1.X - point2.X, point1.Y - point2.Y);
    }

    public static Point operator -(Point point1, Point point2)
    {
        return new(point1.X - point2.X, point1.Y - point2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point operator *(Point left, Point right)
    {
        return new Point(
            left.X * right.X,
            left.Y * right.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point operator *(Point left, float right)
    {
        return new Point(
            left.X * right,
            left.Y * right
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point operator *(float left, Point right)
    {
        return new Point(
            left * right.X,
            left * right.Y
        );
    }

    /// <summary>Transforms a Point by a specified 3x2 matrix.</summary>
    /// <param name="position">The Point to transform.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Transform(Point position, Matrix3x2 matrix)
    {
        return new Point(
            (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M31,
            (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M32
        );
    }

    /// <summary>Transforms a Point by a specified 4x4 matrix.</summary>
    /// <param name="position">The Point to transform.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Transform(Point position, Matrix4x4 matrix)
    {
        return new Point(
            (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
            (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42
        );
    }

    /// <summary>Transforms a Point by the specified Quaternion rotation value.</summary>
    /// <param name="value">The Point to rotate.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point Transform(Point value, Quaternion rotation)
    {
        float x2 = rotation.X + rotation.X;
        float y2 = rotation.Y + rotation.Y;
        float z2 = rotation.Z + rotation.Z;

        float wz2 = rotation.W * z2;
        float xx2 = rotation.X * x2;
        float xy2 = rotation.X * y2;
        float yy2 = rotation.Y * y2;
        float zz2 = rotation.Z * z2;

        return new Vector2(
            value.X * (1.0f - yy2 - zz2) + value.Y * (xy2 - wz2),
            value.X * (xy2 + wz2) + value.Y * (1.0f - xx2 - zz2)
        );
    }

    /// <summary>Transforms a Point normal by the given 3x2 matrix.</summary>
    /// <param name="normal">The source vector.</param>
    /// <param name="matrix">The matrix.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point TransformNormal(Point normal, Matrix3x2 matrix)
    {
        return new Point(
            (normal.X * matrix.M11) + (normal.Y * matrix.M21),
            (normal.X * matrix.M12) + (normal.Y * matrix.M22)
        );
    }

    /// <summary>Transforms a Point normal by the given 4x4 matrix.</summary>
    /// <param name="normal">The source vector.</param>
    /// <param name="matrix">The matrix.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point TransformNormal(Point normal, Matrix4x4 matrix)
    {
        return new Point(
            (normal.X * matrix.M11) + (normal.Y * matrix.M21),
            (normal.X * matrix.M12) + (normal.Y * matrix.M22)
        );
    }

    public static bool TryParse(string value, out Point point)
    {
        if (!string.IsNullOrEmpty(value))
        {
            string[] xy = value.Split(',');
            if (xy.Length == 2
                && float.TryParse(xy[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float x)
                && float.TryParse(xy[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float y))
            {
                point = new Point(x, y);
                return true;
            }
        }

        point = default;
        return false;
    }

    /// <summary>
    /// Compares two <see cref="Point"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Point"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Point"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Point left, Point right) => left.X == right.X && left.Y == right.Y;

    /// <summary>
    /// Compares two <see cref="Point"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Point"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Point"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Point left, Point right) => (left.X != right.X) || (left.Y != right.Y);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Point other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Point other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Point)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)} }}";
}
