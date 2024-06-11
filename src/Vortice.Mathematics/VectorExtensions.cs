// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

/// <summary>
/// Defines extension methods for types in the vector types.
/// </summary>
public static class VectorExtensions
{
    /// <summary>Gets the element at the specified index.</summary>
    /// <param name="vector">The vector to get the element from.</param>
    /// <param name="index">The index of the element to get.</param>
    /// <returns>The value of the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int GetElement(this Int2 vector, int index)
    {
        if ((uint)(index) >= Int2.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return vector.GetElementUnsafe(index);
    }

    /// <summary>Gets the element at the specified index.</summary>
    /// <param name="vector">The vector to get the element from.</param>
    /// <param name="index">The index of the element to get.</param>
    /// <returns>The value of the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int GetElement(this Int4 vector, int index)
    {
        if ((uint)(index) >= (uint)(Int4.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return vector.GetElementUnsafe(index);
    }

    /// <summary>Creates a new <see cref="Vector2" /> with the element at the specified index set to the specified value and the remaining elements set to the same value as that in the given vector.</summary>
    /// <param name="vector">The vector to get the remaining elements from.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value to set the element to.</param>
    /// <returns>A <see cref="Vector2" /> with the value of the element at <paramref name="index" /> set to <paramref name="value" /> and the remaining elements set to the same value as that in <paramref name="vector" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static Int2 WithElement(this Int2 vector, int index, int value)
    {
        if ((uint)(index) >= (uint)(Int2.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Int2 result = vector;
        result.SetElementUnsafe(index, value);
        return result;
    }

    /// <summary>Creates a new <see cref="Vector2" /> with the element at the specified index set to the specified value and the remaining elements set to the same value as that in the given vector.</summary>
    /// <param name="vector">The vector to get the remaining elements from.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value to set the element to.</param>
    /// <returns>A <see cref="Vector2" /> with the value of the element at <paramref name="index" /> set to <paramref name="value" /> and the remaining elements set to the same value as that in <paramref name="vector" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static Int3 WithElement(this Int3 vector, int index, int value)
    {
        if ((uint)(index) >= (uint)(Int3.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Int3 result = vector;
        result.SetElementUnsafe(index, value);
        return result;
    }

    /// <summary>Creates a new <see cref="Vector2" /> with the element at the specified index set to the specified value and the remaining elements set to the same value as that in the given vector.</summary>
    /// <param name="vector">The vector to get the remaining elements from.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value to set the element to.</param>
    /// <returns>A <see cref="Vector2" /> with the value of the element at <paramref name="index" /> set to <paramref name="value" /> and the remaining elements set to the same value as that in <paramref name="vector" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static Int4 WithElement(this Int4 vector, int index, int value)
    {
        if ((uint)(index) >= (uint)(Int4.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Int4 result = vector;
        result.SetElementUnsafe(index, value);
        return result;
    }

    /// <summary>Gets the element at the specified index.</summary>
    /// <param name="vector">The vector to get the element from.</param>
    /// <param name="index">The index of the element to get.</param>
    /// <returns>The value of the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static uint GetElement(this UInt2 vector, int index)
    {
        if ((uint)(index) >= (uint)(UInt2.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return vector.GetElementUnsafe(index);
    }

    /// <summary>Gets the element at the specified index.</summary>
    /// <param name="vector">The vector to get the element from.</param>
    /// <param name="index">The index of the element to get.</param>
    /// <returns>The value of the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static uint GetElement(this UInt3 vector, int index)
    {
        if ((uint)(index) >= (uint)(UInt3.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return vector.GetElementUnsafe(index);
    }

    /// <summary>Gets the element at the specified index.</summary>
    /// <param name="vector">The vector to get the element from.</param>
    /// <param name="index">The index of the element to get.</param>
    /// <returns>The value of the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static uint GetElement(this UInt4 vector, int index)
    {
        if ((uint)(index) >= (uint)(UInt4.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return vector.GetElementUnsafe(index);
    }

    /// <summary>Creates a new <see cref="Vector2" /> with the element at the specified index set to the specified value and the remaining elements set to the same value as that in the given vector.</summary>
    /// <param name="vector">The vector to get the remaining elements from.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value to set the element to.</param>
    /// <returns>A <see cref="Vector2" /> with the value of the element at <paramref name="index" /> set to <paramref name="value" /> and the remaining elements set to the same value as that in <paramref name="vector" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static UInt2 WithElement(this UInt2 vector, int index, uint value)
    {
        if ((uint)(index) >= (uint)(UInt2.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        UInt2 result = vector;
        result.SetElementUnsafe(index, value);
        return result;
    }

    /// <summary>Creates a new <see cref="Vector2" /> with the element at the specified index set to the specified value and the remaining elements set to the same value as that in the given vector.</summary>
    /// <param name="vector">The vector to get the remaining elements from.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value to set the element to.</param>
    /// <returns>A <see cref="Vector2" /> with the value of the element at <paramref name="index" /> set to <paramref name="value" /> and the remaining elements set to the same value as that in <paramref name="vector" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static UInt3 WithElement(this UInt3 vector, int index, uint value)
    {
        if ((uint)(index) >= (uint)(UInt3.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        UInt3 result = vector;
        result.SetElementUnsafe(index, value);
        return result;
    }

    /// <summary>Creates a new <see cref="Vector2" /> with the element at the specified index set to the specified value and the remaining elements set to the same value as that in the given vector.</summary>
    /// <param name="vector">The vector to get the remaining elements from.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value to set the element to.</param>
    /// <returns>A <see cref="Vector2" /> with the value of the element at <paramref name="index" /> set to <paramref name="value" /> and the remaining elements set to the same value as that in <paramref name="vector" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static UInt4 WithElement(this UInt4 vector, int index, uint value)
    {
        if ((uint)(index) >= (uint)(UInt4.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        UInt4 result = vector;
        result.SetElementUnsafe(index, value);
        return result;
    }

    /// <summary>Gets the element at the specified index.</summary>
    /// <param name="vector">The vector to get the element from.</param>
    /// <param name="index">The index of the element to get.</param>
    /// <returns>The value of the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static double GetElement(this Double2 vector, int index)
    {
        if ((uint)(index) >= (uint)(Double2.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return vector.GetElementUnsafe(index);
    }

    /// <summary>Gets the element at the specified index.</summary>
    /// <param name="vector">The vector to get the element from.</param>
    /// <param name="index">The index of the element to get.</param>
    /// <returns>The value of the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static double GetElement(this Double3 vector, int index)
    {
        if ((uint)(index) >= (uint)(Double3.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return vector.GetElementUnsafe(index);
    }

    /// <summary>Gets the element at the specified index.</summary>
    /// <param name="vector">The vector to get the element from.</param>
    /// <param name="index">The index of the element to get.</param>
    /// <returns>The value of the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static double GetElement(this Double4 vector, int index)
    {
        if ((uint)(index) >= (uint)(Double4.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return vector.GetElementUnsafe(index);
    }

    /// <summary>Creates a new <see cref="Vector2" /> with the element at the specified index set to the specified value and the remaining elements set to the same value as that in the given vector.</summary>
    /// <param name="vector">The vector to get the remaining elements from.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value to set the element to.</param>
    /// <returns>A <see cref="Vector2" /> with the value of the element at <paramref name="index" /> set to <paramref name="value" /> and the remaining elements set to the same value as that in <paramref name="vector" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static Double2 WithElement(this Double2 vector, int index, double value)
    {
        if ((uint)(index) >= (uint)(Double2.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Double2 result = vector;
        result.SetElementUnsafe(index, value);
        return result;
    }

    /// <summary>Creates a new <see cref="Vector2" /> with the element at the specified index set to the specified value and the remaining elements set to the same value as that in the given vector.</summary>
    /// <param name="vector">The vector to get the remaining elements from.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value to set the element to.</param>
    /// <returns>A <see cref="Vector2" /> with the value of the element at <paramref name="index" /> set to <paramref name="value" /> and the remaining elements set to the same value as that in <paramref name="vector" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static Double3 WithElement(this Double3 vector, int index, double value)
    {
        if ((uint)(index) >= (uint)(Double3.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Double3 result = vector;
        result.SetElementUnsafe(index, value);
        return result;
    }

    /// <summary>Creates a new <see cref="Vector2" /> with the element at the specified index set to the specified value and the remaining elements set to the same value as that in the given vector.</summary>
    /// <param name="vector">The vector to get the remaining elements from.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value to set the element to.</param>
    /// <returns>A <see cref="Vector2" /> with the value of the element at <paramref name="index" /> set to <paramref name="value" /> and the remaining elements set to the same value as that in <paramref name="vector" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static Double4 WithElement(this Double4 vector, int index, double value)
    {
        if ((uint)(index) >= (uint)(Double4.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Double4 result = vector;
        result.SetElementUnsafe(index, value);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetElementUnsafe(in this Int2 vector, int index)
    {
        Debug.Assert((index >= 0) && (index < Int2.Count));

        ref int address = ref Unsafe.AsRef(in vector.X);
        return Unsafe.Add(ref address, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetElementUnsafe(in this Int4 vector, int index)
    {
        Debug.Assert((index >= 0) && (index < Int4.Count));

        ref int address = ref Unsafe.AsRef(in vector.X);
        return Unsafe.Add(ref address, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe(ref this Int2 vector, int index, int value)
    {
        Debug.Assert((index >= 0) && (index < Int2.Count));

        Unsafe.Add(ref vector.X, index) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe(ref this Int3 vector, int index, int value)
    {
        Debug.Assert((index >= 0) && (index < Int3.Count));

        Unsafe.Add(ref vector.X, index) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe(ref this Int4 vector, int index, int value)
    {
        Debug.Assert((index >= 0) && (index < Int4.Count));

        Unsafe.Add(ref vector.X, index) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint GetElementUnsafe(in this UInt2 vector, int index)
    {
        Debug.Assert((index >= 0) && (index < UInt2.Count));

        ref uint address = ref Unsafe.AsRef(in vector.X);
        return Unsafe.Add(ref address, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint GetElementUnsafe(in this UInt3 vector, int index)
    {
        Debug.Assert((index >= 0) && (index < UInt3.Count));

        ref uint address = ref Unsafe.AsRef(in vector.X);
        return Unsafe.Add(ref address, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint GetElementUnsafe(in this UInt4 vector, int index)
    {
        Debug.Assert((index >= 0) && (index < UInt4.Count));

        ref uint address = ref Unsafe.AsRef(in vector.X);
        return Unsafe.Add(ref address, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe(ref this UInt2 vector, int index, uint value)
    {
        Debug.Assert((index >= 0) && (index < UInt2.Count));

        Unsafe.Add(ref vector.X, index) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe(ref this UInt3 vector, int index, uint value)
    {
        Debug.Assert((index >= 0) && (index < UInt3.Count));

        Unsafe.Add(ref vector.X, index) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe(ref this UInt4 vector, int index, uint value)
    {
        Debug.Assert((index >= 0) && (index < UInt4.Count));

        Unsafe.Add(ref vector.X, index) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double GetElementUnsafe(in this Double2 vector, int index)
    {
        Debug.Assert((index >= 0) && (index < Double2.Count));

        ref double address = ref Unsafe.AsRef(in vector.X);
        return Unsafe.Add(ref address, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double GetElementUnsafe(in this Double3 vector, int index)
    {
        Debug.Assert((index >= 0) && (index < Double3.Count));

        ref double address = ref Unsafe.AsRef(in vector.X);
        return Unsafe.Add(ref address, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double GetElementUnsafe(in this Double4 vector, int index)
    {
        Debug.Assert((index >= 0) && (index < Double4.Count));

        ref double address = ref Unsafe.AsRef(in vector.X);
        return Unsafe.Add(ref address, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe(ref this Double2 vector, int index, double value)
    {
        Debug.Assert((index >= 0) && (index < Double2.Count));

        Unsafe.Add(ref vector.X, index) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe(ref this Double3 vector, int index, double value)
    {
        Debug.Assert((index >= 0) && (index < Double3.Count));

        Unsafe.Add(ref vector.X, index) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe(ref this Double4 vector, int index, double value)
    {
        Debug.Assert((index >= 0) && (index < Double4.Count));

        Unsafe.Add(ref vector.X, index) = value;
    }
}
