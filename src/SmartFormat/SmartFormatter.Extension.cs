//
// Copyright SmartFormat Project maintainers and contributors.
// Licensed under the MIT license.
//

using System;
using System.Collections.Generic;
using SmartFormat.Core.Output;
using SmartFormat.Core.Parsing;
using SmartFormat.Pooling.SmartPools;
using SmartFormat.ZString;

namespace SmartFormat;

/// <summary>
/// Smart formatter extension
/// </summary>
public static class SmartFormatterExtension
{
    /// <summary>
    /// Evaluate single selector.
    /// </summary>
    /// <param name="smartFormatter"></param>
    /// <param name="formatParsed"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static object? EvaluateSingleSelector(this SmartFormatter smartFormatter, Format formatParsed, params object?[] args)
    {
        // The first item is the default and will be used for the action,
        // but all args go to FormatDetails.OriginalArgs
        var current = ((IList<object?>) args).Count > 0 ? args[0] : args; // The first item is the default.

        using var zsOutput = new ZStringOutput(ZStringBuilderUtilities.CalcCapacity(formatParsed));
        using var fdo = FormatDetailsPool.Instance.Pool.Get(out var formatDetails);
        formatDetails.Initialize(smartFormatter, formatParsed, args, null, zsOutput);

        using var fio = FormattingInfoPool.Instance.Pool.Get(out var formattingInfo);
        formattingInfo.Initialize(formatDetails, formatParsed, current);

        smartFormatter.Registry.ThrowIfNoExtensions();
        if (formattingInfo.Format is null) return null;

        if (formattingInfo.Format.Items.Count != 1) throw new InvalidOperationException(
            "There are more than one selector!");

        return smartFormatter.Evaluator.EvaluateSingleSelector(formattingInfo);
    }
}
