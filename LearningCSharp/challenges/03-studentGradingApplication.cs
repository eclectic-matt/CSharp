namespace Challenges;

public static class GradingApplication 
{
	public static int GetScoresSum(int[] scores)
	{
		int sum = 0;
		foreach(int score in scores)
		{
			sum += score;
		}
		return sum;
	}

	public static decimal GetScoresAverage(int[] scores)
	{
		int sum = GetScoresSum(scores);
		int scoreCount = scores.Length;
		if(scoreCount == 0){
			return 0;
		}else{
			return (decimal) sum / scoreCount;
		}
	}

	public static string GetScoresGrade(int[] scores)
	{
		decimal average = GetScoresAverage(scores);
		if(average >= 97){
			return "A+";
		}else if(average >= 93){
			return "A";
		}else if(average >= 90){
			return "A-";
		}else if(average >= 87){
			return "B+";
		}else if(average >= 83){
			return "B";
		}
		//DEFAULT - RETURN C
		return "C";
	}
}