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

public enum swipeDirection
{
    up,
    down
}

public class gameControl : MonoBehaviour {

    private handActivity hand;
    private axisActivity axis;
    private eyes domEye;

    private bool beat;
    private float beatTime;

    //public Text handDebug;
    //public Text axisDebug;
    //public Text handCon;
    //public Text axisCon;
    //public Text curAccel;
    //public Text curDirection;
    public Text scoreText;
    public Text HP;

    private Vector2 firstPress;
    private Vector2 secondPress;
    private Vector2 swipe;

    private direction Direct;
    private swipeDirection lastSwipe;

    public characterSprites touchSprite;
    public characterSprites axisSprite;

    public AudioClip[] music = new AudioClip[3];
    private float[] musicBeat = new float[] { 1.0f, 0.9f, 0.8f };
    private bool acceptBeat = false;
    private bool beatHit;
    private AudioSource audioSourse;
    private int currentDiff = 0;

    private int score;
    private int beatsHit;
    private int activBeatsHit;
    private int lives = 1000;


    //  blue   |  blue
    //  touch  |  axis
    //  1      |  2
    //---------|----------
    //  red    |  red
    //  touch  |  axis
    //  3      |  4

    //1
    public GameObject[] blueTouch = new GameObject[2];
    //2
    public GameObject[] blueAxis = new GameObject[2];
    //3
    public GameObject[] redTouch = new GameObject[2];
    //4
    public GameObject[] redAxis = new GameObject[2];
    private int randomZone = 0;


    // Use this for initialization
    void Start ()
    {
        domEye = GameObject.Find("eye").GetComponent<persisEye>().dominantEye;
        pickAct();
        lastSwipe = swipeDirection.up;
        audioSourse = this.gameObject.GetComponent<AudioSource>();
        audioSourse.clip = music[0];
        audioSourse.Play();
        StartCoroutine(beatWait());
	}

    IEnumerator beatWait()
    {
        yield return new WaitForSeconds(musicBeat[currentDiff]-0.05f);
        acceptBeat = true;
        beatHit = false;
        Debug.Log("beat");
        beatsHit++;
        if (beatsHit == 50 && currentDiff < 2)
        {
            beatsHit = 0;
            currentDiff++;
            Debug.Log("diff up");
            audioSourse.clip = music[currentDiff];
            audioSourse.Play();
        }
        activBeatsHit++;
        if (activBeatsHit == 10)
        {
            Debug.Log("change act");
            activBeatsHit = 0;
            pickAct();
        }
        yield return new WaitForSeconds(0.1f);
        acceptBeat = false;
        if (acceptBeat == false)
        {
            lives--;
        }
        StartCoroutine(beatWait());
    }
	
	// Update is called once per frame
	void Update ()
    {
        HP.text = lives.ToString();
        scoreText.text = score.ToString();
        //curAccel.text = Input.acceleration.ToString();
        //curDirection.text = Direct.ToString();
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
        if (lives <= 0)
        {
            //show overlay to score and quit 
        }
    }

    private void jumps()
    {
        if (Input.touches.Length > 0)
        {
            //handDebug.text = "touch";
            touchSprite.setFrame();
            //2 frames per tap
            StartCoroutine(jumpTimer());
            if (acceptBeat == true)
            {
                score = + 10 * (currentDiff + 1);
                beatHit = true;
            }
            else
            {
                lives = -1;
            }
        }
    }

