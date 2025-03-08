// See https://aka.ms/new-console-template for more information

//REQUIRED PACKAGES
using OfficeOpenXml;
using System.Xml.Linq;
using System.Text;
using System.Xml;

//MY PACKAGES
using Challenges;
using Numbers;

//=====================
//https://learn.microsoft.com/en-gb/training/modules/csharp-write-first/
//======================

//https://learn.microsoft.com/en-gb/training/modules/csharp-write-first/2-exercise-hello-world
//Console.WriteLine appends a new line character
Console.WriteLine("Hello, World!");
Console.WriteLine("A new message!");
//Console.Write prints to the current line
Console.Write("Congratulations!");
Console.Write(" ");
Console.Write("You wrote your first lines of code!");
//Challenge
Console.WriteLine("This is the first line.");
Console.WriteLine("This is the second line.");

//======================
//VARIABLE DECLARATIONS
//======================

//int declarations are just the number
int myInteger = 5;
//double declarations end with D
double myDouble = 3.14D;
//single characters are wrapped in '' SINGLE quote marks
char myChar = 'A';
//strings are wrapped in "" DOUBLE quote marks
string myString = "Matt";
//booleans are booleans
bool myBool = false;

//CONSTANT DECLARATIONS
const int myConstantInt = 4;
//myConstantInt = 3; 	//error

//MULTIPLE DECLARATIONS
//variables of the same type can be declared on the same line
int x = 4, y = 5, z = 6;
Console.WriteLine(x + y + z);
//variables of the same value can be assigned on the same line
x = y = z = 9;
Console.WriteLine(x + y + z);

/*
The general rules for naming variables are:
	Names can contain letters, digits and the underscore character (_)
	Names must begin with a letter or underscore
	Names should start with a lowercase letter, and cannot contain whitespace
	Names are case-sensitive ("myVar" and "myvar" are different variables)
	Reserved words (like C# keywords, such as int or double) cannot be used as names
*/

//======================
//DISPLAYING VARIABLES
//======================

//the two lines below achieve the same output
Console.WriteLine("Hello " + myString);
//NOTE: preface interpolated strings with a $ dollar sign
Console.WriteLine($"Hello {myString}");
//ADD VARIABLE NUMBERS TOGETHER
Console.WriteLine(myInteger + myDouble);

//======================
//EQUALITY AND EVALATION
//======================

//test equality using ==
Console.WriteLine("'a' == 'a': " + ("a" == "a"));	//true
Console.WriteLine("'a' == 'A': " + ("a" == "A"));	//false
Console.WriteLine("1 == 2: " + (1 == 2));			//false
Console.WriteLine("myString == 'D': " + (myString == "D"));	//false

//test inequality using !=
Console.WriteLine("'a' != 'a': " + ("a" != "a"));	//false
Console.WriteLine("'a' != 'A': " + ("a" != "A"));	//true
Console.WriteLine("1 != 2: " + (1 != 2));			//true

//greater than and less than operators
Console.WriteLine("1 < 2: " + (1 < 2));		//true
Console.WriteLine("1 < 2: " + (1 <= 2));	//true
Console.WriteLine("1 > 2: " + (1 > 2));		//false
Console.WriteLine("1 >= 2: " + (1 >= 2));	//false

//using string methods to "massage" the data into equivalence
string value1 = " a";
string value2 = "A ";
Console.WriteLine("' a' == 'A ' when trimmed and lowercased: " + (value1.Trim().ToLower() == value2.Trim().ToLower()));	//true

if(myString == "Matt"){
	Console.WriteLine($"{myString} is my best friend!");
}

//using sting methods that return a boolean
string pangram = "The quick brown fox jumps over the lazy dog.";
Console.WriteLine("Pangram contains 'fox': " + pangram.Contains("fox"));	//true
Console.WriteLine("Pangram contains 'cow': " + pangram.Contains("cow"));	//false
//using logical negation
Console.WriteLine("NOT Pangram contains 'cow': " + !pangram.Contains("cow"));	//false

