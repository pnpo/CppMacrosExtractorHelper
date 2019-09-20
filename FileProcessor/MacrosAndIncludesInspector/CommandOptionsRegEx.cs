using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MacrosAndIncludesInspector
{
    public static class CommandOptionsRegEx
    {
        private static readonly string commandLineMacroNameValueString = @"(?<macro>[_a-zA-Z0-9]+)(?<value>(=('[^']'|\\?""[^""]*\\?""|[^- /\n]*)?)?)";
        private static readonly string commandLineFullMacroString = @"(?<macrodef>[-/]D" + commandLineMacroNameValueString + ")";

        public static readonly Regex CommandLineMacro = new Regex(commandLineFullMacroString);
        public static readonly Regex CommandLineInclude = new Regex(@"([-/]I|[-](iquote|isystem|idirafter))\s*(?<directory>[^-/\n]+)");
        public static readonly Regex CMakeIncludes = new Regex(@"include_directories\s*\((?<dir>[^,)]+,?)+\)");
        public static readonly Regex CMakeMacrosOld = new Regex($"add_definitions\\s*\\(({commandLineFullMacroString}\\s*)*\\)");
        public static readonly Regex CMakeMacrosNew = new Regex($"add_compile_definitions\\s*\\(({commandLineMacroNameValueString}?\\s*)*\\)");
    }
}
