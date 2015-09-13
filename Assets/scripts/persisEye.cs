using UnityEngine;
using System.Collections;

public enum eyes
{
    left,
    right
}

public class persisEye : MonoBehaviour {

    public eyes dominantEye;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
