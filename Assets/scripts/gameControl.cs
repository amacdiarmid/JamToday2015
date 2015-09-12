using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum handActivity
{
    jumps,
    squats
}

public enum axisActivity
{
    swimming,
    running,
    weights
}

public enum direction
{
    tiltBack,
    tiltForward,
    tiltLeft,
    tiltRight,
    turnLeft,
    turnRight
}

public class gameControl : MonoBehaviour {

    private handActivity hand;
    private axisActivity axis;

    private bool beat;
    private float beatTime;

    public Text handDebug;
    public Text axisDebug;
    public Text handCon;
    public Text axisCon;
    public Text curAccel;
    public Text curDirection;

    private Vector2 firstPress;
    private Vector2 secondPress;
    private Vector2 swipe;

    private direction Direct;

    public 

	// Use this for initialization
	void Start ()
    {
        changeActivity(handActivity.jumps);
        changeActivity(axisActivity.running);
	}
	
	// Update is called once per frame
	void Update ()
    {
        curAccel.text = Input.acceleration.ToString();
        curDirection.text = Direct.ToString();
        switch (hand)
        {
            case handActivity.jumps:
                jumps();
                break;
            case handActivity.squats:
                squats();
                break;
            default:
                break;
        }
        switch (axis)
        {
            case axisActivity.swimming:
                swimming();
                break;
            case axisActivity.running:
                running();
                break;
            case axisActivity.weights:
                weights();
                break;
            default:
                break;
        }
    }

    private void jumps()
    {
        if (Input.touches.Length > 0)
        {
            handDebug.text = "touch";
        }
    }

    private void squats()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                firstPress = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                secondPress = new Vector2(t.position.x, t.position.y);
                swipe = new Vector2(secondPress.x - firstPress.x, secondPress.y - firstPress.y);
                swipe.Normalize();
                if (swipe.y > 0 && (swipe.x > -0.5f || swipe.x < 0.5f))
                {
                    handDebug.text = "swipe up";
                }
                if (swipe.y < 0 && (swipe.x > -0.5f || swipe.x < 0.5f))
                {
                    handDebug.text = "swipe down";
                }
            }
        }
    }

    private void swimming()
    {
        if (Direct == direction.tiltBack && Input.acceleration.z < -0.2)
        {
            Direct = direction.tiltForward;
            axisDebug.text = "tilt back";
        }
        if (Direct == direction.tiltForward && Input.acceleration.z > 0.2)
        {
            Direct = direction.tiltBack;
            axisDebug.text = "tilt Forward";
        }
    }

    private void running()
    {
        if (Direct == direction.turnRight && Input.acceleration.x > 0.2)
        {
            Direct = direction.turnLeft;
            axisDebug.text = "turn right";
        }
        if (Direct == direction.turnLeft && Input.acceleration.x < -0.2)
        {
            Direct = direction.turnRight;
            axisDebug.text = "turn left";
        }
    }

    private void weights()
    {
        if (Direct == direction.tiltRight && Input.acceleration.y < -0.2)
        {
            Direct = direction.tiltLeft;
            axisDebug.text = "tilt right";
        }
        if (Direct == direction.tiltLeft && Input.acceleration.y > 0.2)
        {
            Direct = direction.tiltRight;
            axisDebug.text = "tilt left";
        }
    }

    public void changeActivity(handActivity tempHand)
    {
        if (tempHand == handActivity.jumps)
        {
            hand = handActivity.jumps;
            handCon.text = hand.ToString();
        }
        else if (tempHand == handActivity.squats)
        {
            hand = handActivity.squats;
            handCon.text = hand.ToString();
        }
        else
        {
            handCon.text = "OH SHIT 1";
        }
    }

    public void changeActivity(axisActivity tempAxis)
    {
        if (tempAxis == axisActivity.running)
        {
            Direct = direction.turnRight;
            axis = axisActivity.running;
            axisCon.text = axis.ToString();
        }
        else if (tempAxis == axisActivity.swimming)
        {
            Direct = direction.tiltBack;
            axis = axisActivity.swimming;
            axisCon.text = axis.ToString();
        }
        else if (tempAxis == axisActivity.weights)
        {
            Direct = direction.tiltRight;
            axis = axisActivity.weights;
            axisCon.text = axis.ToString();
        }
        else
        {
            axisDebug.text = "OH SHIT 2";
        }
    }

}
