using UnityEngine;

/// <summary>
/// A collection of handy math functions, in addition to the ones present in "Mathf"
/// </summary>
public static class MathFunctions
{
	/// <summary>
	/// Returns the nearest integer to the snapSteps-interval.
	/// Original: https://www.reddit.com/r/Unity3D/comments/38bvns/round_to_even_number_or_nearest_5_in_c/
	/// </summary>
	public static float SnappedNumber(float input, float snapSteps = 10f)
	{
		if (snapSteps <= 0f)
			throw new UnityException("factor argument must be above 0");

		float snappedNumber = Mathf.Round(input / snapSteps) * snapSteps;

		return snappedNumber;
	}

	public static Vector3 CalculateSnappedPosition(Vector3 positionToSnap, int snappingSteps)
	{
		Vector3 snappedCoords = positionToSnap;

		snappedCoords.x = Mathf.Round(snappedCoords.x * snappingSteps) / snappingSteps;
		snappedCoords.y = Mathf.Round(snappedCoords.y * snappingSteps) / snappingSteps;
		snappedCoords.z = Mathf.Round(snappedCoords.z * snappingSteps) / snappingSteps;

		return snappedCoords;
	}
}