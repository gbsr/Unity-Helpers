using UnityEngine;

/// <summary>
/// How to create objects through code.
/// </summary>
public class CreateObjectInCode : MonoBehaviour
{
	private void Start()
	{
		//Create a hitbox
		var hitbox = new GameObject();			//Instantiates a gameObject
		hitbox.AddComponent<BoxCollider2D>();
		hitbox.tag = "Hitbox";
	}
}