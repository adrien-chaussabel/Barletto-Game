using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleMusicChange : MonoBehaviour
{
    public Unit bossUnit;
    public AudioSource audioSrc;
    public AudioClip transition;
    public AudioClip track2;

    bool below50 = false;
    bool transitionDone = false;
    // Start is called before the first frame update
    void Start()
    {
        bossUnit = GetComponent<BattleScript>().enemyUnit;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossUnit.currentHP <= bossUnit.maxHP * .5 && !below50)
        {
            below50 = true;
            audioSrc.clip = transition;
            audioSrc.loop = false;
            audioSrc.Play();
        }

        if(below50 && !audioSrc.isPlaying && !transitionDone)
        {
            transitionDone = true;
            audioSrc.clip = track2;
            audioSrc.loop = true;
            audioSrc.Play();
        }
    }
}
