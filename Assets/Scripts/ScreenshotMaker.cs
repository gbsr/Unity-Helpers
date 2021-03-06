﻿using UnityEngine;

/// <summary>
/// Simple script for saving screenshots in the Persistent Data-directory and thus
/// avoid possible Dropbox-notifications or the like showing up while doing screenshots.
/// <para>See picture of the problem: https://www.dropbox.com/s/tivgyuugmqb6ov7/Screenshot%20%28with%20Dropbox-notification%29.png?dl=0 </para>
/// </summary>
public class ScreenshotMaker : MonoBehaviour
{
	//Consider setting this to the property "Application.productName" in Awake().
	public string yourGameName = string.Empty;

	//The path where your Screenshots will be saved.
	public static string DataPath { get { return Application.persistentDataPath + "/Screenshots/"; } }

	[Tooltip("Choose a key to your liking here.")]
	public KeyCode keyToDoScreenshotWith = KeyCode.F12;

	//Is public to be able to set it through other scripts for doing higher res pics.
	public ResolutionOverloads resolutionToUse = ResolutionOverloads.normalResolution;

	[Tooltip("Flip how the files should be named: FALSE = ScreenRes_Date, TRUE = Date_ScreenRes")]
	public bool dateBeforeScreenRes = false;

	/// <summary> For doing higher resolution screenshots. Begins on "1" to not cause trouble when used in multiplication. </summary>
	public enum ResolutionOverloads { normalResolution = 1, doubleResolution, tripleResolution, quadrupleResolution }

	private void Awake()
	{
		if (!System.IO.Directory.Exists(DataPath))
			System.IO.Directory.CreateDirectory(DataPath);
	}

	private void Update()
	{
		if (Input.GetKeyDown(keyToDoScreenshotWith))
			CaptureScreen();
	}

	/// <summary>
	/// Saves a Screenshot into the application's Persistent Data-path, in a "Screenshots"-folder.
	/// Size will be the current screen resolution, and fileName will be marked by the current machine's date and time.
	/// </summary>
	private void CaptureScreen()
	{
		//These variables do not affect the actual Screenshot-resolution. They are used for setting the filename.
		int widthString = Screen.width;
		int heightString = Screen.height;

		//Change depending on the value of the resolutionToUse, to give correct fileName.
		widthString *= (int)resolutionToUse;
		heightString *= (int)resolutionToUse;

		string fileName;
		if (dateBeforeScreenRes)
		{
			//String concatenation courtesy of: https://answers.unity.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html
			fileName = string.Format("{0}{1}_Screenshot_{4}_{2}x{3}.png",
								  DataPath,
								  yourGameName,
								  widthString,
								  heightString,
								  System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
		}
		else
		{
			fileName = string.Format("{0}{1}_Screenshot_{2}x{3}_{4}.png",
								  DataPath,
								  yourGameName,
								  widthString,
								  heightString,
								  System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
		}

		//Capture the screen and log it.
		ScreenCapture.CaptureScreenshot(fileName, (int)resolutionToUse);
		Debug.Log("Saved screeshot at: " + fileName);
	}
}