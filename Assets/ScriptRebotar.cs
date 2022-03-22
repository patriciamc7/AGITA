using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRebotar : MonoBehaviour
{
	public GameObject player;
	public bool Bolexit = false; 
	public string collName;
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "PointRay")
		{
			Bolexit = true;
			collName = name; 
			Debug.Log("hola"); 
		}
		
	}

	private void OnTriggerEnter(Collider other)
	{
		Bolexit = false;
		//Debug.Log("Entra");

	}
	//private void OnTriggerExit(Collider other)
	//{
	//	if (other.gameObject.name == "Player")
	//	{ 
	//	    Debug.Log("exit");

	//	}
	//}

	//private void OnCollisionExit(Collision collision)
	//{
	//       Debug.Log("HOLA");
	//       if (collision.transform.tag == "Player")
	//       {
	//           Debug.Log("HOLA"); 
	//           Rigidbody rb = collision.rigidbody;
	//           rb.AddExplosionForce(fuerzadelrebote, collision.contacts[0].point, 5);

	//       }
	//   }

}
