using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// Create a button icon which shows a progress bar and resizes when the player inputs the desired button/key (<see cref="activationKey"/>).
/// <para></para>
/// Source: https://www.febucci.com/2019/02/skip-cutscenes-button/
/// </summary>
/// <remarks>
/// Hierarchy setup:
/// - SkipCutSceneButton[RectTransform] - [OPTIONAL] An empty parent with the script attached.
/// 									  Recommended to have, since it allows for resizing the entire button without
/// 									  messing with the direction of the size-adjustment performed on the HolderRect.
/// 	- HolderRect[RectTransform / Image] - Either an empty RectTransform or an Image acting as a background circle.
/// 		- FillImage[Image] - The progress bar.An Image-component(a circle sprite), set to ImageType = Filled.
/// 		- ButtonIconImage[Image] - [OPTIONAL] An icon of the button that this script is going to react to (could be anything really, this is just graphics).
///</remarks>
public class SkipCutsceneButton : MonoBehaviour
{
	[Tooltip("The circle that shows us the overall progress.")]
	public Image fillImage;
	[Tooltip("Holder of the button-graphics, used to change its scale when the 'activationKey' is pressed.")]
	public RectTransform holderRect;

	//Start scale of the holder
	private Vector2 holderRectStartScale;
	[Tooltip("The desired scale we want when we complete the input, i.e. the input percentage is 1.")]
	public Vector2 holderRectTargetScale = new Vector2(1.2f, 1.2f);

	[Tooltip("The key to press to start filling up the button.")]
	public KeyCode activationKey = KeyCode.X;

	[Range(0.01f, 5f)] [Tooltip("Speed that increases the progress if we press the desired 'activationKey'.")]
	public float activeSpeed = 0.5f;

	[Range(0.01f, 5f)] [Tooltip("How fast the progress decreases if we don't press the desired 'activationKey'.")]
	public float cooldownSpeed = 0.6f;

	//Prevents the function to be executed more than once
	private bool functionTriggered = false;

	private void Awake()
	{
		//Assures we don't have null references
		Assert.IsNotNull(fillImage);
		Assert.IsNotNull(holderRect);

		//Cache 
		holderRectStartScale = holderRect.localScale;
		fillImage.fillAmount = 0;
	}

	private void Update()
	{
		//Increases or decreases our progress based on our input (and if we didn't already trigger the function)
		//No need to clamp, since fillAmount is already clamped by Unity -> [0,1] range
		fillImage.fillAmount +=
		((Input.GetKey(activationKey) && !functionTriggered) ?
			   activeSpeed :
				-cooldownSpeed) * Time.deltaTime;

		//Changes the size of the button based on our progress
		holderRect.localScale = Vector2.Lerp(
			holderRectStartScale,
			holderRectTargetScale,
			fillImage.fillAmount * fillImage.fillAmount //you can apply an interpolation there, I applied EaseIn
			);

		//Triggers the function if we didn't already, and the progress/percentage is 1
		if (!functionTriggered && fillImage.fillAmount == 1)
		{
			functionTriggered = true;

			Debug.Log("Button pressed completely");

			//Do something here
			//Skip cutscene or similar
			//[...]
		}
	}
}