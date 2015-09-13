using UnityEngine;
using System.Collections;

public class activityObject : MonoBehaviour {

    public GameObject sprite;
    public handActivity hanActiv;
    public axisActivity axisActiv;


    public void Reset()
    {
        sprite.GetComponent<SpriteRenderer>().sprite = sprite.GetComponent<characterSprites>().idle;
    }
}
