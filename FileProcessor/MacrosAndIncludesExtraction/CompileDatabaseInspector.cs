using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MacrosAndIncludesInspector
{

    /// <summary>
    /// Reads JSON Compiler Databse Files.
    /// The format of these files is as follows:
    /// 
    /// [
    ///    { 
    ///      "directory": "...",
    ///      "command": "...", -- the command used (either this or command shall exist in the file)
    ///      "arguments" : "...", -- the arguments used in the command (either this or command shall exist in the file)
    ///      "file": "...",
    ///      "output" : "..."
    ///     },
    /// ]
    /// </summary>

    public class CompileDatabaseInspector
    {
        private readonly JArray compileDatabase;

        public CompileDatabaseInspector(string compileDatabasePath) {

            if (File.Exists(compileDatabasePath)) {
                using (StreamReader reader = File.OpenText(compileDatabasePath))
                {
                    this.compileDatabase = (JArray)JToken.ReadFrom(new JsonTextReader(reader));
                }
            }
        }


        public List<string> extractCompilationCommand() {
            var commands = new List<string>();
            foreach (JObject entry in this.compileDatabase) {
                string command = (string)entry["command"];
                string arguments = (string)entry["arguments"];
                if(command != null)
                    commands.Add(command);
                if(arguments != null)
                    commands.Add(arguments);
            }

            return commands;
        }

    
    }
}
