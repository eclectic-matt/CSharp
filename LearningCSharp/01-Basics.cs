// See https://aka.ms/new-console-template for more information

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

