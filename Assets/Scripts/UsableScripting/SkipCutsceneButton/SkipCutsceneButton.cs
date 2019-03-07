using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// Image with fill up and size increase
/// Source: https://www.febucci.com/2019/02/skip-cutscenes-button/
/// <para></para>
/// Hierarchy setup:
/// - HolderRect - Either an empty RectTransform or an image acting as a background circle.
///		- FillImage - The progress bar. An Image-component (a circle sprite), set to ImageType = Filled.
///		- ButtonIconImage - [OPTIONAL] An icon of the button that this script is going to react to.
/// </summary>
public class SkipCutsceneButton : MonoBehaviour
{
	[Tooltip("Holder of the entire button, used to change its scale")]
	public RectTransform holderRect;
	[Tooltip("The circle that shows us the overall progress")]
	public Image fillImage;

	//Start scale of the holder
	private Vector2 startHolderScale;
	[Tooltip("The desired scale we want when we complete the input, i.e. the input percentage is 1")]
	public Vector2 targetHoldercale = new Vector2(1.2f, 1.2f);

	[Tooltip("The key to start filling up the button.")]
	public KeyCode activationKey = KeyCode.X;

	[Tooltip("Speed that increases the progress if we press the desired key")]
	public float activeSpeed = 0.5f;

	[Tooltip("How fast the progress decreases if we don't press the desired key")]
	public float cooldownSpeed = 0.6f;

	//Prevents the function to be executed more than once
	private bool functionTriggered = false;

	private void Awake()
	{
		//Assures we don't have null references
		Assert.IsNotNull(fillImage);
		Assert.IsNotNull(holderRect);

		startHolderScale = holderRect.localScale;
		fillImage.fillAmount = 0;
		fillImage.fillMethod = Image.FillMethod.Radial360;
		fillImage.fillOrigin = 2;
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
			startHolderScale,
			targetHoldercale,
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