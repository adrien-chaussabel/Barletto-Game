using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToDungeon : MonoBehaviour
{

    public Transform player;
    public Transform teleportLoc;
    public AudioSource audioSource;
    public AudioClip switchAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.position, gameObject.transform.position) < .1)
        {
            player.position = teleportLoc.position;
            audioSource.clip = switchAudio;
            audioSource.Play();
        }
    }
}