//======================
//CONDITIONAL LOGIC
//======================
int saleAmount = 1001;
//use the ternary conditional in the form <condition> ? <trueResult> : <falseResult>;
int discount = saleAmount > 1000 ? 100 : 50;
Console.WriteLine($"Discount: {discount}");
Console.WriteLine($"Discount: {(saleAmount > 1000 ? 100 : 50)}");

//CHALLENGE - COIN FLIP
Random coin = new Random();
int flip = coin.Next(0,2);
Console.WriteLine($"Random Value: {flip}");
Console.WriteLine((flip == 0) ? "heads" : "tails");

//CHALLENGE - ADMIN PERMISSIONS
string permission = "Admin|Manager";
int level = 55;
//determine admin or manager
if(permission.Contains("Admin")){
	Console.WriteLine((level > 55) ? "Welcome, Super Admin user." : "Welcome, Admin user.");
}else if(permission.Contains("Manager")){
	Console.WriteLine((level > 20) ? "Contact an Admin for access" : "You do not have sufficient privileges.");
}else{
	Console.WriteLine("You do not have sufficient privileges.");
}

//======================
//VALUE TYPES
//INTEGRAL/FLOATING
//======================

//all of the types below are value types - they store the value directly
Console.WriteLine("Signed integral types:");

Console.WriteLine($"sbyte  : {sbyte.MinValue} to {sbyte.MaxValue}");
Console.WriteLine($"short  : {short.MinValue} to {short.MaxValue}");
Console.WriteLine($"int    : {int.MinValue} to {int.MaxValue}");
Console.WriteLine($"long   : {long.MinValue} to {long.MaxValue}");

Console.WriteLine("");
Console.WriteLine("Unsigned integral types:");

Console.WriteLine($"byte   : {byte.MinValue} to {byte.MaxValue}");
Console.WriteLine($"ushort : {ushort.MinValue} to {ushort.MaxValue}");
Console.WriteLine($"uint   : {uint.MinValue} to {uint.MaxValue}");
Console.WriteLine($"ulong  : {ulong.MinValue} to {ulong.MaxValue}");

Console.WriteLine("");
Console.WriteLine("Floating point types:");
Console.WriteLine($"float  : {float.MinValue} to {float.MaxValue} (with ~6-9 digits of precision)");
Console.WriteLine($"double : {double.MinValue} to {double.MaxValue} (with ~15-17 digits of precision)");
Console.WriteLine($"decimal: {decimal.MinValue} to {decimal.MaxValue} (with 28-29 digits of precision)");

//======================
//REFERENCE TYPES
//======================

//the below are all reference types 
//(i.e. the variable actually holds a reference to its memory address on the heap where the data is stored)

//define an array of integers
int[] data;
//use the "new" keyword to set a value
data = new int[3];
//or do the above in one line
int[] allInOne = new int[3];


//showing the differences between value and reference types below

//VALUE TYPES
int val_A = 2;
int val_B = val_A;
//this line only affect val_B
val_B = 5;

Console.WriteLine("--Value Types--");
Console.WriteLine($"val_A: {val_A}");
Console.WriteLine($"val_B: {val_B}");

//REFERENCE TYPES
int[] ref_A= new int[1];
ref_A[0] = 2;
int[] ref_B = ref_A;
//this line DOES affect ref_A as they are the same data on the heap
ref_B[0] = 5;

Console.WriteLine("--Reference Types--");
Console.WriteLine($"ref_A[0]: {ref_A[0]}");
Console.WriteLine($"ref_B[0]: {ref_B[0]}");

/*
//STICK WITH BASIC TYPES WHERE POSSIBLE 
	int for most whole numbers
	decimal for numbers representing money
	bool for true or false values
	string for alphanumeric value
//USE SPECIALTY COMPLEX TYPES WHERE APPROPRIATE
	byte: working with encoded data that comes from other computer systems or using different character sets.
	double: working with geometric or scientific purposes. double is used frequently when building games involving motion.
	System.DateTime for a specific date and time value.
	System.TimeSpan for a span of years / months / days / hours / minutes / seconds / milliseconds.
*/

