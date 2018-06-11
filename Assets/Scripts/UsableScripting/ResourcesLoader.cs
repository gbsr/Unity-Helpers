using UnityEngine;

/// <summary>
/// Contains functions for loading an Object and get a sensible error if it failed.
/// </summary>
public class ResourcesLoader
{
	/// <summary>
	/// Returns a GameObject from the Resources-folder.
	/// </summary>
	public static GameObject LoadGameObject(string resourcesPath)
	{
		var obj = Resources.Load(resourcesPath) as GameObject;
		if (obj)
		{
			return obj;
		}
		else
		{
			Debug.LogError("There exists no Asset with that path! " +
				"Check if the Asset has been moved out of the Resources-folder. " +
				"Desired Asset: " + resourcesPath);
			return null;
		}
	}
}