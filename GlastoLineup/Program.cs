using System.Threading.Tasks.Dataflow;
using HtmlAgilityPack;
using System.Globalization;

var web = new HtmlWeb();

//DEFINE THE SITE URL TO SCRAPE
string siteUrl = "https://glastonburyfestivals.co.uk/line-up/line-up-2024/";
//LOAD THIS WEB DOCUMENT
var document = web.Load(siteUrl);
//INITIALIZE THE LISTINGS (NOT CURRENTLY USED - PLANNED FOR FUTURE)
var listings = new List<Listing>();

//RESET LINEUP CSV FILE BETWEEN RUNS
File.WriteAllText(@"S:\Development\Github\CSharp\GlastoLineup\lineup.csv", "Stage Name,Day,Artist(s),Start Time,End Time" + Environment.NewLine);

//GET TEXT INFO TO TITLE CASE ARTIST/STAGE NAMES
TextInfo textInfo = new CultureInfo("en-UK", false).TextInfo;

//GET ALL STAGE DIVS
string stageSelector = ".stage-container";
var stageDivs = document.DocumentNode.QuerySelectorAll("div" + stageSelector);

//VARIABLES WHICH WE WILL STORE AND USE AS WE PROGRESS
string stageName = String.Empty;
string dayName = String.Empty;
string artistName = String.Empty;

//ITERATE STAGE DIVS
foreach(var element in stageDivs)
{
	//EXTRACT THE STAGE NAME AND FORMAT
	stageName = element.Id;
	stageName = stageName.Replace("-", " ");
	stageName = textInfo.ToTitleCase(stageName);
	Console.WriteLine("PROCESSING STAGE: " + stageName);

	//WILL NEED TO PROCESS THESE SEQUENTIALLY TO MATCH TO THE DATE
	foreach(var subElement in element.GetChildElements())
	{
		//THE DAY NAME IS STORED AT THE THIS LEVEL WITHIN A h4 ELEMENT
		if(subElement.Name == "h4"){
			dayName = textInfo.ToTitleCase(subElement.InnerHtml);
			//Console.WriteLine("DAY FOUND: " + dayName);
		}

		//THE ARTIST LISTINGS FOR THE DAY ARE STORED IN A table ELEMENT
		if(subElement.Name == "table"){

			//THE INDIVIDUAL LISTINGS ARE STORED IN td ELEMENTS
			var stageListings = subElement.QuerySelectorAll("tr");

			//FOREACH ARTIST (AS A SINGLE tr)
			foreach(var tr in stageListings)
			{
				//THE ARTIST NAME IS IN THE FIRST TD
				var artistTd = tr.QuerySelector("td:first-child");
				//CHECK IF THIS IS AN EMPTY LISTING
				if(artistTd.OuterHtml == "<td></td>"){
					//SKIP
					continue;
				}
				if(artistTd.FirstChild.Name != "#text"){
					//THIS LISTING HAS AN ANCHOR ARTIST LINK
					artistName = artistTd.GetChildElements().First().InnerHtml;
				}else{
					artistName = artistTd.InnerHtml;
				}

				//THE START/FINISH TIMES ARE THE SECOND TD
				var timingTd = tr.QuerySelector("td:last-child");
				string timingString = timingTd.InnerHtml;
				string[] timings = timingString.Split("&nbsp;-&nbsp;");
				string startTime = String.Empty;
				string endTime = String.Empty;

				if(timings.Length > 1){
					startTime = timings[0];
					endTime = timings[1];
				}else{
					if(timings[0] == ""){
						startTime = "ALL DAY";
					}else{
						startTime = timings[0];
					}
					endTime = "ALL DAY";
				}
				//OUTPUT THE MATCHED ARTIST NAME
				//Console.WriteLine("ARTIST FOUND: " + artistName + " playing on " + dayName + " from " + startTime + " to " + endTime);
				//WRITE TO CSV FILE
				File.AppendAllText(@"S:\Development\Github\CSharp\GlastoLineup\lineup.csv", $"\"{stageName}\",{dayName},\"{artistName}\",{startTime},{endTime}" + Environment.NewLine);
			}
		}
	}
}



//WILL USE IN FUTURE VERSION OF THIS SCRIPT
public record Listing 
{ 
	public string? Name { get; set; } 
	public string? StageGroup { get; set; } 
	public string? Stage { get; set; } 
	public string? Day { get; set; } 
	public string? Start { get; set; } 
	public string? Finish { get; set; } 
}
