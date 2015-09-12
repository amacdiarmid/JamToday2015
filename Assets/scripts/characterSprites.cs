using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class characterSprites : MonoBehaviour {

    public Sprite idle;
    public Sprite frame1;
    public Sprite frame2;
    private SpriteRenderer image;

	// Use this for initialization
	void Start ()
    {
        image = this.gameObject.GetComponent<SpriteRenderer>();
        image.sprite = idle;
	}

    public void setFrame()
    {
        if (image.sprite == idle)
        {
            image.sprite = frame1;
        }
        else if (image.sprite == frame1)
        {
            image.sprite = frame2;
        }
        else if (image.sprite == frame2)
        {
            image.sprite = frame1;
        }
    }
}
