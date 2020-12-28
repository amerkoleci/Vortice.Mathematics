// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines the intersection between a <see cref="Plane"/> and a bounding volume.
    /// </summary>
    public enum PlaneIntersectionType
    {
        /// <summary>
        /// There is no intersection, the bounding volume is in the negative half space of the plane.
        /// </summary>
        Front,
        /// <summary>
        /// There is no intersection, the bounding volume is in the positive half space of the plane.
        /// </summary>
        Back,
        /// <summary>
        /// The plane is intersected.
        /// </summary>
        Intersecting
    }
}
