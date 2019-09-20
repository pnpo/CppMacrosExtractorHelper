using MacrosAndIncludesInspector.Inspectors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MacrosAndIncludesInspector
{
    public class MacrosExtractAPI
    {
        public MacrosExtractAPI() { }


        public void ExtractFromProject(string path)
        {
            if (Directory.Exists(path))
            {
                ProcessDir(path);
            }
        }
        private void ProcessDir(string path)
        {
            Dictionary<string, string> macros = new Dictionary<string, string>();
            List<string> includes = new List<string>();
            try
            {
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                foreach (string fileName in fileEntries)
                {
                    ProcessFile(fileName, ref macros, ref includes);
                }

                GenerateJSonFile(macros, includes, path);


            }

            catch (Exception ex)
            {

            }
        }

        private void GenerateJSonFile(Dictionary<string, string> macros, List<string> includes, string path)
        {
            JObject root = new JObject();
            JArray jarray = new JArray();
            foreach (var elem in includes) {
                jarray.Add(elem);
            }
            JProperty global_include_paths = new JProperty("global_include_paths", jarray);
            JProperty projectCustomPaths = new JProperty("Project Custom Paths", new JObject(global_include_paths));
            JObject custom_Object = new JObject();
            custom_Object.Add(projectCustomPaths);
            JProperty custom = new JProperty("custom", custom_Object);

            foreach (var kvp in macros) {
                JProperty prop = new JProperty(kvp.Key, kvp.Value);
                custom_Object.Add(prop);
            }

            root.Add(custom);

            string json = JsonConvert.SerializeObject(root, Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText($"{path}\\CxCPPDefaultMacros.json", json);

        }

        static void ProcessFile(string fileName, ref Dictionary<string, string> macros, ref List<string> includes)
        {
            try
            {
                if (fileName.EndsWith("Makefile", true, null))
                {
                    MakefileInspector mkinspector = new MakefileInspector();
                   
                    includes.AddRange(mkinspector.ExtractIncludes(fileName));
                    var macrosMKFiles = mkinspector.ExtractMacros(fileName);
                    foreach (var kvp in macrosMKFiles)
                    {
                        macros.Add(kvp.Key, kvp.Value);
                    }

                }
                else if (fileName.EndsWith(".cmake", true, null) || fileName.EndsWith("CMakeLists.txt", true, null))
                {
                    CMakeInspector cmkInpsector = new CMakeInspector();
                    includes.AddRange(cmkInpsector.ExtractIncludes(fileName));
                    var macrosCMKFiles = cmkInpsector.ExtractMacros(fileName);
                    foreach (var kvp in macrosCMKFiles) {
                        macros.Add(kvp.Key, kvp.Value);
                    }

                }
                else if (fileName.EndsWith("compilation_database.json", true, null))
                {
                    CommandLineArgumentExtractor clExtractor = new CommandLineArgumentExtractor();
                    CompileDatabaseInspector cdbInspector = new CompileDatabaseInspector(fileName);
                    var commands = cdbInspector.extractCompilationCommand();
                    foreach (string c in commands)
                    {
                        Dictionary<string, string> cmdMacros = clExtractor.extractMacros(c);
                        foreach (var kvp in cmdMacros)
                        {
                            macros.Add(kvp.Key, kvp.Value);
                        }
                        includes.AddRange(clExtractor.extractIncludes(c));
                    }
                }
            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
