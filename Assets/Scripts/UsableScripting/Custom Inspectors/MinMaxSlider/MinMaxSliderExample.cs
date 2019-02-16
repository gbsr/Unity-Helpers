using UnityEngine;

public class MinMaxSliderExample : MonoBehaviour
{
	public MinMaxSlider minMaxSlider;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			float randomNumber = UnityEngine.Random.Range(minMaxSlider.min, minMaxSlider.max);
			Debug.Log($"Random number within MinVal and MaxVal: {randomNumber}");
		}
	}
}