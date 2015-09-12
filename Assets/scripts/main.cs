using UnityEngine;
using System.Collections;

public class main : MonoBehaviour {

	// Use this for initialization
	public void ChangeScene (string sceneName){
		Application.LoadLevel (sceneName);
	}

}
