using UnityEngine;
using System.Collections;

public class touchTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        //Touch myTouch = Input.GetTouch(0);

        //Touch[] myTouches = Input.touches;
        //for (int i = 0; i < Input.touchCount; i++)
       // {
       //     Debug.Log("touch");
        //}
        transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
    }
}
