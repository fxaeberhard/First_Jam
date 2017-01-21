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
    int targetLeft;
    


    void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple instances of SoundEffects!");
        }
        Instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        targetLeft = GameObject.FindGameObjectsWithTag("Item").Length;
        Bass = GetComponent<AudioSource>();
    }

    void Update()
    {

      if(targetLeft <= 4)
        {
           if (!Bass.isPlaying)
                {
                Bass.Play();
                }
        }
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
