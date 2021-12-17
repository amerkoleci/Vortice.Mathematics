// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Describes a 5-by-4 floating point matrix.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Matrix5x4
{
    /// <summary>
    /// Value at row 1 column 1.
    /// </summary>
    public float M11;

    /// <summary>
    /// Value at row 1 column 2.
    /// </summary>
    public float M12;

    /// <summary>
    /// Value at row 1 column 3.
    /// </summary>
    public float M13;

    /// <summary>
    /// Value at row 1 column 4.
    /// </summary>
    public float M14;

    /// <summary>
    /// Value at row 2 column 1.
    /// </summary>
    public float M21;

    /// <summary>
    /// Value at row 2 column 2.
    /// </summary>
    public float M22;

    /// <summary>
    /// Value at row 2 column 3.
    /// </summary>
    public float M23;

    /// <summary>
    /// Value at row 2 column 4.
    /// </summary>
    public float M24;

    /// <summary>
    /// Value at row 3 column 1.
    /// </summary>
    public float M31;

    /// <summary>
    /// Value at row 3 column 2.
    /// </summary>
    public float M32;

    /// <summary>
    /// Value at row 3 column 3.
    /// </summary>
    public float M33;

    /// <summary>
    /// Value at row 3 column 4.
    /// </summary>
    public float M34;

    /// <summary>
    /// Value at row 4 column 1.
    /// </summary>
    public float M41;

    /// <summary>
    /// Value at row 4 column 2.
    /// </summary>
    public float M42;

    /// <summary>
    /// Value at row 4 column 3.
    /// </summary>
    public float M43;

    /// <summary>
    /// Value at row 4 column 4.
    /// </summary>
    public float M44;

    /// <summary>
    /// Value at row 5 column 1.
    /// </summary>
    public float M51;

    /// <summary>
    /// Value at row 5 column 2.
    /// </summary>
    public float M52;

    /// <summary>
    /// Value at row 5 column 3.
    /// </summary>
    public float M53;

    /// <summary>
    /// Value at row 5 column 4.
    /// </summary>
    public float M54;
}
