using UnityEngine;
using System.Collections;

public class IntroMusic : MonoBehaviour {

	// Use this for initialization
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
    void Update() {
        print(Application.loadedLevel);
        if (Application.loadedLevel == 5)
            Destroy(gameObject);
    }
}

