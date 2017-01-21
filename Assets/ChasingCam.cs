using UnityEngine;
using System.Collections;

public class ChasingCam : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, 0,-10) ;
        Debug.Log(Player.transform.position.x);
	}
}
