using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : GenericSingleton<ScoreManager>
{
	public int currentScore;
	private int highScoreLength = 5;
	private const string highScoreKey = "HighScore";
	private List<int> sortingList;

	private void Start()
	{
		sortingList = new List<int>();

		//Create list with the values from the "HighScore"-list (which contains float-values at the specified index) in PlayerPrefs.
		for (int i = 0; i < highScoreLength; i++)
		{
			sortingList.Add(PlayerPrefs.GetInt(highScoreKey + i));
		}
	}

	private void Update()
	{
		//Debug set high score
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			currentScore = UnityEngine.Random.Range(0, 100);
			SaveHighScore();
		}

		//Debug get HighScore
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			GetHighScores();
		}

		//print("currentScore: " + currentScore);
	}

	//Public for test.
	public void SaveHighScore()
	{
		int lowestHighScore = PlayerPrefs.GetInt(highScoreKey + "4");

		if (currentScore < lowestHighScore)
		{
			print("<color=yellow>We have too low score biiatch.</color>");
			return;
		}

		sortingList.Clear();

		//currentScore is greater than last. Overwrite last index.
		PlayerPrefs.SetInt(highScoreKey + (highScoreLength - 1), currentScore);

		//Make list
		for (int i = 0; i < highScoreLength; i++)
		{
			sortingList.Add(PlayerPrefs.GetInt(highScoreKey + i));
		}

		//Sort
		sortingList.Sort();
		//sortingList.Reverse();

		//Set the PlayerPrefs to our Sorted list.
		for (int i = 0; i < sortingList.Count; i++)
		{
			PlayerPrefs.SetInt("HighScore" + i, sortingList[i]);
		}
	}

	private void GetHighScores()
	{
		for (int i = 0; i < highScoreLength; i++)
		{
			print("HighScore " + i + " : " + sortingList[i]);
		}
	}

	public void SetScore(float finishingTime)
	{
		currentScore += (int)finishingTime;
	}

	private void ResetHighScore()
	{
		//Delete ALL PlayerPrefs. May not be desired.
		//PlayerPrefs.DeleteAll();

		//Delete this particular list of PlayerPrefs-values.
		for (int i = 0; i < highScoreLength; i++)
		{
			PlayerPrefs.DeleteKey(highScoreKey + i);
		}
	}

	public void Penalty()
	{
		currentScore += 10;
	}

	public void IncreaseScore()
	{
		currentScore += 5;
	}
}