using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleScript : MonoBehaviour
{
    public GameObject playerSprite;
    public GameObject enemySprite;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;

    Unit playerUnit;
    public Unit enemyUnit;

    public TMPro.TextMeshProUGUI dialogue;

    public UnitCardScript playerCard;
    public UnitCardScript enemyCard;

    public Sprite playerIdleSprite;
    public Sprite playerAttack1Sprite;
    public Sprite playerAttack2Sprite;

    GameObject PlayerGO;
    GameObject EnemyGO;

    public Button attackBtn1;
    public Button attackBtn2;

    public Scene overworldScene;
    public string battleSceneName;

    public AudioSource battleMusicSource;
    public AudioClip victorySong;
    public bool isFinalBattle = false;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(battleSceneName));
        StartCoroutine(SetupBattle());
    }

    void Update()
    {

        if ((Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) && state == BattleState.WON)
        {
            if(isFinalBattle)
            {
                SceneManager.LoadScene("Credits");
            }
            GameObject.Find("OverworldAudio").GetComponent<AudioSource>().Play();
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }


    IEnumerator SetupBattle()
    {
        PlayerGO = Instantiate(playerSprite, playerBattleStation);
        playerUnit = PlayerGO.GetComponent<Unit>();
        EnemyGO = Instantiate(enemySprite, enemyBattleStation);
        enemyUnit = EnemyGO.GetComponent<Unit>();

        attackBtn1.gameObject.SetActive(false);
        attackBtn2.gameObject.SetActive(false);

        playerIdleSprite = PlayerGO.GetComponentInChildren<SpriteRenderer>().sprite;

        dialogue.text = "You encountered a(n) " + enemyUnit.unitName + "! You must defeat it to continue.";

        playerCard.SetHUD(playerUnit);
        enemyCard.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerPunch()
    {
        PlayerGO.GetComponentInChildren<SpriteRenderer>().sprite = playerAttack1Sprite;

        yield return new WaitForSeconds(2f);

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage1);
        enemyCard.setHP(enemyUnit.currentHP, enemyUnit.maxHP);

        yield return new WaitForSeconds(1f);

        PlayerGO.GetComponentInChildren<SpriteRenderer>().sprite = playerIdleSprite;

        checkDead(isDead);
    }

    IEnumerator PlayerBow()
    {
        PlayerGO.GetComponentInChildren<SpriteRenderer>().sprite = playerAttack2Sprite;

        yield return new WaitForSeconds(2f);

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage2);
        enemyCard.setHP(enemyUnit.currentHP, enemyUnit.maxHP);

        yield return new WaitForSeconds(1f);

        PlayerGO.GetComponentInChildren<SpriteRenderer>().sprite = playerIdleSprite;

        checkDead(isDead);
    }

    void checkDead(bool isDead)
    {
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            attackBtn1.gameObject.SetActive(false);
            attackBtn2.gameObject.SetActive(false);
            StartCoroutine(EnemyTurn());
        }
    }

    void EndBattle()
    {
        attackBtn1.gameObject.SetActive(false);
        attackBtn2.gameObject.SetActive(false);

        if (state == BattleState.WON)
        {
            dialogue.text = "You deafted " + enemyUnit.unitName + "!";
            battleMusicSource.clip = victorySong;
            battleMusicSource.loop = false;
            battleMusicSource.Play();
        } else
        {
            dialogue.text = "You were defeated by " + enemyUnit.unitName + ".";
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogue.text = enemyUnit.unitName + " is attacking";

        yield return new WaitForSeconds(.8f);

        float rng = Random.Range(0.0f, 1.0f);

        bool isDead = playerUnit.TakeDamage(rng <= .85 ? enemyUnit.damage1 : enemyUnit.damage2);

        dialogue.text = enemyUnit.unitName + " used a " + (rng <= .85 ? "weak attack" : "strong attack") + "!";

        yield return new WaitForSeconds(2f);

        playerCard.setHP(playerUnit.currentHP, playerUnit.maxHP);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void PlayerTurn()
    {
        attackBtn1.gameObject.SetActive(true);
        attackBtn2.gameObject.SetActive(true);
        dialogue.text = "Choose an action:";
        // show buttons maybe
    }

    public void OnPunchButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerPunch());
    }                                               

    public void OnBowButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerBow());
    }


}
