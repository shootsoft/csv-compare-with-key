using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csv_compare_with_key
{
    class Program
    {
        static void Main(string[] args)
        {

            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                CSV csv = new CSV(options);
                csv.BuildIndex();
                csv.SearchAndGenerate();
            }
            Console.Read();
            Console.WriteLine("Press any key to exit...");
        }
    }
}