//======================
//METHODS
//======================
//declare a method (prefaced with the return type)
void SayHello()
{
	Console.WriteLine("Hello!");
}
//call the defined method
SayHello();

//NOTE: you may call a method before its definition in the code - hoisted?
int[] a = {1,2,3,4,5};

Console.WriteLine("Contents of Array:");
PrintArray();

void PrintArray()
{
	foreach (int x in a)
	{
		Console.Write($"{x} ");
	}
	Console.WriteLine();
}

//BEST PRACTICES FOR NAMING - PascalCase method names with descriptive parameter names
//void ShowData(string a, int b, int c);				//Poor, unclear what is done and what the params mean
//void DisplayDate(string month, int day, int year);	//Good, better for understanding the method and params

//define a method
void DisplayRandomNumbers()
{
	Random random = new Random();
	for(int i = 0; i < 5; i++)
	{
		//Generate a random number between 1 and 100
		Console.Write($"{random.Next(1, 100)} ");
	}
}

Console.WriteLine("Generating some random numbers: ");
DisplayRandomNumbers();
Console.WriteLine(" ");

//STORED CHALLENGES IN THEIR OWN NAMESPACE

//Generate the output of the Medicine Times Reformat
//Challenges.ChallengesOutput.MedicineTimes();

//Generate the output of the IP Address Validation
Console.WriteLine(" ");
Console.WriteLine("IP ADDRESS VALIDATION");
Challenges.ChallengesOutput.IPAddressValidation();


//=========================
//PERSONAL CHALLENGE
//GENERATE COMPLEX NUMBERS
//=========================

Console.WriteLine(" ");
Console.WriteLine("COMPLEX NUMBERS");
//define a complex number (1 + 2i)
Complex c1 = new(1, 2);
Console.WriteLine("c1: " + c1.Print());
//define another complex number (2 + 4i)
Complex c2 = new(2, 4);
Console.WriteLine("c2: " + c2.Print());
//define the addition of the two above numbers
Complex c3 = c1.Add(c2);
Console.WriteLine("c1 + c2: " + c3.Print());
//define the muliplication result of the two above numbers
Complex c4 = c1.Multiply(c2);
Console.WriteLine("c1 * c2: " + c4.Print());
//define the subtraction of the two above numbers
Complex c5 = c2.Subtract(c1);
Console.WriteLine("c2 - c1: " + c5.Print());
//define the division result of the two above numbers
Complex c6 = c2.Divide(c1);
Console.WriteLine("c2 / c1: " + c6.Print());

//================
// ARRAYS 
//================
string[] fraudulentOrderIDs = new string[3];

fraudulentOrderIDs[0] = "A123";
fraudulentOrderIDs[1] = "B456";
fraudulentOrderIDs[2] = "C789";
try {
	//the below line will cause an exception (out of bounds)
	fraudulentOrderIDs[3] = "D000";
}catch{
	Console.WriteLine("Tried to add a 4th element to a 3-sized array!");
}

//output the array values by index
Console.WriteLine($"First: {fraudulentOrderIDs[0]}");
Console.WriteLine($"Second: {fraudulentOrderIDs[1]}");
Console.WriteLine($"Third: {fraudulentOrderIDs[2]}");

//reassign the value in the first array element
fraudulentOrderIDs[0] = "F000";
Console.WriteLine($"Reassign First: {fraudulentOrderIDs[0]}");
Console.WriteLine($"There are {fraudulentOrderIDs.Length} fraudulent orders to process.");

//quick array initialize/assign
string[] validOrderIDs = [ "A123", "B456", "C789" ];
Console.WriteLine($"There are {validOrderIDs.Length} valid orders to process.");

//iterate through order IDs
Console.WriteLine("Iterate order ids:");
foreach(string orderId in validOrderIDs)
{
	Console.WriteLine($"Current Order: {orderId}");
}

//init an array of ints
int[] inventory = { 200, 450, 700, 175, 250 };
//iterate this array, generating a sum as we go
int sum = 0, bin = 0;
foreach (int item in inventory)
{
	bin++;
	sum += item;
	Console.WriteLine($"Bin {bin} = {item} items (Running total: {sum})");
}
Console.WriteLine($"Sum of the inventory: {sum}");

