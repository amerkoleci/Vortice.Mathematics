// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Vector type containing two 64 bit floating point components.
/// </summary>
[DebuggerDisplay("X={X}, Y={Y}")]
public struct Double2 : IEquatable<Double2>, IFormattable
{
    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public double X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public double Y;

    internal const int Count = 2;

    /// <summary>
    /// Initializes a new instance of the <see cref="Double2"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Double2(double value)
    {
        X = value;
        Y = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double2" /> struct.
    /// </summary>
    /// <param name="x">Initial value for the X component of the vector.</param>
    /// <param name="y">Initial value for the Y component of the vector.</param>
    public Double2(double x, double y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double2" /> struct.
    /// </summary>
    /// <param name="xy">Initial value for the X and Y component of the vector.</param>
    public Double2(in Vector2 xy)
    {
        X = xy.X;
        Y = xy.Y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int2" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Double2(ReadOnlySpan<double> values)
    {
        if (values.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 2 uint values.");
        }

        this = Unsafe.ReadUnaligned<Double2>(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// A <see cref="Double2"/> with all of its components set to zero.
    /// </summary>
    public static Double2 Zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => default;
    }

    /// <summary>
    /// The X unit <see cref="Double2"/> (1, 0).
    /// </summary>
    public static Double2 UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1.0, 0.0);
    }

    /// <summary>
    /// The Y unit <see cref="Double2"/> (0, 1).
    /// </summary>
    public static Double2 UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0.0, 1.0);
    }

