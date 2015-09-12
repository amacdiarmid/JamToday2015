using UnityEngine;
using System.Collections;

public class backgroundScript : MonoBehaviour {
	public Texture2D background;

	void OnGUI (){
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
