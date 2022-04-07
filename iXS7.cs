using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;

namespace iXS7
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://www.google.com/search?client=firefox-b-d&q=c%23+csv+file+site%3Adocs.microsoft.com
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-reorder-the-fields-of-a-delimited-file-linq
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-compute-column-values-in-a-csv-text-file-linq

            // (Re-) Construct a valid un-optimized Siemens S7 DataBlock using an iX Tag-Export file addressing (possibly) fractured DataBlocks.

            /* Input format (.txt):
             * // Name,DataType,Address_1 //
             * RCP_VD_12,INT32,DB10.DBD12
             * RCP_VB_27, INT16,DB10.DBB27
             * 
             * Output format (.scl, .db):
             * DATA_BLOCK "DB10"
             * { S7_Optimized_Access := 'FALSE' }
             * VERSION : 0.1
             * NON_RETAIN
             *    VAR
             *        DB10_PADDING0 : Real;
             *        RCP_VD_4 : Real;
             *        RCP_VD_8 : Real;
             *        RCP_VD_12 : Real;
             *        RCP_VW_16 : Word;
             *        DB10_PADDING1 : Byte;
             *        DB10_PADDING2 : Byte;
             *        DB10_PADDING3 : Byte;
             *        DB10_PADDING4 : Byte;
             *        DB10_PADDING5 : Byte;
             *        DB10_PADDING6 : Byte;
             *        DB10_PADDING7 : Byte;
             *        DB10_PADDING8 : Byte;
             *        DB10_PADDING9 : Byte;
             *        RCP_VB_26 : Byte;
             *        RCP_VB_27 : Byte;
             *        DB10_PADDING10 : Byte;
             *        DB10_PADDING11 : Byte;
             *        RCP_V_30_0 : Bool;
             *    END_VAR
             */


            // Parse input .csv file    -   InputData()
            // Determine (number of) DataBlocks
            // Create Dictionary as <String> DB, <List> Address
            // Interpolate missing addresses in list of addresses for each DB
            // Write output file in SCL-format

            // Valid path to input/output file
            string iPath = @"C:\Users\FRAGO\Documents\py\Projects\iXS7\input\iX_exp.txt";
            string oPath = @"C:\Users\FRAGO\Documents\py\Projects\iXS7\output\S7_imp.scl";

            try
            {
                if (!File.Exists(iPath))
                {
                    throw new FileNotFoundException();
                }

                // Debug
                Console.WriteLine("\nReading file...\n\n");

                // String array of lines from input file
                string[] lines = File.ReadAllLines(iPath);

                //List<string> DB_addresses = InputDataTest(lines);
                InputDataTest(lines);
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine("File- or Path Error");
            }

            catch (IOException e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }

            // Debug
            Console.ReadKey();
        }

        static void InputDataTest(string[] iXExportFileContent)
        {
            // Locate header
            foreach (string line in iXExportFileContent)
            {
                if (line.StartsWith("// "))
                {
                    string tempHeader = line.Trim(new char[] { ' ', '/' });
                    string[] columns = tempHeader.Split(',');
                    break;
                }
            }

            List<iXExport> content = iXExportFileContent
                .Skip(1)
                .Select(line => new iXExport(columns, line))
                .ToList();

            // Debug
            foreach (iXExport tag in content)
            {
                Console.WriteLine(tag.S7DataType
                    + "\t"
                    + tag.ByteAddress);
            }
        }

        static void InputData(IEnumerable<string> strs)
        {
            IEnumerable<string> tagName =

                //
                from line in strs

                    // Split each row into array of strings
                let exportData = line.Split(',')

                // Skip Header
                //let dbData = exportData.Skip(1)

                // Select the tag name column
                select exportData[0];

            IEnumerable<string> tagAddressList =

                //
                from line in strs

                    // Split each row into array of strings
                let exportData = line.Split(',')

                // Skip Header
                //let dbData = exportData.Skip(1)

                // select the tag address column
                select exportData[2];

            //var results = query.ToList();
            var name = tagName.ToList();
            var address = tagAddressList.ToList();

            // Dictionary<string, string> dataBlocks;

            // Debug
            for (int i = 0; i < tagAddressList.Count(); i++)
            {
                Console.WriteLine(name[i] + "\t" + address[i]);
            }
        }

        static List<string> InputDataTest2(string[] strs)
        {
            //IEnumerable<string> tagName =

            //    // Skip Header
            //    from line in strs.Skip(1)

            //    // Split each row into array of strings
            //    let exportData = line.Split(',')

            //    // Select the tag name column
            //    select exportData[0];

            //// Convert query result to list
            //var name = tagName.ToList();

            IEnumerable<string> tagAddressList =

                // Skip Header
                from line in strs.Skip(1)

                    // Split each row into array of strings
                let exportData = line.Split(',')

                // select the tag address column
                select exportData[2];

            //var results = query.ToList();
            //var address = tagAddressList.ToList();

            return tagAddressList.ToList();

            //// Debug
            //for (int i = 0; i < tagAddressList.Count(); i++)
            //{
            //    Console.WriteLine(name[i] + "\t" + address[i]);
            //}
        }

        private static void OutputFile(string foutPath)
        {
            try
            {
                if (!Directory.Exists(foutPath))
                {
                    throw new DirectoryNotFoundException();
                }
                // Write to file
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Output Directory Error");
            }
        }
    }
    class iXExport
    {
        public string Name { get; private set; }
        public string FullAddress { get; private set; }
        public string DB { get; private set; }
        public string iXDataType { get; private set; }
        public string S7DataType { get; private set; }
        public int ByteAddress { get; private set; }
        public int BitAddress { get; private set; }

        // pass in the order of the columns, instead of the headers...
        public iXExport(Dictionary<string, int> columns, string exportLine)
        {
            // Split each string, delimiter = ','
            string[] col = exportLine.Split(',');

            string value = "";
            if (columns.TryGetValue("Name", out value))
            {
                Name = col[columns["Name"]];
            }

            if (columns.TryGetValue("iXDataType", out value))
            {
                iXDataType = col[columns["iXDataType"]];
            }

            if (columns.TryGetValue("FullAddress", out value))
            {
                FullAddress = col[columns["FullAddress"]];
            }

            // Change this!!!!
            // Assign superficial properties
            FullAddress = col[2];
            iXDataType = col[1];

            // String manipulation to extract the "profound" properties
            string[] temp = FullAddress.Split('.');
            DB = temp[0].ToString();

            S7DataType = Regex.Replace(temp[1].ToString(), @"[0-9$]", ""); // Filter out the numeric address value
            ByteAddress = Convert.ToInt32(Regex.Replace(temp[1], @"[a-zA-Z]", "")); // Filter out the alphanumerical chars

            if (temp.Length > 2)
            {
                BitAddress = Convert.ToInt32(temp[2]); // might not exist, check first
            }
            else
            {
                BitAddress = 0;
            }
        }
        public void getHeaderVals(string[] headers)
        {
            // identify headers
            Dictionary<string, int> idx = new Dictionary<string, int>();

            for (int i = 0; i < headers.Count(); i++)
            {
                switch (headers[i])
                {
                    case string entry when entry.StartsWith("Name"):
                        idx.Add("Name", i);
                        break;

                    case string entry when entry.StartsWith("DataType"):
                        idx.Add("iXDataType", i);
                        break;

                    case string entry when entry.StartsWith("Address_"):
                        idx.Add("FullAddress", i);
                        break;
                }
            }
        }
    }
}
