using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : GenericSingleton<ScoreManager>
{
	public static bool instantiated;
	public float currentScore;
	private int highScoreLength = _GameVars.Saving.highScoreLength;

	List<int> sortingList;

	private void Start()
	{
		Player.PlayerDead += Penalty;
		sortingList = new List<int>();

		//Create list
		for (int i = 0; i < highScoreLength; i++)
		{
			sortingList.Add((int)PlayerPrefs.GetFloat(_GameVars.Saving.highScore + i));
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
		int lowestHighScore = PlayerPrefs.GetInt(_GameVars.Saving.highScore + "4");

		if (currentScore < lowestHighScore)
		{
			print("<color=yellow>We have too low score biiatch.</color>");
			return;
		}

		sortingList.Clear();

		//currentScore is greater than last. Overwrite last index.
		PlayerPrefs.SetFloat(_GameVars.Saving.highScore + (highScoreLength - 1), currentScore);

		//Make list
		for (int i = 0; i < highScoreLength; i++)
		{
			sortingList.Add((int)PlayerPrefs.GetFloat(_GameVars.Saving.highScore + i));
		}

		//Sort
		sortingList.Sort();
		//sortingList.Reverse();

		//Set the PlayerPrefs to our Sorted list.
		for (int i = 0; i < sortingList.Count; i++)
		{
			PlayerPrefs.SetFloat(_GameVars.Saving.highScore + i, sortingList[i]);
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
		currentScore += finishingTime;
	}

	private void ResetHighScore()
	{
		PlayerPrefs.DeleteAll();
	}

	public void Penalty()
	{
		currentScore += 10; 
	}

	//Public for tests and assembly-shit
	private void OnDestroy()
	{
		Player.PlayerDead -= Penalty;
	}

	public void IncreaseScore()
	{
		currentScore += 5; 
	}
}