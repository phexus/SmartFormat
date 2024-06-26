﻿// 
// Copyright SmartFormat Project maintainers and contributors.
// Licensed under the MIT license.

using SmartFormat.Core.Parsing;

namespace SmartFormat.ZString;

/// <summary>
/// Extensions to <see cref="ZStringBuilder"/>.
/// </summary>
internal static class ZStringBuilderUtilities
{
    // DefaultBufferSize of Utf16ValueStringBuilder
    internal const int DefaultBufferSize = 32768;

    /// <summary>
    /// Calculates the estimated output string capacity for a <see cref="Format"/>.
    /// </summary>
    /// <param name="format"></param>
    /// <returns>The estimated output string capacity for a <see cref="Format"/>.</returns>
    internal static int CalcCapacity(Format format)
    {
        return format.Length + format.Items.Count * 8;
    }

    /// <summary>
    /// Creates a new instance of <see cref="ZStringBuilder"/> with the given initial capacity.
    /// </summary>
    internal static ZStringBuilder CreateZStringBuilder()
    {
        return new ZStringBuilder(false);
    }

    /// <summary>
    /// Creates a new instance of <see cref="ZStringBuilder"/> with the given initial capacity.
    /// </summary>
    /// <param name="format">The estimated buffer capacity will be calculated from the <see cref="Format"/> instance.</param>
    internal static ZStringBuilder CreateZStringBuilder(Format format)
    {
        return CreateZStringBuilder(CalcCapacity(format));
    }

    /// <summary>
    /// Creates a new instance of <see cref="ZStringBuilder"/> with the given initial capacity.
    /// </summary>
    /// <param name="capacity">The estimated capacity required. This will reduce or avoid incremental buffer increases.</param>
    internal static ZStringBuilder CreateZStringBuilder(int capacity)
    {
        var sb = new ZStringBuilder(false);
        if (capacity > DefaultBufferSize)
            sb.Grow(capacity - DefaultBufferSize);

        return sb;
    }
}
