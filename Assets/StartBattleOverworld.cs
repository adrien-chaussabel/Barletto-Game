using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattleOverworld : MonoBehaviour
{
    public GameObject Player;
    public AudioSource OverworldAudio;
    public string battleSceneName;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            OverworldAudio.Pause();
            SceneManager.LoadSceneAsync(battleSceneName, LoadSceneMode.Additive);
            // SceneManager.SetActiveScene(SceneManager.GetSceneByName(battleSceneName));
            gameObject.SetActive(false);
        }
    }
}
