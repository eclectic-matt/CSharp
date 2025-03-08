// See https://aka.ms/new-console-template for more information

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