//CHALLENGE - DETECT FRAUDULENT ORDERS
Console.WriteLine(" ");
Console.WriteLine("Detect fraudulent orders!");
string[] ordersToCheck = [ "B123", "C234", "A345", "C15", "B177", "G3003", "C235", "B179" ];
foreach(string order in ordersToCheck)
{
	if(order.StartsWith("B")){
		Console.WriteLine($"Fraudulent Order Detected: {order}");
	}
}

//CHALLENGE - STRING ARRAYS 
Console.WriteLine(" ");
Console.WriteLine("Generate fortune teller info");
void GenerateFortuneTeller(int luck)
{
	string[] text = {"You have much to", "Today is a day to", "Whatever work you do", "This is an ideal time to"};
	string[] good = {"look forward to.", "try new things!", "is likely to succeed.", "accomplish your dreams!"};
	string[] bad = {"fear.", "avoid major decisions.", "may have unexpected outcomes.", "re-evaluate your life."};
	string[] neutral = {"appreciate.", "enjoy time with friends.", "should align with your values.", "get in tune with nature."};
	string[] fortune = (luck > 75 ? good : (luck < 25 ? bad : neutral));
	
	Console.WriteLine(" ");
	Console.WriteLine("A fortune teller whispers the following words:");
	for(int i = 0; i < 4; i++)
	{
		Console.Write($"{text[i]} {fortune[i]} ");
	}
}
Random random = new Random();
for(int index = 0; index < 4; index++)
{
	int currentLuck = random.Next(100);
	GenerateFortuneTeller(currentLuck);
}

//=====================
// CHALLENGE
// STUDENT GRADING APP
//=====================
//https://learn.microsoft.com/en-gb/training/modules/guided-project-calculate-print-student-grades/5-exercise-format-strings
Console.WriteLine(" ");
Console.WriteLine("Student Score Grading Application:");
Console.WriteLine(" ");

// initialize variables - graded assignments 
int sophia1 = 93;
int sophia2 = 87;
int sophia3 = 98;
int sophia4 = 95;
int sophia5 = 100;
int[] sophiaScores = [ sophia1, sophia2, sophia3, sophia4, sophia5 ];
Console.WriteLine($"Sophia Score Sum: {GradingApplication.GetScoresSum(sophiaScores)}");
Console.WriteLine($"Sophia Score Average: {GradingApplication.GetScoresAverage(sophiaScores)}");
Console.WriteLine($"Sophia Score Grade: {GradingApplication.GetScoresGrade(sophiaScores)}");
Console.WriteLine(" ");

int nicolas1 = 80;
int nicolas2 = 83;
int nicolas3 = 82;
int nicolas4 = 88;
int nicolas5 = 85;
int[] nicolasScores = [ nicolas1, nicolas2, nicolas3, nicolas4, nicolas5 ];
Console.WriteLine($"Nicolas Score Sum: {GradingApplication.GetScoresSum(nicolasScores)}");
Console.WriteLine($"Nicolas Score Average: {GradingApplication.GetScoresAverage(nicolasScores)}");
Console.WriteLine($"Nicolas Score Grade: {GradingApplication.GetScoresGrade(nicolasScores)}");
Console.WriteLine(" ");

int zahirah1 = 84;
int zahirah2 = 96;
int zahirah3 = 73;
int zahirah4 = 85;
int zahirah5 = 79;
int[] zahirahScores = [ zahirah1, zahirah2, zahirah3, zahirah4, zahirah5 ];
Console.WriteLine($"Zahirah Score Sum: {GradingApplication.GetScoresSum(zahirahScores)}");
Console.WriteLine($"Zahirah Score Average: {GradingApplication.GetScoresAverage(zahirahScores)}");
Console.WriteLine($"Zahirah Score Grade: {GradingApplication.GetScoresGrade(zahirahScores)}");
Console.WriteLine(" ");

