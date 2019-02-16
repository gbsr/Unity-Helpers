//Written by Pablo Sorribes Bernhard, 2019 02 16

using UnityEditor;
using UnityEngine;

/// <summary>
/// Note that "property" in this case means the entire drawer for all the fields and labels.
/// </summary>

//Tutorial showing how to draw a PropertyDrawer: https://riptutorial.com/unity3d/example/8282/custom-property-drawer
//Drawing an actual property field: https://youtu.be/-bxaYugwVL4?t=1507
[CustomPropertyDrawer(typeof(MinMaxSlider))]
public class MinMaxSliderDrawer : PropertyDrawer
{
	//Update this for how many fields/lines you want to draw in your property.
	private readonly int amountOfFieldsInProperty = 2;		
	private readonly float fieldHeight = EditorGUIUtility.singleLineHeight;

	private readonly float fieldPaddingY = 2f;
	private readonly float fieldWidth = 60f;
	private float SliderHorizontalOffset { get { return fieldWidth + 10f; } }

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty(position, label, property);

		var prefixLabelContent = label;
		prefixLabelContent.tooltip = $"Slider allowing you to set your custom ranges, and get the respective min/max values set in the slider.";

		//Draw label.
		//"PrefixLabel" adds a field for the name of the variable being drawn (in this case the variable for the MinMaxSlider-class).
		//All Fields end up being indented a bit to the right of the label.
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), prefixLabelContent);
		//position = EditorGUI.IndentedRect(position);
		//EditorGUI.DropShadowLabel(position, label);
		
		int oldIndent;
		SetIndent(out oldIndent, 0);

		#region Field Positioning

		//offset position.y by field size & Calculate Rects.
		//Save the start pos to add it if you add more fields.
		float startPosY = position.y;
		var minSliderRangeLabelRect = new Rect(position.x, startPosY, fieldWidth, fieldHeight);
		var maxSliderRangeLabelRect = new Rect(position.x + position.width - fieldWidth, startPosY, fieldWidth, fieldHeight);
		var sliderRect = new Rect(position.x + SliderHorizontalOffset, startPosY, position.width - (SliderHorizontalOffset * 2), fieldHeight);

		//offset position.y by field size
		startPosY += fieldHeight + fieldPaddingY;
		var minValLabelRect = new Rect(position.x + (fieldWidth / 2), startPosY, fieldWidth + (fieldWidth / 4), fieldHeight);
		var maxValLabelRect = new Rect(position.x + position.width - (fieldWidth * 2), startPosY, fieldWidth + (fieldWidth / 4), fieldHeight);

		#endregion Field Positioning

		#region Get Properties & Create float values

		//Get references to the properties for a slider's minimum/maximum range.
		var minSliderRangeProp = property.FindPropertyRelative("sliderRangeMin");
		var maxSliderRangeProp = property.FindPropertyRelative("sliderRangeMax");
		float minSliderRangeVal = minSliderRangeProp.floatValue;
		float maxSliderRangeVal = maxSliderRangeProp.floatValue;

		//Get references to the min/max value-properties and their float values.
		var minProp = property.FindPropertyRelative("minVal");
		var maxProp = property.FindPropertyRelative("maxVal");
		float minval = minProp.floatValue;
		float maxval = maxProp.floatValue;

		#endregion Get Properties & Create float values

		#region Min/Max Range of Sliders

		//Create labels with tooltips for the Float-fields.
		var minSliderLabel = new GUIContent("Min", "Lower total range for the slider.");
		var maxSliderLabel = new GUIContent("Max", "Upper total range for the slider.");

		//Draw MinMax Slider float fields without shittons of label padding
		var originalLabelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 30f;
		minSliderRangeVal = EditorGUI.FloatField(minSliderRangeLabelRect, minSliderLabel, minSliderRangeVal);
		maxSliderRangeVal = EditorGUI.FloatField(maxSliderRangeLabelRect, maxSliderLabel, maxSliderRangeVal);
		EditorGUIUtility.labelWidth = originalLabelWidth;

		//Clamp slider's min/max range to stop from crossing each other and balla ur.
		minSliderRangeVal = Mathf.Clamp(minSliderRangeVal, float.MinValue, maxSliderRangeVal);
		maxSliderRangeVal = Mathf.Clamp(maxSliderRangeVal, minSliderRangeVal, float.MaxValue);

		//Assign clamped values to slider's min/max range.
		minSliderRangeProp.floatValue = minSliderRangeVal;
		maxSliderRangeProp.floatValue = maxSliderRangeVal;

		#endregion Min/Max Range of Sliders

		//Draw MinMaxSlider and save previous values to clamp the result later on.
		EditorGUI.MinMaxSlider(sliderRect, ref minval, ref maxval, minSliderRangeVal, maxSliderRangeVal);
		float previousMaxVal = maxval;

		#region Min/Max Value
		//Create labels with tooltips for the Float-fields.
		var minLabel = new GUIContent("MinVal", "Minimum Value returned from the slider.");
		var maxLabel = new GUIContent("MaxVal", "Maximum Value returned from the slider.");

		//Draw Float fields without shittons of label padding
		originalLabelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 45f;
		minval = EditorGUI.FloatField(minValLabelRect, minLabel, minval);
		maxval = EditorGUI.FloatField(maxValLabelRect, maxLabel, maxval);
		EditorGUIUtility.labelWidth = originalLabelWidth;

		//Clamp the values to stop them from crossing each other.
		minval = Mathf.Clamp(minval, minSliderRangeVal, previousMaxVal);
		maxval = Mathf.Clamp(maxval, minval, maxSliderRangeVal);

		//Set the clamped slider/float field values.
		minProp.floatValue = minval;
		maxProp.floatValue = maxval;

		#endregion Min/Max Value

		//Reset indent and update the variable.
		SetIndent(out oldIndent, oldIndent);

		EditorGUI.EndProperty();

		//Allow for Undo/Redo on the values of the Sliders.
		property.serializedObject.ApplyModifiedProperties();
	}

	/// <summary>
	/// Returns the total needed amount of pixels for drawing the property drawer, including all of its fields and labels.
	/// </summary>
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return (fieldHeight + fieldPaddingY) * amountOfFieldsInProperty;
	}

	private void SetIndent(out int currentIndent, int desiredIndent)
	{
		currentIndent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = desiredIndent;
	}
}