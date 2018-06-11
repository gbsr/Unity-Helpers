using UnityEngine;

/// <summary>
/// How to write a FlipFlop-switch which works like the FlipFlop-node in Unreal Engine 4.
/// </summary>
public class FlipFlopBoolSwitch : MonoBehaviour
{
	private bool test;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			print("LAST STATE: " + test);

			//Here's where the magic actually happens.
			test = !test;

			print("NEW STATE: " + test);

			if (test)
				print("Do TRUE stuff!");
			else
				print("Do FALSE stuff!");
		}
	}
}