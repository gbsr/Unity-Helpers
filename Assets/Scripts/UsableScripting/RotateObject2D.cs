using UnityEngine;

/// <summary>
/// Simple Component to rotate a 2D object (such as a sprite) at a constant rate around the scene's Forward-axis.
/// This will affect the object's Z-axis but leave the X- & Y-axises unaffected. 
/// Thus, you could manipulate the X- & Y-rotation of the object to change which way the rotation is actually "working".
/// </summary>
public class RotateObject2D : MonoBehaviour
{
	[Range(0f, 1000f)]
	public float rotationSpeed = 500;

	[Tooltip("FALSE = Counterclockwise, TRUE = Clockwise")]
	public bool clockwiseRotation = false;

	private void Update()
	{
		float direction = 1f;
		if (clockwiseRotation)
		{
			direction *= -1f;
		}

		Vector3 newRotation = transform.eulerAngles + Vector3.forward * rotationSpeed * Time.deltaTime * direction;
		transform.eulerAngles = newRotation;
	}
}