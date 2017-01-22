using UnityEngine;
using System.Collections;

public class Sattelite : MonoBehaviour
{
 
    
    [SerializeField]GameObject Spawn;
    [SerializeField]GameObject OutOfNowhere;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Spawn);
        DontDestroyOnLoad(OutOfNowhere);
    }

    void Start ()
    {
        
    }
	
	
	void Update ()
    {
        transform.Translate(Vector3.left * Time.deltaTime );
        transform.Translate(Vector3.down * Time.deltaTime / 8);
        
    }

    void FixedUpdate()
    {
       
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "OutOfNowhere")
        {
            transform.position = Spawn.transform.position;
        }
    }
}
