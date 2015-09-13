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
            Debug.Log("left eye");
            perEye.dominantEye = eyes.left;
        }
        else if (slider.value == 1)
        {
            Debug.Log("right eye");
            perEye.dominantEye = eyes.right;
        }
    }
}
