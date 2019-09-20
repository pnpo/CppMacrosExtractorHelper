using MacrosAndIncludesInspector.Inspectors;
using System;
using System.Collections.Generic;

namespace MacrosAndIncludesInspector
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbfile = @"C:\users\nunoo\desktop\testing\compilation_database.json";

            var cmakeF1 = @"C:\users\nunoo\desktop\testing\CMakeLists.txt";
            var cmakeF2 = @"C:\users\nunoo\desktop\testing\file1.cmake";
            var cmakeF3 = @"C:\users\nunoo\desktop\testing\file2.cmake";
            var cmakeF4 = @"C:\users\nunoo\desktop\testing\file3.cmake";
            var cmakeF5 = @"C:\users\nunoo\desktop\testing\file4.cmake";
            var cmakeF6 = @"C:\users\nunoo\desktop\testing\file1_defintions.cmake";


            var gccCommand = @"gcc main.c -DFOO -DBAR=10";
            var gccCommandWithIncludes = @"gcc main.c -DFOO -DBAR=10 -I \myincludes -I\otherIncludes";
            var gccCommandWithIncludes2 = @"gcc main.c -iquote \myincludes -idirafter\otherIncludes";
            var clangCommand = @"clang++ main.c /DFOO2 -DBAR=";
            var clangCommandWithIncludes = @"gcc main.c /DFOO -DBAR=10 /I \myincludes -I\otherIncludes";
            var clCommand = @"clang++ main.c /DFOO2 /DBAR2=";
            var clCommandWithIncludes = @"gcc main.c /DFOO /DBAR=10 /I \myincludes /I\otherIncludes";

            CommandLineArgumentExtractor commandExtractor = new CommandLineArgumentExtractor();

            Dictionary<string, string> result1 = commandExtractor.extractMacros(gccCommand);
            Dictionary<string, string> result2 = commandExtractor.extractMacros(gccCommandWithIncludes);
            List<string> result3 = commandExtractor.extractIncludes(gccCommandWithIncludes);
            Dictionary<string, string> result4 = commandExtractor.extractMacros(gccCommandWithIncludes2);
            List<string> result5 = commandExtractor.extractIncludes(gccCommandWithIncludes2);

            Dictionary<string, string> result6 = commandExtractor.extractMacros(clangCommand);
            List<string> result7 = commandExtractor.extractIncludes(clangCommandWithIncludes);

            Dictionary<string, string> result8 = commandExtractor.extractMacros(clCommand);
            List<string> result9 = commandExtractor.extractIncludes(clCommandWithIncludes);


            CompileDatabaseInspector compileDatabaseInspector = new CompileDatabaseInspector(dbfile);

            List<string> command = compileDatabaseInspector.extractCompilationCommand();
            foreach (string c in command)
            {
                Dictionary<string, string> macros = commandExtractor.extractMacros(c);
                List<string> directories = commandExtractor.extractIncludes(c);
            }


            CMakeInspector cmakeInspector = new CMakeInspector();

            List<string> result10 = cmakeInspector.ExtractIncludes(cmakeF1);
            List<string> result11 = cmakeInspector.ExtractIncludes(cmakeF2);
            List<string> result12 = cmakeInspector.ExtractIncludes(cmakeF3);
            List<string> result13 = cmakeInspector.ExtractIncludes(cmakeF4);
            List<string> result14 = cmakeInspector.ExtractIncludes(cmakeF5);

            Dictionary<string, string> result15 = cmakeInspector.ExtractMacros(cmakeF1);
            Dictionary<string, string> result16 = cmakeInspector.ExtractMacros(cmakeF2);
            Dictionary<string, string> result17 = cmakeInspector.ExtractMacros(cmakeF3);
            Dictionary<string, string> result18 = cmakeInspector.ExtractMacros(cmakeF4);
            Dictionary<string, string> result19 = cmakeInspector.ExtractMacros(cmakeF5);
            Dictionary<string, string> result20 = cmakeInspector.ExtractMacros(cmakeF6);




        }
    }
}
