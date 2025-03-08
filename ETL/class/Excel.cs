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