using UnityEngine;

// This script shows how you can combine meshes in runtime.

// "Manually combining GameObjects that are close to each other can be a very good alternative to draw call batching. 
// For example, a static cupboard with lots of drawers often makes sense to just combine into a single Mesh, 
// either in a 3D modeling application or using Mesh.CombineMeshes." 
// - (https://docs.unity3d.com/Manual/DrawCallBatching.html)

// Copy meshes from children into the parent's Mesh.
// CombineInstance stores the list of meshes.
// These are combined and assigned to the attached Mesh.
// Source: https://docs.unity3d.com/ScriptReference/Mesh.CombineMeshes.html

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
	private void Start()
	{
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
		CombineInstance[] meshesToCombine = new CombineInstance[meshFilters.Length];

		int i = 0;
		while (i < meshFilters.Length)
		{
			meshesToCombine[i].mesh = meshFilters[i].sharedMesh;
			meshesToCombine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			meshFilters[i].gameObject.SetActive(false);

			i++;
		}

		//Create a new mesh out of the combined meshes of the children.
		transform.GetComponent<MeshFilter>().mesh = new Mesh();
		transform.GetComponent<MeshFilter>().mesh.CombineMeshes(meshesToCombine);
		transform.gameObject.SetActive(true);
	}
}