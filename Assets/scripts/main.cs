using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class main : MonoBehaviour {

    public Slider slider;
    private persisEye perEye;

    void Awake()
    {
       perEye = GameObject.Find("eye").GetComponent<persisEye>();
    }

	// Use this for initialization
	public void ChangeScene (string sceneName){
		Application.LoadLevel (sceneName);
	}

    public void setEyeValue()
    {
        if (slider.value == 0)
        {
            perEye.dominantEye = eyes.left;
        }
        else if (slider.value == 1)
        {
            perEye.dominantEye = eyes.right;
        }
    }
}