    IEnumerator jumpTimer()
    {
        yield return new WaitForSeconds(0.1f);
        touchSprite.setFrame();
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
                //i framer per swipe. cant do the same swipe direction in a row
                if (swipe.y > 0 && (swipe.x > -0.5f || swipe.x < 0.5f) && lastSwipe == swipeDirection.down)
                {
                    //handDebug.text = "swipe up";
                    touchSprite.setFrame();
                    if (acceptBeat == true)
                    {
                        score = +10 * (currentDiff + 1);
                        beatHit = true;
                    }
                    else
                    {
                        lives = -1;
                    }
                }
                if (swipe.y < 0 && (swipe.x > -0.5f || swipe.x < 0.5f) && lastSwipe == swipeDirection.up)
                {
                    //handDebug.text = "swipe down";
                    touchSprite.setFrame();
                    if (acceptBeat == true)
                    {
                        score = +10 * (currentDiff + 1);
                        beatHit = true;
                    }
                    else
                    {
                        lives = -1;
                    }
                }
            }
        }
    }

    private void swimming()
    {
        //i framer per swipe. cant do the same swipe direction in a row
        if (Direct == direction.tiltBack && Input.acceleration.z < -0.2)
        {
            Direct = direction.tiltForward;
            //axisDebug.text = "tilt back";
            axisSprite.setFrame();
            if (acceptBeat == true)
            {
                score = +10 * (currentDiff + 1);
                beatHit = true;
            }
            else
            {
                lives = -1;
            }
        }
        if (Direct == direction.tiltForward && Input.acceleration.z > 0.2)
        {
            Direct = direction.tiltBack;
            //axisDebug.text = "tilt Forward";
            axisSprite.setFrame();
            if (acceptBeat == true)
            {
                score = +10 * (currentDiff + 1);
                beatHit = true;
            }
            else
            {
                lives = -1;
            }
        }
    }

    private void running()
    {
        //i framer per swipe. cant do the same swipe direction in a row
        if (Direct == direction.turnRight && Input.acceleration.x > 0.2)
        {
            Direct = direction.turnLeft;
            //axisDebug.text = "turn right";
            axisSprite.setFrame();
            if (acceptBeat == true)
            {
                score = +10 * (currentDiff + 1);
                beatHit = true;
            }
            else
            {
                lives = -1;
            }
        }
        if (Direct == direction.turnLeft && Input.acceleration.x < -0.2)
        {
            Direct = direction.turnRight;
            //axisDebug.text = "turn left";
            axisSprite.setFrame();
            if (acceptBeat == true)
            {
                score = +10 * (currentDiff + 1);
                beatHit = true;
            }
            else
            {
                lives = -1;
            }
        }
    }

    private void weights()
    {
        if (Direct == direction.tiltRight && Input.acceleration.y < -0.2)
        {
            Direct = direction.tiltLeft;
            //axisDebug.text = "tilt right";
            axisSprite.setFrame();
            if (acceptBeat == true)
            {
                score = +10 * (currentDiff + 1);
                beatHit = true;
            }
            else
            {
                lives = -1;
            }
        }
        if (Direct == direction.tiltLeft && Input.acceleration.y > 0.2)
        {
            Direct = direction.tiltRight;
            //axisDebug.text = "tilt left";
            axisSprite.setFrame();
            if (acceptBeat == true)
            {
                score = +10 * (currentDiff + 1);
                beatHit = true;
            }
            else
            {
                lives = -1;
            }
        }
    }

    public void changeActivity(handActivity tempHand)
    {
        if (tempHand == handActivity.jumps)
        {
            hand = handActivity.jumps;
            //handCon.text = hand.ToString();
        }
        else if (tempHand == handActivity.squats)
        {
            hand = handActivity.squats;
            //handCon.text = hand.ToString();
        }
        else
        {
            //handCon.text = "OH SHIT 1";
        }
    }

    public void changeActivity(axisActivity tempAxis)
    {
        if (tempAxis == axisActivity.running)
        {
            Direct = direction.turnRight;
            axis = axisActivity.running;
            //axisCon.text = axis.ToString();
        }
        else if (tempAxis == axisActivity.swimming)
        {
            Direct = direction.tiltBack;
            axis = axisActivity.swimming;
            //axisCon.text = axis.ToString();
        }
        else if (tempAxis == axisActivity.weights)
        {
            Direct = direction.tiltRight;
            axis = axisActivity.weights;
            //axisCon.text = axis.ToString();
        }
        else
        {
            //axisDebug.text = "OH SHIT 2";
        }
    }

    void pickAct()
    {
        //hide old activaties
        if (randomZone == 1 || randomZone == 4)
        {
            blueTouch[0].SetActive(false);
            blueTouch[1].SetActive(false);
            redAxis[0].SetActive(false);
            redAxis[1].SetActive(false);
        }
        else if (randomZone == 2 || randomZone == 3)
        {
            blueAxis[0].SetActive(false);
            blueAxis[1].SetActive(false);
            blueTouch[0].SetActive(false);
            blueTouch[1].SetActive(false);
        }
        //select new activaties
        randomZone = Random.Range(1, 4);
        GameObject tempAct;
        if (randomZone == 1 || randomZone == 4)
        {
            tempAct = blueTouch[Random.Range(0, 2)];
            tempAct.SetActive(true);
            touchSprite = tempAct.GetComponent<activityObject>().sprite.GetComponent<characterSprites>();
            changeActivity(tempAct.GetComponent<activityObject>().hanActiv);
            //move to less dom eye
            if (domEye == eyes.right)
            {
                tempAct.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
            }
            else
            {
                tempAct.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
            }
            tempAct = redAxis[Random.Range(0, 2)];
            tempAct.SetActive(true);
            axisSprite = tempAct.GetComponent<activityObject>().sprite.GetComponent<characterSprites>();
            changeActivity(tempAct.GetComponent<activityObject>().axisActiv);
            //move to more dominant eye side   
            if (domEye == eyes.right)
            {
                tempAct.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
            }
            else
            {
                tempAct.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
            }
        }
        else if (randomZone == 2 || randomZone == 3)
        {
            tempAct = blueAxis[Random.Range(0, 2)];
            tempAct.SetActive(true);
            axisSprite = tempAct.GetComponent<activityObject>().sprite.GetComponent<characterSprites>();
            changeActivity(tempAct.GetComponent<activityObject>().axisActiv);
            //move to less dominant eye side
            if (domEye == eyes.right)
            {
                tempAct.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
            }
            else
            {
                tempAct.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
            }
            tempAct = redTouch[Random.Range(0, 2)];
            tempAct.SetActive(true);
            touchSprite = tempAct.GetComponent<activityObject>().sprite.GetComponent<characterSprites>();
            changeActivity(tempAct.GetComponent<activityObject>().hanActiv);
            //move to more dominant eye side  
            if (domEye == eyes.right)
            {
                tempAct.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
            }
            else
            {
                tempAct.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
            }
        }
    }

}