int jeong1 = 90;
int jeong2 = 92;
int jeong3 = 98;
int jeong4 = 100;
int jeong5 = 97;
int[] jeongScores = [ jeong1, jeong2, jeong3, jeong4, jeong5 ];
Console.WriteLine($"Jeong Score Sum: {GradingApplication.GetScoresSum(jeongScores)}");
Console.WriteLine($"Jeong Score Average: {GradingApplication.GetScoresAverage(jeongScores)}");
Console.WriteLine($"Jeong Score Grade: {GradingApplication.GetScoresGrade(jeongScores)}");
Console.WriteLine(" ");

Console.WriteLine("GRADES SUMMARY");
Console.WriteLine("Name\tScore\tGrade");
Console.WriteLine($"Sophia\t{GradingApplication.GetScoresAverage(sophiaScores)}\t{GradingApplication.GetScoresGrade(sophiaScores)}");
Console.WriteLine($"Nicolas\t{GradingApplication.GetScoresAverage(nicolasScores)}\t{GradingApplication.GetScoresGrade(nicolasScores)}");
Console.WriteLine($"Zahirah\t{GradingApplication.GetScoresAverage(zahirahScores)}\t{GradingApplication.GetScoresGrade(zahirahScores)}");
Console.WriteLine($"Jeong\t{GradingApplication.GetScoresAverage(jeongScores)}\t{GradingApplication.GetScoresGrade(jeongScores)}");


//============================
// LEARNING C# ITERABLES
//============================

Console.WriteLine("ITERATOR TO GENERATE SOME NUMBERS:");
foreach(int number in IterableTests.SomeNumbers()){
	Console.Write(number.ToString() + " ");
}

Console.WriteLine(" ");
Console.WriteLine("GENERATE EVEN NUMBERS:");
foreach(int number in IterableTests.EvenSequence(5,16))
{
	Console.Write(number.ToString() + " ");
}

Console.WriteLine(" ");
Console.WriteLine("GET DAYS OF WEEK:");
foreach(string day in IterableTests.GetDaysOfWeek())
{
	Console.Write(day + " ");
}

//============================
// LEARNING LINQ
// Language INtegrated Query
//============================

//LINQ SOURCE: https://learn.microsoft.com/en-gb/dotnet/csharp/linq/
Console.WriteLine(" ");
Console.WriteLine("GET SCORES ABOVE 80:");

// Specify the data source.
int[] scores = [97, 92, 81, 60];

// Define the query expression.
IEnumerable<int> scoreQuery =
	from score in scores
	where score > 80
	select score;

// Execute the query.
foreach (var i in scoreQuery)
{
	Console.Write(i + " ");
}
// Output: 97 92 81

// Create a data source from an XML document.
// using System.Xml.Linq;
//Define the path (absolute needed, relative links to the /bin director)
var contactFilePath = "S:/Development/Github/CSharp/LearningCSharp/data/myContactList.xml";
//Use the XElement to load the XML file
XElement contactList = XElement.Load(contactFilePath);
//Define an inumerable and define the Linq query
//This searches the XML for a descendant "contact" element
//And from these descendants, extract the "phone" element
IEnumerable<string> contactQuery =
	from item in contactList.Descendants("contact")
	select (string) item.Element("phone");

//Execute the query
foreach (var i in contactQuery)
{
	Console.Write(i + " ");
}

//=========================
// FURTHER TESTING 
// LOADING EXCEL SHEETS
//=========================
//HAD TO INSTALL EPPlus VIA NUGET:
//dotnet add package EPPlus --version 7.6.1
//ALSO NEEDS THE FOLLOWING PACKAGES:
//using OfficeOpenXml;
//using System.Xml.Linq;
//using System.Text;

