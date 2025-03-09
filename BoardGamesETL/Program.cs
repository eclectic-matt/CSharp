/**
 * Board Games Extract-Transform-Load program. 
 */

using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Data;
using System.Text.RegularExpressions;

string RemoveSpecialCharacters(string str)
{
	//VALID CHARACTERS a-z, A-Z, 0-9, _.',:()! 
    return Regex.Replace(str, "[^a-zA-Z0-9_.',:()! ]+", "", RegexOptions.Compiled);
}

/*
//LOAD THE ENTIRE BGG RANKINGS AS CSV FILE
string csvFilePath = @"S:\Downloads\boardgames_ranks_2025-03-09\boardgames_ranks.csv";
var csv = new CSV();
//LOADING THE FILE INTO AN ARRAY
Console.WriteLine("Output ALL lines in the CSV:");
string[] fileContents = csv.ReadFile(csvFilePath);
foreach(string line in fileContents){
	Console.WriteLine(line);
}
*/

Console.WriteLine(" ");
Console.WriteLine("GET FILTERED BG DATA FROM XML BGG EXPORT:");

//https://boardgamegeek.com/xmlapi/search?search=a
var bggExportFilePath = "S:/Downloads/bggSearch_a.xml";
XElement bgList = XElement.Load(bggExportFilePath);
IEnumerable<BoardGame> bgQuery = 
	from boardgame in bgList.Descendants("boardgame")
	where boardgame.Element("yearpublished") != null
	select new BoardGame (
		Name: (string) boardgame.Element("name"),
		YearPublished: (int) boardgame.Element("yearpublished")
	);

//==================
// DATABASE (MySQL)
//==================
//SEE: http://localhost:8080/phpmyadmin/sql.php?db=test&table=board_games&pos=0
Console.WriteLine(" ");
Console.WriteLine(" ");
Console.WriteLine("Connect to a MySQL Database");

//Initialize DB connection
var dbCon = DBConnection.Instance();
//Localhost
dbCon.Server = "127.0.0.1";
//test db (no authentication setup)
dbCon.DatabaseName = "test";
dbCon.UserName = "root";
dbCon.Password = "";
string tableName = "board_games";

int totalRowsInserted = 0;
//If a connection was made
if (dbCon.IsConnect())
{
	foreach (BoardGame game in bgQuery)
	{
		//REPLACE SPECIAL CHARS FROM THE GAME NAME STRING 
		string gameName = RemoveSpecialCharacters(game.Name);
		string insertQuery = $"INSERT INTO {tableName} (name, year_published) VALUES ( \"{gameName}\", {game.YearPublished} );";
		var insertCmd = new MySqlCommand(insertQuery, dbCon.Connection);
		int rowsInserted = insertCmd.ExecuteNonQuery();
		totalRowsInserted += rowsInserted;
		Console.WriteLine($"Inserted {rowsInserted} rows for the game {game.Name}");
	}
	dbCon.Close();
}

Console.WriteLine($"Inserted {totalRowsInserted} Board Games into the Database");
/*Console.WriteLine("FILTERED RESULTS COUNT: " + bgQuery.Count());
//Execute the query
foreach (BoardGame game in bgQuery)
{
	//Console.WriteLine("GAME '" + game.Name + "' - released: " + game.YearPublished);

}*/


public record BoardGame (string Name, int YearPublished);

