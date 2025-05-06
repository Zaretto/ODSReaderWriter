### C# Open Document Spreadsheet Reader Writer

This is a simple ODS[1] reader writer. 

Based on the work of Josip Kremenic[2]


### Reading

    var odsReaderWriter = new ODSReaderWriter();
    var spreadsheetData = odsReaderWriter.ReadOdsFile(tempPath);
    DataTable table = spreadsheetData.Tables[0];
    System.Console.WriteLine("Sheet {0}", table.TableName);
    foreach (DataTable table in spreadsheetData.Tables)
    {
        System.Console.WriteLine("Sheet {0}", table.TableName);
        foreach (var row in table.AsEnumerable())
        {
            if (row.ItemArray.Any(ia => !string.IsNullOrEmpty(ia.ToString())))
                System.Console.WriteLine("    {0}", string.Join(",", row.ItemArray.Select(xx => xx.ToString())));
        }
    }
    
### Writing

#### Simple list
    var odsReaderWriter = new ODSReaderWriter();
    List<Person> people = new List<Person>
    {
        new Person { Id = 1, Name = "Alice", Salary = 50000.5 },
        new Person { Id = 2, Name = "Bob", Salary = 60000.75 }
    };
    odsReaderWriter.WriteOdsFile(people, "People", tempPath);

#### Dictionary list
    var dictionary = new Dictionary<string, IEnumerable<Person>> {
        { "Sheet1", GetTestData() },
        { "Sheet2", GetTestData() }
    };
    odsReaderWriter.WriteOdsFile(dictionary, "test-d.ods");
    
### NOTES

Tested only with reading LibreOffice.

### Developers

* Richard Harrison
* Josip Kremenic
* Nathan Crawford (@njcrawford )

----
[1] https://opendocumentformat.org/

[2] https://www.codeproject.com/Articles/38425/How-to-Read-and-Write-ODF-ODS-Files-OpenDocument-2

