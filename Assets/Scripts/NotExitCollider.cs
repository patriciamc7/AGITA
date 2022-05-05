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
			kinctMovePlayer.Bolexit = false;
			collName = name;
		}

	}

	private void OnTriggerStay(Collider other)
	{
		kinctMovePlayer.Bolexit = true;
	}
}
