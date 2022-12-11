
using System.Data;
using System.Linq;
using System.Text;
using Zaretto.ODS;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var odsReaderWriter = new ODSReaderWriter();
var spreadsheetData = odsReaderWriter.ReadOdsFile(@"c:\temp\ods-test.ods");
if (spreadsheetData != null)
{
    foreach (DataTable table in spreadsheetData.Tables)
    {
        System.Console.WriteLine("Sheet {0}", table.TableName);
        foreach (var row in table.AsEnumerable())
        {
            if (row.ItemArray.Any(ia => !string.IsNullOrEmpty(ia.ToString())))
                System.Console.WriteLine("    {0}", string.Join(",", row.ItemArray.Select(xx => xx.ToString())));
        }
    }
    odsReaderWriter.WriteOdsFile(spreadsheetData, "c:/temp/test.ods");
}
