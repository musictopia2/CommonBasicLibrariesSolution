﻿using System;
namespace CommonBasicLibraries.BasicDataSettingsAndProcesses
{
    public record ConditionActionPair<T>(Predicate<T> Predicate, Action<T, string> Action, string Value = ""); //i like the idea of doing records.
}