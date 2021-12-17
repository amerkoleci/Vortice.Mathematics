// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Interface that converts packed vector types to and from <see cref="Vector4"/> values.
/// </summary>
public interface IPackedVector
{
    /// <summary>
    /// Converts the packed representation into a <see cref="Vector4"/>.
    /// </summary>
    Vector4 ToVector4();
}

/// <summary>
/// Converts packed vector types to and from <see cref="Vector4"/> values.
/// </summary>
public interface IPackedVector<TPacked> : IPackedVector
{
    /// <summary>
    /// Gets or Sets the packed representation of the value.
    /// </summary>
    TPacked PackedValue { get; }
}
