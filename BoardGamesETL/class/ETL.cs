namespace BoardGamesETL.@class;

/**
 * Abstracted implementation of an Extract Transform Load structure.
 */
abstract public class ETL 
{
	//The path to the resource being loaded
	string resourcePath = "";
	//The handle to the resource
	object? resourceHandle;
	//Array of string data
	string[]? fileData;

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

	public string[] FileData
	{
		get { return fileData != null ? fileData : []; }
		set { fileData = value; }
	}

	public string[] Filter(IEnumerable<string>? queryString)
	{
		//If there is no fileData, return an empty array
		if(fileData == null) return [];
		//If there is no queryString, return the full fileData array
		if(queryString == null) return fileData;
		
		//Prepare return array
		string[] returnData = [];
		//Apply filter
		foreach (string str in queryString){
			Console.WriteLine("QUERY FILTER: " + str);
			//GET A VALID STRING (USING AN EMPTY STRING IF THE line IS null)
			string lineData = str == null ? String.Empty : str;
			//RESIZE THE FILE DATA ARRAY BY 1
			Array.Resize(ref returnData, fileData.Length + 1);
			//SET THE FINAL ELEMENT OF THE ARRAY TO BE THE NEW LINE DATA
			fileData.SetValue(returnData, fileData.Length - 1);
		}
		//RETURN THE RESULT
		return returnData;
	}
}
