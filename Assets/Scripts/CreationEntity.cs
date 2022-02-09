using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationEntity : MonoBehaviour
{
	public float radius = 1;
	public Vector2 regionSize = Vector2.one;
	public int rejectionSamples = 30;
	public float displayRadius = 1;
	public GameObject[] objects;
	public float[] prob;
	List<Vector2> points;

	void OnValidate()
	{
		points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);
	}
    void Start()
    {
		if (points != null)
		{
			foreach (Vector2 point in points)
			{
				Vector3 newpoint = new Vector3(point.x, 1, point.y);
				int randPos = Random.Range(0, objects.Length);
				if (Random.Range(0f,1f)<= prob[randPos])
					Instantiate(objects[randPos], newpoint, Quaternion.Euler(Vector3.up*(Random.Range(0,4)*90)));

			}
		}
    }
    void OnDrawGizmos()
    {
        Vector3 line = new Vector3(regionSize.x, 1, regionSize.y) / 2;
        Vector3 size = new Vector3(regionSize.x, 1, regionSize.y);
        Gizmos.DrawWireCube(line, size);
        if (points != null)
        {
            foreach (Vector2 point in points)
            {
                Vector3 newpoint = new Vector3(point.x, 1, point.y);
                Gizmos.DrawSphere(newpoint, displayRadius);
            }
        }
    }
}
