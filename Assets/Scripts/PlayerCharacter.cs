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
    [SerializeField]
    float GRAVITY = 0.01f;
    [SerializeField]
    float FIXEDGRAVITY = 0.01f;
    [SerializeField]
    float PLAYERTHRUST = 0.01f;
    [SerializeField]
    float PLAYERTHRUSTMAXLENGTH = 0.5f;
    [SerializeField]
    float MAXFY = 4;
    [SerializeField]
    float DAMPING = .99f;
    float Timer = 0;
    float TimeBeforeRestart = 0.5f;
    float countItem = 0;

    [SerializeField]
    float MAXTIME = 20;

    bool Started = false;
    bool lose = false;
    [SerializeField]
    Text time;
    [SerializeField]
    GameObject GameOverText;
    [SerializeField]Scene LastScene;
    [SerializeField]Animator Wave;
    float waveSpawnTime = 0.08f;
    Vector3 DefaultScale = new Vector3(1.4f,1.4f,0);
    Vector3 newScale = new Vector3(0.7f,0.7f, 0);
    bool Scaled;
    
    Animator animator;

    float clock = 0;
    int targetLeft;

    void Awake()
    {
      
    }
    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("WaveEffect", waveSpawnTime, waveSpawnTime);
        GameOverText.SetActive(false);
        targetLeft = GameObject.FindGameObjectsWithTag("Item").Length;
        animator = GetComponent<Animator>();
       
        vy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Started = true;
        if (clock > MAXTIME)
        { 
            GameOverText.SetActive(true);
            Time.timeScale = 0;
            TimeBeforeRestart -= Time.unscaledDeltaTime;
            if (Input.anyKey && TimeBeforeRestart <= 0)
            {
                
                restartCurrentScene();
                Time.timeScale = 1;
                
            }
        }

        if(targetLeft == 0)
        {
            Time.timeScale = 0;
            TimeBeforeRestart -= Time.unscaledDeltaTime;
            if (Input.anyKey && TimeBeforeRestart <= 0)
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
            Started = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            Wave.transform.localScale = DefaultScale;
            Scaled = false;
        }
            if (!Started)
        {
            return;
        }
        fy = 0;
        Timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            
            if (Timer < 0.5f)
            {
                if (!Scaled)
                {
                    Wave.transform.localScale = newScale;
                    Scaled = true;
                }
                fy += PLAYERTHRUST;

            }
            
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            
            if (Timer < 0.5f)
            {
                if (!Scaled)
                {
                    Wave.transform.localScale = newScale;
                    Scaled = true;
                }
                fy -= PLAYERTHRUST;
            }
                            
        }
       
       


        float grav = transform.position.y * GRAVITY + Mathf.Sign(transform.position.y) * FIXEDGRAVITY;
        //float grav = transform.position.y * GRAVITY + Mathf.Sign(transform.position.y) * 0.02f;
        //float grav = Mathf.Sign(transform.position.y) * Mathf.Sqrt(Mathf.Abs(transform.position.y)) * GRAVITY;

        //Debug.Log("t" + Timer);
        fy -= grav;

        //if (Mathf.Abs(fy) > MAXFY)
        //{
        //    Debug.Log("limit " + fy);
        //    fy *= .95f;
        //}
        fy = Mathf.Max(fy, -MAXFY);
        fy = Mathf.Min(fy, MAXFY);
        //Debug.Log("fy" + fy);

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
        //vy += fy * Time.deltaTime;

        vy = DAMPING * vy + fy * Time.deltaTime;
        Debug.Log("fy" + fy + " g " + grav + " dx: " + vy * Time.deltaTime);

        if (Started)
        {
            transform.position = new Vector2(transform.position.x + 3.5f * Time.deltaTime * vx, transform.position.y + vy * Time.deltaTime);
            clock += Time.deltaTime;
        }
        //vy = vy * 0.8f;

        time.text = Mathf.Round((MAXTIME -clock) *100)/100+"";
        //vy += fy * Time.deltaTime;
    }

    void FixedUpdate()
    {
       
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Item")
        {
            SoundEffects.Instance.PickItem();
            //vy += 1;
            Destroy(collider.gameObject);
            targetLeft -= 1;
            //targetLeft = 0;
            Debug.Log("eats");
            animator.SetTrigger("eats");
            countItem += 1;
        }

        if (collider.gameObject.tag == "Limit")
        {
            SoundEffects.Instance.KnockLimit();
            vx = vx *-1;
            transform.localScale = new Vector3(vx, 1, 1);
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
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
    void WaveEffect()
    {
        Instantiate(Wave, transform.position, transform.rotation);
    }
    
}
