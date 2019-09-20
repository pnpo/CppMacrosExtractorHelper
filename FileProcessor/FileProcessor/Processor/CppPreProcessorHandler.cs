using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FileProcessor.Processor.Parser;
using FileProcessor.Processor.Visitor;

namespace FileProcessor.Processor
{
    public class CppPreProcessorHandler
    {
        public CppPreProcessorHandler(string fileContent, string fileName)
        {
            PreProcessFile(fileContent, fileName);
        }

        private void AppendNewLineToFileContent(ref string content)
        {
            if (!content.EndsWith("\r\n") && !content.EndsWith("\n") && !content.EndsWith("\r") || content.EndsWith("\\\r") || content.EndsWith("\\\n") || content.EndsWith("\\\r\n"))
            {
                content += Environment.NewLine;
            }
        }

        protected IParseTree GetPreProcessorParseTree(string code, string fileName)
        {
            var input = new AntlrInputStream(code);
            var lexer = new MacroLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MacroParser(tokens);
            var context = parser.preprocess();
            return context;
        }

        protected virtual CPreProcessorVisitor GetVisitor(string fileContent)
        {
            return new CPreProcessorVisitor(fileContent);
        }

        private void PreProcessFile(string fileContent, string fileName)
        {
            AppendNewLineToFileContent(ref fileContent);

            // TOKENIZATION
            IParseTree context = GetPreProcessorParseTree(fileContent, fileName);
            
            // PREPROCESSING
            var preprocessVisitor = GetVisitor(fileContent);
            preprocessVisitor.Visit(context);
            fileContent = preprocessVisitor.PreProcessedCode;

            preprocessVisitor.GenerateJsonFile(fileName);
        }
    }
}
