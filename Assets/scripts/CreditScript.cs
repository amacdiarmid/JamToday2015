using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour {


	
	public float speed = 5;
	
	void Update ()
	{
		transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));
	}


}