/*
//COMMENTING THIS SECTION OUT AS IT HAS TO RUN THROUGH 23130 ROWS OF DATA - TAKES A FEW SECONDS!

Console.WriteLine(" ");
Console.WriteLine(" ");
Console.WriteLine("OUTPUT MOT STATIONS IN SHREWSBURY FROM EXCEL SHEET:");

//MUST SET LICENSE TO NON-COMMERCIAL TO WORK
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//EXCEL TESTING
using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"C:\Users\Matt\Documents\Personal Docs\active-mot-stations.xlsx")))
{
	var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
	var totalRows = myWorksheet.Dimension.End.Row;
	var totalColumns = myWorksheet.Dimension.End.Column;

	var sb = new StringBuilder(); //this is your data
	for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
	{
		//GET ALL THE DATA (EVERY COLUMN FROM 1 -> totalColumns)
		//var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
		
		//SKIP IF NOT LOCATED IN Shrewsbury?
		if(myWorksheet.Cells[rowNum,6].Value.ToString().ToLower() != "shrewsbury") continue;

		//GET THE DATA FROM COLUMN 2 ONLY (TradingName IN THIS EXAMPLE)
		var row = myWorksheet.Cells[rowNum, 2, rowNum, 2].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
		sb.AppendLine(string.Join(",", row));
	}
	Console.WriteLine(sb.ToString());
}
*/


//=======================
// NEW EXCEL CLASS (ETL)
//=======================
Console.WriteLine(" ");
Console.WriteLine(" ");
Console.WriteLine("LOAD AN EXCEL FILE AND READ DATA");

//NEW CLASS TESTS - EXCEL HANDLER
string excelFilePath = @"C:\Users\Matt\Documents\Personal Docs\active-mot-stations.xlsx";
//Define a new instance of the Excel class
var xl = new Excel();
//Load the file in the path specified above
xl.LoadFile(excelFilePath);

//Load a sheet by sheetIndex
xl.LoadSheetByIndex(0);

//OR Load a sheet by sheet name
xl.LoadSheetByName("active-mot-stations");

ValidationResponse rowResponse = xl.GetTotalRows();
if(rowResponse.Successful){
	Console.WriteLine("File row count: " + rowResponse.Information);
}
ValidationResponse colResponse = xl.GetTotalColumns();
if(colResponse.Successful){
	Console.WriteLine("File column count: " + colResponse.Information);
}
/*
//THIS IS SLOW - LOADS ALL 23130 ROWS
ValidationResponse colData = xl.GetColumnData(2);
if(colData.Successful){
	Console.WriteLine("Data in column 2: " + colData.Information);
}
*/

//===============
//CSV FILE READ 
//===============
Console.WriteLine(" ");
Console.WriteLine(" ");
Console.WriteLine("LOAD A CSV FILE AND READ DATA");

string csvFilePath = @"C:\Users\Matt\Documents\Corsair_Link_20231030_19_19_43.csv";
var csv = new CSV();
//LOADING THE WHOLE FILE INTO A STRING (MEMORY ISSUES?)
//string fileContents = csv.ReadFile(csvFilePath);
//Console.WriteLine(fileContents);
//LOADING THE FILE INTO AN ARRAY
Console.WriteLine("Output ALL lines in the CSV:");
string[] fileContents = csv.ReadFile(csvFilePath);
foreach(string line in fileContents){
	Console.WriteLine(line);
}

Console.WriteLine(" ");
Console.WriteLine(" ");
Console.WriteLine("Output CSV lines which were written at 7:19:");
foreach(string line in fileContents){
	//ONLY OUTPUT LINES WRITTEN AT 7:19
	if(line.Contains("7:19")){
		Console.WriteLine(line);
	}
}



//===============
// GOOGLE SHEETS
//===============
Console.WriteLine(" ");
Console.WriteLine(" ");
Console.WriteLine("Output the contents of a Google Sheet (as CSV):");
//Define a google sheet url to load (note: using the /export?format=csv option)
string googleSheetPath = "https://docs.google.com/spreadsheets/d/114vBgHnryVDqZU7tOTBXFB-qh5fNQ4dsAPProBE3xnE/export?format=csv";
//Initialize a HTTP Client
using (HttpClient client = new HttpClient())
{
	//Setup an async method which loads the entire url contents into a string
    string s = await client.GetStringAsync(googleSheetPath);
	//Output the string to the console
	Console.WriteLine(s);
}