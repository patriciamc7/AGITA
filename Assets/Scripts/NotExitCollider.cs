using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotExitCollider : MonoBehaviour
{
	//public GameObject player;
	public bool Bolexit = false;
	public string collName;

	private void OnTriggerExit(Collider other)
	{
		
		if (other.gameObject.name == "Cube")
		{
			Bolexit = true;
			collName = name;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		Bolexit = false;
	}
}
