This script can be used to create a "Skip Cutscene"-button, 
ie. an UI-element with an icon for a button and a round progress bar which 
increases while the player presses said button, and decreases when the button is released. 

The script can also affect the scale of the button icon, making it become bigger or smaller
when the player presses the button, and return to its initial size when the button is released.

Keep in mind that you will *not* need Unity's UI Button-component for this to work.
This script just affects a circle-shaped UI Image to simulate the progress bar-effect.

For this example I have made 3 circle-shaped images, where each is a bit smaller than the last. 
But you could probably just a same sized circle and then rescale it in Unity tho.

***Hierarchy setup:***
- SkipCutSceneButton [RectTransform] - 	[OPTIONAL] An empty parent with the script attached. 
										Recommended to have, since it allows for resizing the entire button without 
										messing with the direction of the size-adjustment performed on the HolderRect.
	- HolderRect [RectTransform/Image] - Either an empty RectTransform or an Image acting as a background circle.
		- FillImage [Image] - The progress bar. An Image-component (a circle sprite), set to ImageType = Filled.
		- ButtonIconImage [Image] - [OPTIONAL] An icon of the button that this script is going to react to (could be anything really, this is just graphics).
	
***Source:***
https://www.febucci.com/2019/02/skip-cutscenes-button/