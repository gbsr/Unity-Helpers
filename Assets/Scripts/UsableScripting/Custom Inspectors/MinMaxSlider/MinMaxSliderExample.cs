//Written by Pablo Sorribes Bernhard, 2019 02 16

using UnityEngine;

public class MinMaxSliderExample : MonoBehaviour
{
	public float normalFloat;

	[Range(0,1)]
	public float normalSlider;
	public MinMaxSlider minMaxSlider;

	public float normalFloat2;
	public float normalFloat3;

	[Range(0, 1)]
	public float normalSlider2;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			float randomNumber = UnityEngine.Random.Range(minMaxSlider.minVal, minMaxSlider.maxVal);
			Debug.Log($"Random number within MinVal and MaxVal: {randomNumber}");
		}
	}
}