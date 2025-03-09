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

//LOAD THE ENTIRE BGG RANKINGS AS CSV FILE (source: https://boardgamegeek.com/data_dumps/bg_ranks)
string csvFilePath = @"S:\Downloads\boardgames_ranks_2025-03-09\boardgames_ranks.csv";
var csv = new CSV();
//LOADING THE FILE INTO AN ARRAY
Console.WriteLine("Output ALL lines in the CSV:");
string[] fileContents = csv.ReadFile(csvFilePath);
/*foreach(string line in fileContents){
	Console.WriteLine(line);
}*/

//OTHER DATA SOURCES: 
// - https://geekgroup.app/users/EclecticMatt/plays
// - https://boardgamegeek.com/xmlapi/search?search=a
// - https://boardgamegeek.com/data_dumps/bg_ranks


/*
id
name
yearpublished
rank
bayesaverage
average
usersrated
is_expansion
abstracts_rank
cgs_rank
childrensgames_rank
familygames_rank
partygames_rank
strategygames_rank
thematic_rank
wargames_rank
*/
//Int32.TryParse(value, out number) 
//int.TryParse(s, out i) ? i : 42;

/*IEnumerable<BoardGame> bgQuery = 
	from boardgame in fileContents
	where boardgame != null
	select new BoardGame (
		BggId: (int) int.Parse(boardgame.Split(",")[0]),
		Name: (string) boardgame.Split(",")[1],
		YearPublished: (int) int.Parse(boardgame.Split(",")[2]),
		OverallRank: (int) int.Parse(boardgame.Split(",")[3]),
		BayesAverage: (float) float.Parse(boardgame.Split(",")[4]),
		Average: (float) float.Parse(boardgame.Split(",")[5]),
		UsersRated: (int) int.Parse(boardgame.Split(",")[6]),
		IsExpansion: (bool) (boardgame.Split(",")[7] != "0"),
		AbstractsRank: (int) int.Parse(boardgame.Split(",")[8]),
		CgsRank: (int) int.Parse(boardgame.Split(",")[9]),
		ChildrenGamesRank: (int) int.Parse(boardgame.Split(",")[10]),
		FamilyGamesRank: (int) int.Parse(boardgame.Split(",")[11]),
		PartyGamesRank: (int) int.Parse(boardgame.Split(",")[12]),
		StrategyGamesRank: (int) int.Parse(boardgame.Split(",")[13]),
		ThematicGamesRank: (int) int.Parse(boardgame.Split(",")[14]),
		WargamesRank: (int) int.Parse(boardgame.Split(",")[15])
	);
*/

IEnumerable<BoardGame> bgQuery = 
	from boardgame in fileContents
	where boardgame != null
	select new BoardGame (
		BggId: int.TryParse(boardgame.Split(",")[0], out int bggId) ? bggId : 0,
		Name: (string) boardgame.Split(",")[1],
		YearPublished: int.TryParse(boardgame.Split(",")[2], out int year) ? year : 1900,
		OverallRank: int.TryParse(boardgame.Split(",")[3], out int rank) ? rank : 0,
		BayesAverage: float.TryParse(boardgame.Split(",")[4], out float bAv) ? bAv : 0,
		Average: float.TryParse(boardgame.Split(",")[5], out float av) ? av : 0,
		UsersRated: int.TryParse(boardgame.Split(",")[6], out int uRate) ? uRate : 0,
		IsExpansion: (bool) (boardgame.Split(",")[7] != "0"),
		AbstractsRank: int.TryParse(boardgame.Split(",")[8], out int abRnk) ? abRnk : 0,
		CgsRank: int.TryParse(boardgame.Split(",")[9], out int cgsRnk) ? cgsRnk : 0,
		ChildrenGamesRank: int.TryParse(boardgame.Split(",")[10], out int chiRnk) ? chiRnk : 0,
		FamilyGamesRank: int.TryParse(boardgame.Split(",")[11], out int famRnk) ? famRnk : 0,
		PartyGamesRank: int.TryParse(boardgame.Split(",")[12], out int parRnk) ? parRnk : 0,
		StrategyGamesRank: int.TryParse(boardgame.Split(",")[13], out int strRnk) ? strRnk : 0,
		ThematicGamesRank: int.TryParse(boardgame.Split(",")[14], out int thmRnk) ? thmRnk : 0,
		WargamesRank: int.TryParse(boardgame.Split(",")[15], out int warRnk) ? warRnk : 0
	);

foreach(BoardGame game in bgQuery){
	Console.WriteLine($"GAME FOUND: {game.Name} - RANK {game.OverallRank}");
}

/*
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
*/

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
		string insertQuery = $"INSERT INTO {tableName} (";
		insertQuery += "bgg_id,";
		insertQuery += "name,";
		insertQuery += "year_published,";
		insertQuery += "overall_rank,";
		insertQuery += "bayes_average,";
		insertQuery += "average,";
		insertQuery += "users_rated,";
		insertQuery += "is_expansion,";
		insertQuery += "abstracts_rank,";
		insertQuery += "cgs_rank,";
		insertQuery += "children_games_rank,";
		insertQuery += "family_games_rank,";
		insertQuery += "party_games_rank,";
		insertQuery += "strategy_games_rank,";
		insertQuery += "thematic_games_rank,";
		insertQuery += "wargames_rank";
		insertQuery += ") ";
		insertQuery += "VALUES ( ";
		insertQuery += $"\"{game.BggId}\",";
		insertQuery += $"\"{gameName}\",";
		insertQuery += $"{game.YearPublished},";
		insertQuery += $"{game.OverallRank},";
		insertQuery += $"{game.BayesAverage},";
		insertQuery += $"{game.Average},";
		insertQuery += $"{game.UsersRated},";
		insertQuery += $"{game.IsExpansion},";
		insertQuery += $"{game.AbstractsRank},";
		insertQuery += $"{game.CgsRank},";
		insertQuery += $"{game.ChildrenGamesRank},";
		insertQuery += $"{game.FamilyGamesRank},";
		insertQuery += $"{game.PartyGamesRank},";
		insertQuery += $"{game.StrategyGamesRank},";
		insertQuery += $"{game.ThematicGamesRank},";
		insertQuery += $"{game.WargamesRank}";
		insertQuery += ");";
		var insertCmd = new MySqlCommand(insertQuery, dbCon.Connection);
		int rowsInserted = insertCmd.ExecuteNonQuery();
		totalRowsInserted += rowsInserted;
		Console.WriteLine($"Inserted {rowsInserted} rows for the game {game.Name}");
	}
	dbCon.Close();
}

Console.WriteLine($"Inserted {totalRowsInserted} Board Games into the Database");


//public record BoardGame (string Name, int YearPublished);
public record BoardGame (
	int BggId, 
	string Name, 
	int YearPublished,
	int OverallRank,
	float BayesAverage,
	float Average,
	int UsersRated,
	bool IsExpansion,
	int AbstractsRank,
	int CgsRank,
	int ChildrenGamesRank,
	int FamilyGamesRank,
	int PartyGamesRank,
	int StrategyGamesRank,
	int ThematicGamesRank,
	int WargamesRank
);

