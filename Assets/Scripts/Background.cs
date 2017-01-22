using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour
{
   
    // Use this for initialization
    void Awake ()
    {
        DontDestroyOnLoad(transform.gameObject);
       
        
	}
	
	// Update is called once per frame
	void Update ()
    {
      
    }
}