    /// <summary>
    /// A <see cref="Int2"/> with all of its components set to one.
    /// </summary>
    public static Double2 One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1.0, 1.0);
    }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <param name="index">The index of the element to get or set.</param>
    /// <returns>The the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    public double this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => this.GetElement(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this = this.WithElement(index, value);
    }

    /// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
    /// <returns>The interpolated vector.</returns>
    /// <remarks><format type="text/markdown"><![CDATA[
    /// The behavior of this method changed in .NET 5.0. For more information, see [Behavior change for Vector2.Lerp and Vector4.Lerp](/dotnet/core/compatibility/3.1-5.0#behavior-change-for-vector2lerp-and-vector4lerp).
    /// ]]></format></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Lerp(Double2 value1, Double2 value2, float amount)
    {
        return (value1 * (1.0f - amount)) + (value2 * amount);
    }

    /// <summary>Adds two vectors together.</summary>
    /// <param name="left">The first vector to add.</param>
    /// <param name="right">The second vector to add.</param>
    /// <returns>The summed vector.</returns>
    /// <remarks>The <see cref="op_Addition" /> method defines the addition operation for <see cref="Double2" /> objects.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator +(Double2 left, Double2 right)
    {
        return new(left.X + right.X, left.Y + right.Y);
    }

    /// <summary>Subtracts the second vector from the first.</summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The vector that results from subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
    /// <remarks>The <see cref="op_Subtraction" /> method defines the subtraction operation for <see cref="Double2" /> objects.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator -(Double2 left, Double2 right)
    {
        return new(left.X - right.X, left.Y - right.Y);
    }

    /// <summary>Negates the specified vector.</summary>
    /// <param name="value">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    /// <remarks>The <see cref="op_UnaryNegation" /> method defines the unary negation operation for <see cref="Double2" /> objects.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator -(Double2 value)
    {
        return Zero - value;
    }

    /// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The element-wise product vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator *(Double2 left, Double2 right)
    {
        return new(left.X * right.X, left.Y * right.Y);
    }

    /// <summary>Multiplies the specified vector by the specified scalar value.</summary>
    /// <param name="left">The vector.</param>
    /// <param name="scalar">The scalar value.</param>
    /// <returns>The scaled vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator *(Double2 left, float scalar)
    {
        return new(left.X * scalar, left.Y * scalar);
    }

    /// <summary>Multiplies the scalar value by the specified vector.</summary>
    /// <param name="scalar">The vector.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator *(float scalar, Double2 right)
    {
        return new(scalar * right.X, scalar * right.Y);
    }

    /// <summary>Divides the first vector by the second.</summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The vector that results from dividing <paramref name="left" /> by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator /(Double2 left, Double2 right)
    {
        return new(left.X / right.X, left.Y / right.Y);
    }

    /// <summary>Divides the specified vector by a specified scalar value.</summary>
    /// <param name="value1">The vector.</param>
    /// <param name="value2">The scalar value.</param>
    /// <returns>The result of the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator /(Double2 value1, double value2)
    {
        return value1 / new Double2(value2);
    }

    /// <summary>
    /// Creates a new <see cref="Double2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Double2"/> instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Double2(double x) => new(x, x);

    /// <summary>
    /// Casts a <see cref="Double2"/> value to a <see cref="UInt2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Double2"/> value to cast.</param>
    public static explicit operator UInt2(Double2 xy) => new((uint)xy.X, (uint)xy.Y);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Double2" /> to <see cref="Vector2"/>.
    /// </summary>
    /// <param name="xy">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector2(Double2 xy) => new((float)xy.X, (float)xy.Y);

    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(double[] array)
    {
        CopyTo(array, 0);
    }

    public readonly void CopyTo(double[] array, int index)
    {
        if (array is null)
        {
            throw new NullReferenceException(nameof(array));
        }

        if ((index < 0) || (index >= array.Length))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if ((array.Length - index) < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        array[index] = X;
        array[index + 1] = Y;
    }

    /// <summary>Copies the vector to the given <see cref="Span{T}" />.The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination span.</exception>
    public readonly void CopyTo(Span<double> destination)
    {
        if (destination.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(destination));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(destination)), this);
    }

    /// <summary>Attempts to copy the vector to the given <see cref="Span{Double}" />. The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <returns><see langword="true" /> if the source vector was successfully copied to <paramref name="destination" />. <see langword="false" /> if <paramref name="destination" /> is not large enough to hold the source vector.</returns>
    public readonly bool TryCopyTo(Span<double> destination)
    {
        if (destination.Length < 2)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(destination)), this);

        return true;
    }

    /// <summary>Returns a vector whose elements are the absolute values of each of the specified vector's elements.</summary>
    /// <param name="value">A vector.</param>
    /// <returns>The absolute value vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Abs(Double2 value)
    {
        return new Double2(
            Math.Abs(value.X),
            Math.Abs(value.Y)
        );
    }

    /// <summary>Adds two vectors together.</summary>
    /// <param name="left">The first vector to add.</param>
    /// <param name="right">The second vector to add.</param>
    /// <returns>The summed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Add(Double2 left, Double2 right)
    {
        return left + right;
    }

    /// <summary>Subtracts the second vector from the first.</summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The difference vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Subtract(Double2 left, Double2 right)
    {
        return left - right;
    }

    /// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The element-wise product vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Multiply(Double2 left, Double2 right)
    {
        return left * right;
    }

    /// <summary>Multiplies a vector by a specified scalar.</summary>
    /// <param name="left">The vector to multiply.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Multiply(Double2 left, double right)
    {
        return left * right;
    }

    /// <summary>Multiplies a scalar value by a specified vector.</summary>
    /// <param name="left">The scaled value.</param>
    /// <param name="right">The vector.</param>
    /// <returns>The scaled vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Multiply(double left, Double2 right)
    {
        return left * right;
    }

    /// <summary>Divides the first vector by the second.</summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The vector resulting from the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Divide(Double2 left, Double2 right)
    {
        return left / right;
    }

    /// <summary>Divides the specified vector by a specified scalar value.</summary>
    /// <param name="left">The vector.</param>
    /// <param name="divisor">The scalar value.</param>
    /// <returns>The vector that results from the division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Divide(Double2 left, float divisor)
    {
        return left / divisor;
    }

    /// <summary>Negates a specified vector.</summary>
    /// <param name="value">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Negate(Double2 value)
    {
        return -value;
    }

    /// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
    /// <param name="value">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Normalize(Double2 value)
    {
        return value / value.Length();
    }

    /// <summary>Returns the dot product of two vectors.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The dot product.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Dot(Double2 value1, Double2 value2)
    {
        return (value1.X * value2.X)
             + (value1.Y * value2.Y);
    }

    /// <summary>Returns the length of the vector.</summary>
    /// <returns>The vector's length.</returns>
    /// <altmember cref="LengthSquared"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double Length()
    {
        double lengthSquared = LengthSquared();
        return Math.Sqrt(lengthSquared);
    }

    /// <summary>Returns the length of the vector squared.</summary>
    /// <returns>The vector's length squared.</returns>
    /// <remarks>This operation offers better performance than a call to the <see cref="Length" /> method.</remarks>
    /// <altmember cref="Length"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double LengthSquared()
    {
        return Dot(this, this);
    }

    /// <summary>Returns the reflection of a vector off a surface that has the specified normal.</summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="normal">The normal of the surface being reflected off.</param>
    /// <returns>The reflected vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Reflect(Double2 vector, Double2 normal)
    {
        double dot = Dot(vector, normal);
        return vector - (2.0 * (dot * normal));
    }

    /// <summary>Returns a vector whose elements are the square root of each of a specified vector's elements.</summary>
    /// <param name="value">A vector.</param>
    /// <returns>The square root vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 SquareRoot(Double2 value)
    {
        return new Double2(
            Math.Sqrt(value.X),
            Math.Sqrt(value.Y)
        );
    }

    /// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The minimized vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Min(Double2 value1, Double2 value2)
    {
        return new(
            (value1.X < value2.X) ? value1.X : value2.X,
            (value1.Y < value2.Y) ? value1.Y : value2.Y
        );
    }

    /// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The maximized vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Max(Double2 value1, Double2 value2)
    {
        return new(
            (value1.X > value2.X) ? value1.X : value2.X,
            (value1.Y > value2.Y) ? value1.Y : value2.Y
        );
    }

    /// <summary>Restricts a vector between a minimum and a maximum value.</summary>
    /// <param name="value1">The vector to restrict.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The restricted vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Clamp(Double2 value1, Double2 min, Double2 max)
    {
        // We must follow HLSL behavior in the case user specified min value is bigger than max value.
        return Min(Max(value1, min), max);
    }

    /// <summary>Computes the Euclidean distance between the two given points.</summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Distance(Double2 value1, Double2 value2)
    {
        double distanceSquared = DistanceSquared(value1, value2);
        return Math.Sqrt(distanceSquared);
    }

    /// <summary>Returns the Euclidean distance squared between two specified points.</summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance squared.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DistanceSquared(Double2 value1, Double2 value2)
    {
        Double2 difference = value1 - value2;
        return Dot(difference, difference);
    }

    /// <summary>Transforms a vector by a specified 3x2 matrix.</summary>
    /// <param name="position">The vector to transform.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Transform(Double2 position, Matrix3x2 matrix)
    {
        Double2 result = new Double2(matrix.M11, matrix.M12) * position.X;

        result += new Double2(matrix.M21, matrix.M22) * position.Y;
        result += new Double2(matrix.M31, matrix.M32);

        return result;
    }

    /// <summary>Transforms a vector by a specified 4x4 matrix.</summary>
    /// <param name="position">The vector to transform.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Transform(Vector2 position, Matrix4x4 matrix)
    {
        double x = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
        double y = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;

        return new(x, y);
    }

    /// <summary>Transforms a vector by the specified Quaternion rotation value.</summary>
    /// <param name="value">The vector to rotate.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Transform(Double2 value, Quaternion rotation)
    {
        float x2 = rotation.X + rotation.X;
        float y2 = rotation.Y + rotation.Y;
        float z2 = rotation.Z + rotation.Z;

        float wz2 = rotation.W * z2;
        float xx2 = rotation.X * x2;
        float xy2 = rotation.X * y2;
        float yy2 = rotation.Y * y2;
        float zz2 = rotation.Z * z2;

        return new Double2(
            value.X * (1.0f - yy2 - zz2) + value.Y * (xy2 - wz2),
            value.X * (xy2 + wz2) + value.Y * (1.0f - xx2 - zz2)
        );
    }

    /// <summary>Transforms a vector normal by the given 3x2 matrix.</summary>
    /// <param name="normal">The source vector.</param>
    /// <param name="matrix">The matrix.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 TransformNormal(Double2 normal, Matrix3x2 matrix)
    {
        double x = normal.X * matrix.M11 + normal.Y * matrix.M21;
        double y = normal.X * matrix.M12 + normal.Y * matrix.M22;

        return new(x, y);
    }

    /// <summary>Transforms a vector normal by the given 4x4 matrix.</summary>
    /// <param name="normal">The source vector.</param>
    /// <param name="matrix">The matrix.</param>
    /// <returns>The transformed vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 TransformNormal(Double2 normal, Matrix4x4 matrix)
    {
        double x = normal.X * matrix.M11 + normal.Y * matrix.M21;
        double y = normal.X * matrix.M12 + normal.Y * matrix.M22;

        return new(x, y);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Double2 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Double2"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Double2"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Double2 other)
    {
        return X == other.X
            && Y == other.Y;
    }

    /// <summary>
    /// Compares two <see cref="Double2"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Double2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Double2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Double2 left, Double2 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Double2"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Double2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Double2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Double2 left, Double2 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Double2)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)} }}";
}
