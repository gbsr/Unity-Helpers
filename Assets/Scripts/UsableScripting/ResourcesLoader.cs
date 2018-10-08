using UnityEngine;

/// <summary>
/// Contains functions for loading an Object and get a sensible error if it failed.
/// </summary>
public class ResourcesLoader : MonoBehaviour
{
	public static void SpawnObject<T>(string resourcesPath, Vector3 spawnLocation, Quaternion spawnRotation) where T : Object
	{
		var objectToSpawnPrefab = LoadObject<T>(resourcesPath);

		//Avoid errors of not correctly loaded Assets.
		if (!objectToSpawnPrefab)
			return;

		//Spawn your object
		var objectToSpawn = Instantiate(objectToSpawnPrefab, spawnLocation, spawnRotation);
		Debug.Log("Object spawned: " + objectToSpawn.ToString());
	}

	/// <summary>
	/// Generic function for loading any Type (eg. GameObjects, Sprites, Scripts) from the Resources-folder.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="resourcesPath"></param>
	/// <returns></returns>
	public static T LoadObject<T>(string resourcesPath) where T : UnityEngine.Object
	{
		var obj = (T)Resources.Load(resourcesPath);
		if (obj)
		{
			return obj;
		}
		else
		{
			Debug.LogError("There exists no Asset with that path! \n" +
				"Check if the Asset has been moved out of the Resources-folder. \n" +
				"Desired Asset: " + resourcesPath);
			return null;
		}
	}

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
			Debug.LogError("There exists no Asset with that path! \n" +
				"Check if the Asset has been moved out of the Resources-folder. \n" +
				"Desired Asset: " + resourcesPath);
			return null;
		}
	}
}