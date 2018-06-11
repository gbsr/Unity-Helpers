using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains functions for loading scenes asynchroniusly, thus allowing the caller to wait for a scene to have loaded before doing more stuff.
/// </summary>
public class SceneLoader
{
	/// <summary>
	/// Loads the specified scene asynchroniously and sets it as ActiveScene when it is done loading.
	/// <para>Use "yield return" when calling if you want to wait for when this function is done.</para>
	/// <para>Load Mode: ADDITIVE</para>
	/// </summary>
	public static IEnumerator LoadSceneAndWait_Additive(string sceneName)
	{
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
	}

	/// <summary>
	/// Loads the specified scene asynchroniously and sets it as ActiveScene when it is done loading.
	/// <para>Use "yield return" when calling if you want to wait for when this function is done.</para>
	/// <para>Load Mode: SINGLE</para>
	/// </summary>
	public static IEnumerator LoadSceneAndWait_Single(string sceneName)
	{
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
	}
}