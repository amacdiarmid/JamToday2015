using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class playermovement : MonoBehaviour {

    private Vector3 startTransform;
    private Vector3 startRotation;

    private Vector3[] accelList = new Vector3[20];
    private int i = 0;

    public Text startTran;
    public Text curTran;
    public Text debugText;
    public Image light;
     
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
        Vector3 acceleration = Vector3.zero;
        if (i != 20)
        {
            light.color = Color.yellow;
            accelList[i] = Input.acceleration;
            i++;
        }
        else
        {
            //i hate myself for doing it this way but i cant think of antoher so yeah deal with it me future me
            accelList[0] = accelList[1];
            accelList[1] = accelList[2];
            accelList[2] = accelList[3];
            accelList[3] = accelList[4];
            accelList[4] = accelList[5];
            accelList[5] = accelList[6];
            accelList[6] = accelList[7];
            accelList[7] = accelList[8];
            accelList[8] = accelList[9];
            accelList[9] = accelList[10];
            accelList[10] = accelList[11];
            accelList[11] = accelList[12];
            accelList[12] = accelList[13];
            accelList[13] = accelList[14];
            accelList[14] = accelList[15];
            accelList[15] = accelList[16];
            accelList[16] = accelList[17];
            accelList[17] = accelList[18];
            accelList[18] = accelList[19];
            accelList[19] = accelList[20];
            accelList[20] = Input.acceleration;
        }
        //go up
        if (accelList[0].z > 0.1 && accelList[20].z > 0.1 )
        {
            transform.Translate(new Vector3(0, 1, 0));
            light.color = Color.green;
        }
        //go down
        else if (accelList[0].z < -0.1 && accelList[20].z < -0.1)
        {
            transform.Translate(new Vector3(0, -1, 0));
            light.color = Color.green;
        }
        else
        {
            debugText.text = accelList[0].ToString() + "    " + accelList[0].ToString();
            light.color = Color.white;
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
        Input.acceleration.Normalize();
        currentAcel.Normalize();
        return currentAcel.ToString();
    }
}
