﻿namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
public static class Extensions
{
    /// <summary>
    /// Capitalize the first letter of a string.
    /// </summary>
    /// <param name="this"></param>
    /// <returns>Returns a copy of this <see cref="string"/> with capitalized first letter.</returns>
    public static string Capitalize(this string @this)
        => string.Concat(@this[0].ToString().ToUpperInvariant(), @this.AsSpan(1));
}