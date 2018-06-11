using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjectFromResources : MonoBehaviour {

	private const string pathToObject = "Particles/DeathParticles";

	private void SpawnDeathParticles()
	{
		//GameObject deathParticles = Resources.Load(pathToObject) as GameObject;

		GameObject deathParticles = ResourcesLoader.LoadGameObject(pathToObject);
		Instantiate(deathParticles, transform.position, Quaternion.LookRotation(Vector3.up));
	}
}
