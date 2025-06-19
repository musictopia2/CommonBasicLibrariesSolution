namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CssHelpers;
public static partial class CssClassExtractor
{
    public static HashSet<string> ExtractPatternsFromClasses(IEnumerable<string> classNames, int minimumCount = 3)
    {
        var patternCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var className in classNames)
        {
            int dashIndex = className.IndexOf('-');
            if (dashIndex > 0)
            {
                var pattern = className[..dashIndex];
                patternCounts[pattern] = patternCounts.GetValueOrDefault(pattern) + 1;
            }
        }

        return new HashSet<string>(
            patternCounts.Where(p => p.Value >= minimumCount)
                         .Select(p => p.Key),
            StringComparer.OrdinalIgnoreCase);
    }
    public static HashSet<string> ExtractStandaloneClasses(string css)
    {
        var classes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var combinedClasses = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        int pos = 0;
        int length = css.Length;
        bool insideAtRule = false;
        int braceDepth = 0;

        while (pos < length)
        {
            while (pos < length && char.IsWhiteSpace(css[pos]))
            {
                pos++;
            }

            if (pos >= length)
            {
                break;
            }

            if (!insideAtRule && css[pos] == '@')
            {
                insideAtRule = true;
                while (pos < length && css[pos] != '{')
                {
                    pos++;
                }

                if (pos < length && css[pos] == '{')
                {
                    braceDepth = 1;
                    pos++;
                }
                continue;
            }

            if (insideAtRule)
            {
                while (pos < length && braceDepth > 0)
                {
                    if (css[pos] == '{')
                    {
                        braceDepth++;
                    }
                    else if (css[pos] == '}')
                    {
                        braceDepth--;
                    }

                    pos++;
                }
                if (braceDepth == 0)
                {
                    insideAtRule = false;
                }
                continue;
            }

            int selectorStart = pos;
            while (pos < length && css[pos] != '{')
            {
                pos++;
            }

            if (pos >= length)
            {
                break;
            }

            string selectorBlock = css.Substring(selectorStart, pos - selectorStart).Trim();
            pos++; // skip '{'

            var selectors = selectorBlock.Split(',');

            foreach (var rawSelector in selectors)
            {
                var selector = rawSelector.Trim();

                // Extract class names with their positions
                // We'll consider the first class in the selector as 'standalone' candidate
                var parts = SecondGen().Split(selector);
                string firstClassInSelector = null!;

                foreach (var partRaw in parts)
                {
                    var part = partRaw.Trim();

                    if (part.Length > 0 && part[0] == '.')
                    {
                        int colonIndex = part.IndexOf(':');
                        int classEndIndex = colonIndex >= 0 ? colonIndex : part.Length;
                        string className = part.Substring(1, classEndIndex - 1);

                        if (!ThirdGen().IsMatch(className))
                        {
                            continue;
                        }

                        if (firstClassInSelector == null)
                        {
                            firstClassInSelector = className;
                            classes.Add(className);
                        }
                        else
                        {
                            combinedClasses.Add(className);
                        }
                    }
                }
            }

            // Skip CSS declarations block
            int declBraceDepth = 1;
            while (pos < length && declBraceDepth > 0)
            {
                if (css[pos] == '{')
                {
                    declBraceDepth++;
                }
                else if (css[pos] == '}')
                {
                    declBraceDepth--;
                }

                pos++;
            }
        }

        // Exclude classes that only appear in combination but never as first class
        combinedClasses.ExceptWith(classes);

        // Only return classes that appeared as the first class in selectors
        return classes;
    }

    [GeneratedRegex(@"\s+|(?=[+>~])|(?<=[+>~])")]
    private static partial Regex SecondGen();
    [GeneratedRegex(@"^[a-zA-Z0-9_-]+$")]
    private static partial Regex ThirdGen();
}
