using System.Collections;

public static class IterableTests 
{
	public static System.Collections.IEnumerable SomeNumbers() 
	{
		yield return 4;
		yield return 3;
		yield return 5;
	}

	public static System.Collections.Generic.IEnumerable<int> EvenSequence(int firstNumber, int lastNumber)
	{
		for(int number = firstNumber; number <= lastNumber; number++)
		{
			if(number % 2 == 0){
				yield return number;
			}
		}
	}

	public static System.Collections.Generic.IEnumerable<string> GetDaysOfWeek()
	{
		string[] days = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
		for(int index = 0; index < days.Length; index++)
		{
			yield return days[index];
		}
	}
}