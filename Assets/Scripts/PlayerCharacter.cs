using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{

    float fy = 0;
    float vy = 0;
    float vx = 1;
    float GRAVITY = 19.81f;
    float PLAYERTHRUST = 15;
    float MAXFY = 730;
    float Timer = 0;

    float MAXTIME = 20;
    float timeBeforeRestart = 1f;


    [SerializeField]
    Text time;
    [SerializeField]
    GameObject GameOverText;

    float clock = 0;
    int targetLeft ;

    // Use this for initialization
    void Start ()
    {
        GameOverText.SetActive(false);
        targetLeft = GameObject.FindGameObjectsWithTag("Item").Length;

    }

    // Update is called once per frame
    void Update() 
    {

        
        if (clock > MAXTIME)
        {
            
            GameOverText.SetActive(true);
            Time.timeScale = 0;
            if (Input.anyKey)
            {
                restartCurrentScene();
                Time.timeScale = 1;
            }
        }

        if(targetLeft == 0)
        {
            SoundEffects.Instance.Win();
            Time.timeScale = 0;
            if(Input.anyKey)
            {
                LoadNextScene();
                Time.timeScale = 1;
                
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            restartCurrentScene();
            Time.timeScale = 1;
        }



        /////////////MOVE//////////////
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Timer = 0;
        }
        Timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Timer += Time.deltaTime;
            if (Timer < 0.5f)
            {
                fy += PLAYERTHRUST;

            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Timer > 0.5f)
            {
                fy -= PLAYERTHRUST;

            }
        }

        float grav = transform.position.y * 10;
        //Debug.Log("t" + Timer);
        fy -= grav;
        fy = Mathf.Max(fy, -MAXFY);
        fy = Mathf.Min(fy, MAXFY);
            //Debug.Log("fy" + fy);

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {

        }

        if(Input.GetKeyUp(KeyCode.DownArrow))
        {

        }
        //float grav = GRAVITY;
        //if (transform.position.y > 0)
        //{
        //    fy -= grav;
        //}0
        //if (transform.position.y < 0)

        //{0
        //    fy += grav;
        //}
        //transform.position = new Vector2(0, fy * 0.01f);
        vy += fy * Time.deltaTime;
        vy = vy * 0.99f;
        transform.position = new Vector2(transform.position.x + 3.5f* Time.deltaTime*vx, vy * Time.deltaTime );

        clock += Time.deltaTime;

        time.text = Mathf.Round((MAXTIME -clock) *100)/100+"";
        //vy += fy * Time.deltaTime;



    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Item")
        {
            SoundEffects.Instance.PickItem();
            vy += 1;
            Destroy(collider.gameObject);
            targetLeft -= 1;

        }
        if (collider.gameObject.tag == "Limit")
        {
            SoundEffects.Instance.KnockLimit();
            vx = vx *-1;
        }
        if (collider.gameObject.tag == "LimitVertical")
        {
            SoundEffects.Instance.KnockLimit();
            fy = fy * -1 * .9f;
        }
    }
    public void restartCurrentScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
