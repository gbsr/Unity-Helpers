using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : GenericSingleton<LevelManager>
{
	public static System.Action LevelLoaded;

	protected void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		LevelLoaded();
	}

	private void Start()
	{
		if (LevelLoaded == null)
			Debug.LogError("<color=red>LevelLoaded-action doesn't have any subscribers!</color> ");
		else
			LevelLoaded();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			ReloadLevel();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			GotoLevel(PreviousLevel());
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			GotoLevel(NextLevel());
		}
	}

	public void FinishLevel()
	{
		//Show score screen
		//Load next level
	}
	

	private void ReloadLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Debug.Log("Reloaded scene");
	}

	//On button press, run this
	public void GotoLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
		//TODO: check if it's the last level in build index
	}

	public void GotoLevel(int levelIndex)
	{
		SceneManager.LoadScene(levelIndex);
		//TODO: check if it's the last level in build index
	}

	private int NextLevel()
	{
		return SceneManager.GetActiveScene().buildIndex + 1;
	}

	private int PreviousLevel()
	{
		return SceneManager.GetActiveScene().buildIndex - 1;
	}

	protected void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}