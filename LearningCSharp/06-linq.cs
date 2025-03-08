using System.Collections;
using OfficeOpenXml;
using System.Xml.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;

public static class LinqTests 
{
	public static System.Collections.IEnumerable SomeNumbers() 
	{
		yield return 4;
		yield return 3;
		yield return 5;
	}

	public static System.Collections.Generic.IEnumerable<int> EvenSequence(int firstNumber, int lastNumber)
	{
		for(int number = firstNumber; number <= lastNumber; number++)
		{
			if(number % 2 == 0){
				yield return number;
			}
		}
	}

	public static System.Collections.Generic.IEnumerable<string> GetDaysOfWeek()
	{
		string[] days = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
		for(int index = 0; index < days.Length; index++)
		{
			yield return days[index];
		}
	}
}

/**
 * Abstracted implementation of an Extract Transform Load structure.
 */
public class ETL 
{
	string resourcePath = "";
	object? resourceHandle;

	//Get/Set the resource path (url or filepath)
	public string ResourcePath 
	{
		get { return resourcePath; }
		set { resourcePath = value; }
	}
	public object ResourceHandle 
	{
		get { return resourceHandle != null ? resourceHandle : false; }
		set { resourceHandle = value; }
	}
}

public class CSV : ETL 
{
	//NO PACKAGE HANDLER REQUIRED
	string[]? fileData;
	public string[] ReadFile(string path)
	{
		try {
			//INIT FILE DATA
			fileData = [];
			//INITIALIZE THE STREAM READER THAT WILL PARSE THE CSV FILE
			using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
			{
				//WHILE THERE ARE STILL LINES TO READ
				while (streamReader.Peek() >= 0)
				{
					//READ THE CURRENT LINE (MAY BE null)
					string? line = streamReader.ReadLine();
					//GET A VALID STRING (USING AN EMPTY STRING IF THE line IS null)
					string lineData = line == null ? String.Empty : line;
					//RESIZE THE FILE DATA ARRAY BY 1
					Array.Resize(ref fileData, fileData.Length + 1);
					//SET THE FINAL ELEMENT OF THE ARRAY TO BE THE NEW LINE DATA
					fileData.SetValue(lineData, fileData.Length - 1);
				}
			}
		}catch{
			return ["ERROR: FILE COULD NOT BE LOADED"];
		}
		return fileData;
	}
}

public class GoogleSheet : ETL 
{
	string[]? fileData;
	bool loaded = false;
	public async Task ReadFile(string path)
	{
		//SET LOADED TO FALSE
		loaded = false;
		try {
			//INIT FILE DATA
			fileData = [];
			using (HttpClient client = new HttpClient())
			{
				//Setup an async method which loads the entire url contents into a string
				string s = await client.GetStringAsync(path);
				//fileData[] = s.Split("\n");
				//Output the string to the console
				Console.WriteLine(s);
			}
			loaded = true;
		}catch{
			throw new Exception("Error loading from file");
		}
	}

	public string[] GetLoadedData()
	{
		if(!loaded) return [];
		//ELSE
		return fileData;
	}
}

public class Excel : ETL
{
	//THE EXCEL PACKAGE HANDLE
	ExcelPackage? xlPackage;
	ExcelWorksheet? ws;

	public void LoadFile(string path)
	{
		//SET THE RESOURCE PATH
		ResourcePath = path;
		//SET THE LICENSE TO NON-COMMERCIAL
		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		//SET UP THE EXCEL PACKAGE
		xlPackage = new ExcelPackage(new FileInfo(@path));
	}

	public void LoadSheetByName(string name)
	{
		//EXIT SILENTLY IF NO EXCEL FILE LOADED
		if(xlPackage == null) return;
		//LOAD THE WORKSHEET BY NAME
		ws = xlPackage.Workbook.Worksheets[name];
	}

	public void LoadSheetByIndex(int sheetId)
	{
		//EXIT SILENTLY IF NO EXCEL FILE LOADED
		if(xlPackage == null) return;
		//HANDLE 0- OR 1-INDEXED WORKSHEETS
		/*if(ExcelPackage.Compatibility.IsWorksheets1Based == true){
			//INCREMENT THE SHEET ID (ASSUMES 0-INDEXED)
			sheetId += 1;
		}*/

		//LOAD THE WORKSHEET BY SHEET INDEX
		ws = xlPackage.Workbook.Worksheets[sheetId];

	}

	public ValidationResponse GetTotalRows()
	{
		var result = new ValidationResponse();
		result.Successful = false;
		result.Information = "No Excel Workbook loaded! Use LoadFile() first!";

		//CANNOT HANDLE IF NO xlPackage
		if(xlPackage == null) return result;

		//CANNOT HANDLE IF NO ws
		result.Information = "No Excel Worksheet loaded! Use LoadSheetByName() or LoadSheetByIndex() first!";
		if(ws == null) return result;

		//LOAD THE SHEET (NOW SEPARATED INTO THE LoadSheet... METHODS)
		//ws = xlPackage.Workbook.Worksheets[sheetId];

		//CAN NOW COUNT ROWS - PREPARE result
		result.Successful = true;
		result.Information = ws.Dimension.End.Row.ToString();

		return result;
	}

	public ValidationResponse GetTotalColumns()
	{
		var result = new ValidationResponse();
		result.Successful = false;
		result.Information = "No Excel Workbook loaded! Use LoadFile() first!";

		//CANNOT HANDLE IF NO xlPackage
		if(xlPackage == null) return result;

		//CANNOT HANDLE IF NO ws
		result.Information = "No Excel Worksheet loaded! Use LoadSheetByName() or LoadSheetByIndex() first!";
		if(ws == null) return result;

		//LOAD THE SHEET (NOW SEPARATED INTO THE LoadSheet... METHODS)
		//ws = xlPackage.Workbook.Worksheets[sheetId];
		
		//CAN NOW COUNT ROWS - PREPARE result
		result.Successful = true;
		result.Information = ws.Dimension.End.Column.ToString();

		return result;
	}

	public ValidationResponse GetColumnData(int columnId = 1)
	{
		//INITIALIZE THE RESPONSE OBJECT
		var result = new ValidationResponse();
		result.Successful = false;
		result.Information = "Column ID must be 1 or greater!";
		//COLUMNS ARE 1-INDEXED - HANDLE LESS THAN 1
		if(columnId < 1) return result;
		
		//Initialise a string builder to hold the data
		var sb = new StringBuilder();
		//Iterate over rows in the data
		int totalRows = int.Parse(GetTotalRows().Information);
		for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
		{
			//Get the data from the requested column ID
			var row = ws.Cells[rowNum, columnId, rowNum, columnId].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
			sb.AppendLine(string.Join(",", row));
		}
		result.Successful = true;
		result.Information = sb.ToString();

		return result;
	}

}

/**
 * A standard "response" object where an extract/load/transform fails.
 */
public class ValidationResponse
{
	//Successful: true if the method was successful
	public bool Successful { get; set; }
	//Information: returns failure information, or the result of the method as a string
	public string? Information { get; set; }
}

