using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This script keeps tracks information of a specific chunk of level,
/// Used by the LevelManager

public class LevelChunk : MonoBehaviour
{
    public float levelLength = 10f;
	public float safety = 6f;

	private void OnDrawGizmos()
	{
		// draw a box showing how much space the chunk takes up.
		Vector3 center = transform.position + new Vector3(0, 1.5f, levelLength / 2);
		Vector3 size = new Vector3(6, 3, levelLength);
		Gizmos.color = Color.white; 
		Gizmos.DrawWireCube(center, size);

		center = transform.position + new Vector3(0, 1.5f, safety / 2);
		size = new Vector3(6, 3, safety);
		Gizmos.color = new Color(1, 0, 0, 0.2f); // Color.red
		Gizmos.DrawCube(center, size);
	}
}
