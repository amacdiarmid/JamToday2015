using UnityEngine;
using System.Collections;

public enum activity
{
    touch,
    axis
}

public class activityObject : MonoBehaviour {

    public GameObject sprite;
    public activity activ;
    public handActivity hanActiv;
    public axisActivity axisActiv;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
