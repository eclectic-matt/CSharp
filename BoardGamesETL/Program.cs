/**
 * Board Games Extract-Transform-Load program. 
 */

//OTHER DATA SOURCES: 
// - https://geekgroup.app/users/EclecticMatt/plays
// - https://boardgamegeek.com/xmlapi/search?search=a
// - https://boardgamegeek.com/data_dumps/bg_ranks

/*
MySQL => SHOW COLUMNS FROM `board_games`

Field				Type				Null	Key	Default	Extra

id					int(11)				NO		PRI	NULL	auto_increment
bgg_id				int(11)				YES			NULL	
name				varchar(255)		YES			NULL	
year_published		smallint(6)			YES			NULL
overall_rank		int(10) unsigned	YES			NULL
bayes_average		double unsigned		YES			NULL
average				double unsigned		YES			NULL
users_rated			int(10) unsigned	YES			NULL
is_expansion		tinyint(1)			YES			0
abstracts_rank		int(11)				YES			NULL
cgs_rank			int(11)				YES			NULL
children_games_rank	int(11)				YES			NULL
family_games_rank	int(11)				YES			NULL
party_games_rank	int(11)				YES			NULL
strategy_games_rank	int(11)				YES			NULL
thematic_games_rank	int(11)				YES			NULL
wargames_rank		int(11)				YES			NULL


SELECT 'Overall', name FROM `board_games` WHERE overall_rank = 1
UNION ALL 
SELECT 'Childrens', name FROM board_games WHERE children_games_rank = 1
UNION ALL 
SELECT 'Party', name FROM board_games WHERE party_games_rank = 1
UNION ALL 
SELECT 'Strategy', name FROM board_games WHERE strategy_games_rank = 1
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

//Define a LINQ query which extracts the relevant data into a BoardGame Record (defined at bottom of script)
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

//OUTPUT TO THE CONSOLE (DEBUG ONLY)
/*foreach(BoardGame game in bgQuery){
	Console.WriteLine($"GAME FOUND: {game.Name} - RANK {game.OverallRank}");
}*/

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
		//GENERATE AN INSERT QUERY STRING (SEPARATED ONTO NEW LINES FOR EASE OF READING)
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

//THE DATA DEFINITION FOR A BOARD GAME (AS PER THE BGG EXPORT FROM https://boardgamegeek.com/data_dumps/bg_ranks)
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