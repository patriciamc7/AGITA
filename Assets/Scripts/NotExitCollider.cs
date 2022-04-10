using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotExitCollider : MonoBehaviour
{
	//public GameObject player;
	public KinctMovePlayer kinctMovePlayer; 
	public string collName;

	private void OnTriggerExit(Collider other)
	{
		
		if (other.gameObject.name == "Cube")
		{
			kinctMovePlayer.Bolexit = true;
			collName = name;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		kinctMovePlayer.Bolexit = false;
	}
}
