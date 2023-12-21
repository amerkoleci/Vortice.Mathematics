// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if WINDOWS
using System.Drawing;
using WindowsColor = Windows.UI.Color;
using WindowsRect = Windows.Foundation.Rect;
using WindowsSize = Windows.Foundation.Size;

namespace Vortice.Mathematics;

public static class Extensions
{
    public static Point ToSystemPoint(this Windows.Foundation.Point point)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(point.X, int.MaxValue, nameof(point.X));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(point.Y, int.MaxValue, nameof(point.Y));

        return new Point((int)point.X, (int)point.Y);
    }

    public static PointF ToSystemPointF(this Windows.Foundation.Point point) =>
        new((float)point.X, (float)point.Y);

    public static Windows.Foundation.Point ToPlatformPoint(this Point point) =>
        new(point.X, point.Y);

    public static Windows.Foundation.Point ToPlatformPoint(this PointF point) =>
        new(point.X, point.Y);


    public static Size ToSystemSize(this WindowsSize size)
    {
        if (size.Width > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(size.Width));

        if (size.Height > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(size.Height));

        return new Size((int)size.Width, (int)size.Height);
    }

    public static SizeF ToSystemSizeF(this WindowsSize size) =>
        new((float)size.Width, (float)size.Height);

    public static WindowsSize ToPlatformSize(this Size size) =>
        new(size.Width, size.Height);

    public static WindowsSize ToPlatformSize(this SizeF size) =>
        new(size.Width, size.Height);

    public static Rectangle ToSystemRect(this WindowsRect rect)
    {
        if (rect.X > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(rect.X));

        if (rect.Y > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(rect.Y));

        if (rect.Width > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(rect.Width));

        if (rect.Height > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(rect.Height));

        return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
    }

    public static RectangleF ToSystemRectF(this WindowsRect rect) =>
        new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);

    public static WindowsRect ToPlatformRect(this Rectangle rect) => new(rect.X, rect.Y, rect.Width, rect.Height);

    public static WindowsRect ToPlatformRect(this RectangleF rect) => new(rect.X, rect.Y, rect.Width, rect.Height);

    public static Color ToSystemColor(this WindowsColor color) => new(color.R, color.G, color.B, color.A);

    public static WindowsColor ToPlatformColor(this Color color) => WindowsColor.FromArgb(color.A, color.R, color.G, color.B);
    public static WindowsColor ToPlatformColor(this System.Drawing.Color color) => WindowsColor.FromArgb(color.A, color.R, color.G, color.B);

    public static System.Drawing.Color ToSystemDrawingColor(this WindowsColor color) => System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
}
#endif
