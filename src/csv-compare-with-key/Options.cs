using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csv_compare_with_key
{
    public class Options
    {
        [Option('i', "index-csv", Required = true,
            HelpText = "Input csv file to be indexed.")]
        public string IndexCSV { get; set; }

        [OptionArray('k', "index-keys", Required = true,
            HelpText = "Input csv file to be indexed key columns.")]
        public int[] IndexCSVKeys {get;set;}

        [Option('c', "compare-csv", Required = true,
            HelpText = "Input csv file to be compared.")]
        public string CompareCSV { get; set; }

        [OptionArray('e', "compare-keys", Required = true,
            HelpText = "Input csv file to be compared.")]
        public int[] CompareCSVKeys { get; set; }

        [Option('t', "trimes", Required = false, DefaultValue = new char[]{'"'},
            HelpText = "Trimed column chars.")]
        public char[] Trims { get; set; }

        [Option('u', "is-utf8", Required = false, DefaultValue = true,
            HelpText = "Using UTF-8 encoding.")]
        public bool IsUTF8 { get; set; }

        [Option('s', "split", Required = false, DefaultValue = new char[] { ',', '\t' },
            HelpText = "Csv split.")]
        public char[] Split { get; set; }

        [Option('n', "unix-new-line", Required = false, DefaultValue = true,
            HelpText = "Using unix new line.")]
        public bool UnixNewLine { get; set; }

        public Options() 
        {

        }

        public Encoding GetEncoding()
        { 
            return IsUTF8 ? Encoding.UTF8: Encoding.Default;
        }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
