using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to showcase how a custom inspector can affect array elements, with buttons to add/remove array elements, etc.
/// </summary>
public class AlignmentDatabase : MonoBehaviour
{
	public List<Alignement> alignements = new List<Alignement>();
}

[System.Serializable]
public class Alignement
{
	public string alignementName = string.Empty;
	public Color backgroundColor = Color.white;
}