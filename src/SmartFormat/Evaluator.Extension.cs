//
// Copyright SmartFormat Project maintainers and contributors.
// Licensed under the MIT license.
//

using System.Linq;
using SmartFormat.Core.Formatting;
using SmartFormat.Core.Parsing;

namespace SmartFormat;
internal partial class Evaluator
{
    public object? EvaluateSingleSelector(FormattingInfo formattingInfo)
    {
        var format = formattingInfo.Format!;

        OnFormat?.Invoke(this, new FormatEventArgs(format));

        var item = format.Items.Single();
        if (item is LiteralText literalItem)
        {
            return literalItem.BaseString;
        }

        // Otherwise, the item must be a placeholder.
        var placeholder = (Placeholder) item;
        var childFormattingInfo = formattingInfo.CreateChild(placeholder);
        OnPlaceholder?.Invoke(this, new PlaceholderEventArgs(placeholder));

        // Try to get a value for the placeholder...
        return EvaluatePlaceholder(childFormattingInfo) ? childFormattingInfo.CurrentValue : null;
    }
}
