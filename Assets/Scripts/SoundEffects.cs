using UnityEngine;
using System.Collections;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects Instance;

    [SerializeField]
    AudioClip PickItemSound;
    [SerializeField]
    AudioClip KnockLimitSound;
    [SerializeField]
    AudioClip WinSound;
    [SerializeField]
    AudioClip LoseSound;
    [SerializeField]
    AudioSource Bass;
    [SerializeField]
    AudioSource base1;
    int targetRemaining;
    bool restart = true;
    


    void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple instances of SoundEffects!");
        }
        Instance = this;
        //Bass = GetComponent<AudioSource>();
        DontDestroyOnLoad(transform.gameObject);
        
    }

    void Start()
    {
        //Bass.Play();

    }

    void Update()
    {
        targetRemaining = GameObject.FindGameObjectsWithTag("Item").Length;
        
        if (!Bass.isPlaying && targetRemaining <= 4)
        {
            Bass.enabled = true;
        }
       
        //Debug.Log("target" + targetRemaining);
//        if (targetRemaining <= 4 && (!Bass.isPlaying))
        
       
    }
    public void PickItem()
    {
        MakeSound(PickItemSound);
    }

    public void KnockLimit()
    {
        MakeSound(KnockLimitSound);
    }
    public void Win()
    {
        MakeSound(WinSound);
    }

    public void Lose()
    {
        MakeSound(LoseSound);
    }
    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
   
}
