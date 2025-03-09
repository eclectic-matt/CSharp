using System.Text;
using BoardGamesETL.@class;

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
			throw new Exception("ERROR: FILE COULD NOT BE LOADED");
		}
		return fileData;
	}
}
