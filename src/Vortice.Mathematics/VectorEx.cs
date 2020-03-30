// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents extensions methods for <see cref="Vector2"/>, <see cref="Vector3"/> and <see cref="Vector4"/>.
    /// </summary>
    public static class VectorEx
    {
        public static Vector2 MultiplyAdd(Vector2 vector1, Vector2 vector2, Vector2 vector3)
        {
            return new Vector2(
                vector1.X * vector2.X + vector3.X,
                vector1.Y * vector2.Y + vector3.Y
                );
        }

        public static Vector3 MultiplyAdd(Vector3 vector1, Vector3 vector2, Vector3 vector3)
        {
            return new Vector3(
                vector1.X * vector2.X + vector3.X,
                vector1.Y * vector2.Y + vector3.Y,
                vector1.Z * vector2.Z + vector3.Z
                );
        }

        public static Vector4 MultiplyAdd(Vector4 vector1, Vector4 vector2, Vector4 vector3)
        {
            return new Vector4(
                vector1.X * vector2.X + vector3.X,
                vector1.Y * vector2.Y + vector3.Y,
                vector1.Z * vector2.Z + vector3.Z,
                vector1.W * vector2.W + vector3.W
                );
        }
    }
}
