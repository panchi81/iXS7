using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

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

        //static void InputDataTest(string[] iXExportFileContent)
        //{
        //    List<iXExport> content = iXExportFileContent
        //        .Skip(1)
        //        .Select(v => iXExport.FromExportFile(v))
        //        .ToList();

        //    // Debug
        //    foreach (iXExport x in content)
        //    {
        //        Console.WriteLine(x.Name
        //            + "\t"
        //            + x.Address);
        //    }
        //}

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
        
        static void InputDataTest(string[] strs)
        {
            //    List<iXExport> content = iXExportFileContent
            //        .Skip(1)
            //        .Select(v => iXExport.FromExportFile(v))
            //        .ToList();            

            IEnumerable<string> tagName =

                //Skip Header
                from line in strs.Skip(1)

                // Split each row into array of strings
                let exportData = line.Split(',')

                // Select the tag name column
                select exportData[0];

            IEnumerable<string> tagAddressList =
                
                //
                from line in strs.Skip(1)

                //Split each row into array of strings
                let exportData = line.Split(',')

                // Skip Header
                //let dbData = exportData.Skip(1)

                // select the tag address column
                select exportData[2];

            //var results = query.ToList();
            var name = tagName.ToList();
            var address = tagAddressList.ToList();

            // Debug
            for (int i = 0; i < tagAddressList.Count(); i++)
            {
                Console.WriteLine(name[i] + "\t" + address[i]);
            }
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
        string Name;
        string DataType;
        string Address;

        public static iXExport FromExportFile(string exportLine)
        {
            string[] entries = exportLine.Split(',');
            iXExport ixexport = new iXExport();
            ixexport.Name = entries[0];
            ixexport.DataType = entries[1];
            ixexport.Address = entries[2];
            return ixexport;
        }
    }    
}
