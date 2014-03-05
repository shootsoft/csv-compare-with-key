using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csv_compare_with_key
{
    public class CSV
    {
        public Options Option { get; set; }

        private HashSet<string> idx;
        private int indexLineCount = 0;
        private int compareLineCount = 0;

        public CSV()
        {
            idx = new HashSet<string>();
        }

        public CSV(Options opt)
        {
            this.Option = opt;
            idx = new HashSet<string>();
        }

        /// <summary>
        /// Build csv Key index
        /// </summary>
        /// <returns></returns>
        public bool BuildIndex()
        {
            bool r = false;
            try
            {
                using (StreamReader sr = new StreamReader(Option.IndexCSV, Option.GetEncoding()))
                {
                    string line = sr.ReadLine();
                    indexLineCount = 0;
                    while (line != null)
                    {
                        string[] cols = line.Split(Option.Split);
                        string key = GetKey(cols, Option.Trims, Option.IndexCSVKeys);
                        if (!idx.Contains(key))
                        {
                            idx.Add(key);
                        }
                        line = sr.ReadLine();
                        indexLineCount++;
                        if (indexLineCount % 100 == 0)
                        {
                            Console.WriteLine("Index build with " + indexLineCount);
                        }
                    }
                }
                Console.WriteLine("Index build done with " + indexLineCount);
                r = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return r;

        }


        /// <summary>
        /// Get the cols key 
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="trims"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GetKey(string[] cols, char[] trims, int[] keys)
        {
            StringBuilder sb = new StringBuilder();
            if (cols != null && keys != null)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if (cols.Length > keys[i])
                    {
                        sb.Append(cols[keys[i]].Trim(trims));
                    }
                }
            }
            return sb.ToString();

        }


        public bool SearchAndGenerate()
        {
            bool r = false;

            try
            {
                string sameKeyFile = Option.CompareCSV + ".same";
                string diffKeyFile = Option.CompareCSV + ".diff";
                string summaryFile = Option.CompareCSV + ".summary";
                using(StreamReader sr = new StreamReader(Option.CompareCSV, Option.GetEncoding()))
                {
                    StreamWriter swSame = CreateWriter(sameKeyFile);
                    StreamWriter swDiff = CreateWriter(diffKeyFile);
                    StreamWriter swSummary = CreateWriter(summaryFile);
                    string line = sr.ReadLine();
                    int sameLineCount = 0;
                    int diffLineCount = 0;
                    while(line!=null)
                    {
                        string[] cols = line.Split(Option.Split);
                        string key = GetKey(cols, Option.Trims, Option.CompareCSVKeys);
                        if (idx.Contains(key))
                        {
                            swSame.WriteLine(line);
                            sameLineCount++;
                        }
                        else
                        {
                            swDiff.WriteLine(line);
                            diffLineCount++;
                        }

                        line = sr.ReadLine();
                        compareLineCount++;
                        if (compareLineCount % 100 == 0)
                        {
                            Console.WriteLine("Compared record " + compareLineCount);
                        }
                    }
                    Console.WriteLine("Compared done " + compareLineCount);
                    swSummary.WriteLine("Index csv line count: " + this.indexLineCount);
                    swSummary.WriteLine("Index csv key count: " + this.idx.Count);
                    swSummary.WriteLine("Compare csv line count:" + compareLineCount);
                    swSummary.WriteLine("Same key line count: " + sameLineCount);
                    swSummary.WriteLine("Diff key line count: " + diffLineCount);

                    swSame.Close();
                    swDiff.Close();
                    swSummary.Close();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return r;

        }

        private StreamWriter CreateWriter(string sameKeyFile)
        {
            StreamWriter writer = new StreamWriter(sameKeyFile, false, Option.GetEncoding());
            writer.NewLine = Option.UnixNewLine ? "\n" : "\r\n";
            return writer;
        }

    }
}
