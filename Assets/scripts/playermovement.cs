using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playermovement : MonoBehaviour {

    private Vector3 startTransform;
    private Vector3 startRotation;

    public Text startTran;
    public Text curTran;

    public Vector3 currentAcel;

	// Use this for initialization
	void Start ()
    {
        setStartTransform();
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentAcel = Input.acceleration;
        startTran.text = showStartTransform();
        curTran.text = showCurTransform();
        if (Input.acceleration.z < - 0.2)
        {
            transform.Translate(new Vector3(0, -0.1f, 0));
        }
        else if (Input.acceleration.z > 0.2)
        {
            transform.Translate(new Vector3(0, 0.1f, 0));
        }
    }

    public string showStartTransform()
    {
        return startTransform.ToString();
    }

    public void setStartTransform()
    {
        startTransform = Input.acceleration;
    }

    public string showCurTransform()
    {
        currentAcel.Normalize();
        return currentAcel.ToString();
    }
}
