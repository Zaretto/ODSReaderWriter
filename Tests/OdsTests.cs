using Shouldly;
using System.Data;
using System.Reflection;
using System.Text;
using Zaretto.ODS;

namespace Ods
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Salary { get; set; }
    }

    [TestClass]
    public sealed class ReaderWriter
    {
        public ReaderWriter()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        public List<Person> GetTestData()
        {
            // Create sample data
            return new List<Person>
            {
                new Person { Id = 1, Name = "Walker",  Age = 30, DateOfBirth = new DateTime(1965, 12, 15), Salary = 50000.75 },
                new Person { Id = 2, Name = "Tanya",   Age = 29, DateOfBirth = new DateTime(1966, 10, 24), Salary = 60000.50 },
                new Person { Id = 3, Name = "Alice",   Age = 28, DateOfBirth = new DateTime(1967, 10, 22), Salary = 75000.00 }
            };
        }


        [TestMethod]
        public void ReadWrite()
        {
            var odsReaderWriter = new ODSReaderWriter();
            string tempPath = Path.Combine(Path.GetTempPath(), "test.ods");

            var list = GetTestData();
            odsReaderWriter.WriteOdsFile(list, "TestSheet", tempPath);
            var spreadsheetData = odsReaderWriter.ReadOdsFile(tempPath);
            if (spreadsheetData != null)
            {
                spreadsheetData.Tables.Count.ShouldBe(1);
                DataTable table = spreadsheetData.Tables[0];
                System.Console.WriteLine("Sheet {0}", table.TableName);
                foreach (var row in table.AsEnumerable())
                {
                    if (row.ItemArray.Any(ia => !string.IsNullOrEmpty(ia.ToString())))
                        System.Console.WriteLine("    {0}", string.Join(",", row.ItemArray.Select(xx => xx.ToString())));
                    var id = row.ItemArray.GetValue(0).ToString();
                    var item = list.FirstOrDefault(xx => xx.Id.ToString() == id);
                    item.ShouldNotBeNull();
                    row.ItemArray.GetValue(1).ToString().ShouldBe(item.Name);
                    row.ItemArray.GetValue(2).ToString().ShouldBe(item.Age.ToString());
                    row.ItemArray.GetValue(3).ToString().ShouldBe(item.DateOfBirth.ToString());
                    row.ItemArray.GetValue(4).ToString().ShouldBe(item.Salary.ToString());
                }
                //   odsReaderWriter.WriteOdsFile(spreadsheetData, "c:/temp/test.ods");
            }
        }
    }
}
