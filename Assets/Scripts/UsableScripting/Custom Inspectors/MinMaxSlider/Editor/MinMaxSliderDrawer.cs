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
	private readonly int amountOfFieldsInProperty = 2;
	private readonly float fieldHeight = EditorGUIUtility.singleLineHeight;

	private readonly float fieldPaddingY = 2f;
	private readonly float fieldWidth = 60f;
	private float SliderHorizontalOffset { get { return fieldWidth + 10f; } }

	//private readonly float minSliderValue = 0.1f;
	//private readonly float maxSliderValue = 10f;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty(position, label, property);

		//Draw label.
		//"PrefixLabel" adds a field for the name of the variable being drawn (in this the variable for the MinMaxValue-class).
		//All Fields end up being indented a bit to the right of the label.
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		int oldIndent;
		SetIndent(out oldIndent, 0);

		#region Field Positioning

		//offset position.y by field size & Calculate Rects.
		//Save the start pos to add it if you add more fields.
		float startPosY = position.y;
		var minSliderRangeLabelRect = new Rect(position.x, startPosY, fieldWidth, fieldHeight);
		var maxSliderRangeLabelRect = new Rect(position.x + position.width - fieldWidth, startPosY, fieldWidth, fieldHeight);
		var minSliderRect = new Rect(position.x + SliderHorizontalOffset, startPosY, position.width - (SliderHorizontalOffset * 2), fieldHeight);

		//offset position.y by field size
		startPosY += fieldHeight + fieldPaddingY;
		var minLabelRect = new Rect(position.x + (fieldWidth / 2), startPosY, fieldWidth + (fieldWidth / 4), fieldHeight);
		var maxLabelRect = new Rect(position.x + position.width - (fieldWidth * 2), startPosY, fieldWidth + (fieldWidth / 4), fieldHeight);

		#endregion Field Positioning

		#region Get Properties & Create float values

		//Get references to the properties for a slider's minimum/maximum range.
		var minSliderRangeProp = property.FindPropertyRelative("minSliderRange");
		var maxSliderRangeProp = property.FindPropertyRelative("maxSliderRange");
		float minSliderRangeValue = minSliderRangeProp.floatValue;
		float maxSliderRangeValue = maxSliderRangeProp.floatValue;

		//Get references to the min/max value-properties and their floatvalues.
		var minProp = property.FindPropertyRelative("min");
		var maxProp = property.FindPropertyRelative("max");
		float minval = minProp.floatValue;
		float maxval = maxProp.floatValue;

		#endregion Get Properties & Create float values

		#region Min/Max Range of Sliders

		//Create labels with tooltips for the Float-fields.
		var minSliderLabel = new GUIContent("Min", "Minimum Value for the slider.");
		var maxSliderLabel = new GUIContent("Max", "Maximum Value for the slider.");

		//Draw MinMax-Float fields without shittons of label padding
		var originalLabelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 30f;
		minSliderRangeValue = EditorGUI.FloatField(minSliderRangeLabelRect, minSliderLabel, minSliderRangeValue);
		maxSliderRangeValue = EditorGUI.FloatField(maxSliderRangeLabelRect, maxSliderLabel, maxSliderRangeValue);
		EditorGUIUtility.labelWidth = originalLabelWidth;

		//Clamp slider's min/max range to stop from crossing each other and balla ur.
		minSliderRangeValue = Mathf.Clamp(minSliderRangeValue, float.MinValue, maxSliderRangeValue);
		maxSliderRangeValue = Mathf.Clamp(maxSliderRangeValue, minSliderRangeValue, float.MaxValue);

		//Assign clamped values to slider's min/max range.
		minSliderRangeProp.floatValue = minSliderRangeValue;
		maxSliderRangeProp.floatValue = maxSliderRangeValue;

		#endregion Min/Max Range of Sliders

		//Draw MinMaxSlider and save previous values to clamp the result later on.
		EditorGUI.MinMaxSlider(minSliderRect, ref minval, ref maxval, minSliderRangeValue, maxSliderRangeValue);
		float previousMaxVal = maxval;

		#region Min/Max Value
		//Create labels with tooltips for the Float-fields.
		var minLabel = new GUIContent("MinVal", "Minimum Value returned from the slider.");
		var maxLabel = new GUIContent("MaxVal", "Maximum Value returned from the slider.");

		//Draw Float fields without shittons of label padding
		originalLabelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 45f;
		minval = EditorGUI.FloatField(minLabelRect, minLabel, minval);
		maxval = EditorGUI.FloatField(maxLabelRect, maxLabel, maxval);
		EditorGUIUtility.labelWidth = originalLabelWidth;

		//Clamp the values to stop them from crossing each other.
		minval = Mathf.Clamp(minval, minSliderRangeValue, previousMaxVal);
		maxval = Mathf.Clamp(maxval, minval, maxSliderRangeValue);

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