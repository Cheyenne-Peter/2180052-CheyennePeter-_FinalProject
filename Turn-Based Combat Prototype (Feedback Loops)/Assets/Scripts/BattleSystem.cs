using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum SystemState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    Unit playerUnit;
    Unit enemyUnit;

    public HUDScript playerHUD;
    public HUDScript enemyHUD;

    public Text StateChangeText;

    public int clickcounterHigh;
    public int clickcounterMedium;
    public int buttonCheck;
    public int randomEnemyAttack;

    public Button mediumButton;
    public Button highButton;

    public bool isDefending;
    public bool isOverHeating;
    public bool enemyisDefending;

    public GameObject defendBubble;
    public GameObject enemyDefendBubble;
    //public GameObject howtoPanel;

    public ParticleSystem PlayerDamage;
    public ParticleSystem EnemyDamage;
    public ParticleSystem HealEffect;
    public ParticleSystem PlayerShoot;
    public ParticleSystem EnemyShoot;
    public ParticleSystem EnemySpawn;
    public ParticleSystem EnemyHealEffect;
    Scene currentScene;

    public SystemState state;
    // Start is called before the first frame update
    void Start()
    {
        state = SystemState.START;
        enemy.SetActive(true);
        player.SetActive(true);
        clickcounterHigh = 5;
        clickcounterMedium = 7;
        buttonCheck = 0;
        currentScene = SceneManager.GetActiveScene();
        StartCoroutine(BattleSetup());
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("QUIIIIT");
        }
    }

    IEnumerator BattleSetup()
    {
        if (currentScene.name == "Stage1")
        {
           /* howtoPanel.SetActive(true);
            yield return new WaitForSeconds(8f);
            howtoPanel.SetActive(false); */


            EnemySpawn.Play();
            StateChangeText.text = "Look Over There!!";
            yield return new WaitForSeconds(1.5f);

            GameObject playerGO = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
            playerUnit = playerGO.GetComponent<Unit>();

            GameObject enemyGO = Instantiate(enemy, enemySpawnPoint.position, Quaternion.identity);
            enemyUnit = enemyGO.GetComponent<Unit>();

            StateChangeText.text = " An " + enemyUnit.unitName + " has appeared! ";

            playerHUD.SetUP(playerUnit);
            enemyHUD.SetUP(enemyUnit);

            yield return new WaitForSeconds(2f);

            state = SystemState.PLAYERTURN;
            PlayerTurn();
        }
        else if (currentScene.name == "Stage2")
        {
           /* howtoPanel.SetActive(true);
            yield return new WaitForSeconds(8f);
            howtoPanel.SetActive(false); */

            EnemySpawn.Play();
            StateChangeText.text = "You Have Unlocked A New Ability!";
            yield return new WaitForSeconds(1.5f);

            GameObject playerGO = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
            playerUnit = playerGO.GetComponent<Unit>();

            GameObject enemyGO = Instantiate(enemy, enemySpawnPoint.position, Quaternion.identity);
            enemyUnit = enemyGO.GetComponent<Unit>();

            StateChangeText.text = " Oh no! The " + enemyUnit.unitName + " is back! ";

            playerHUD.SetUP(playerUnit);
            enemyHUD.SetUP(enemyUnit);

            yield return new WaitForSeconds(2f);

            state = SystemState.PLAYERTURN;
            PlayerTurn();
        }
        else if (currentScene.name == "Stage3")
        {
           /* howtoPanel.SetActive(true);
            yield return new WaitForSeconds(8f);
            howtoPanel.SetActive(false); */

            EnemySpawn.Play();
            StateChangeText.text = "Look! You unlocked even more abilities";
            yield return new WaitForSeconds(1.5f);

            GameObject playerGO = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
            playerUnit = playerGO.GetComponent<Unit>();

            GameObject enemyGO = Instantiate(enemy, enemySpawnPoint.position, Quaternion.identity);
            enemyUnit = enemyGO.GetComponent<Unit>();

            StateChangeText.text = " Seriously??? The " + enemyUnit.unitName + " is back again!? ";

            playerHUD.SetUP(playerUnit);
            enemyHUD.SetUP(enemyUnit);

            yield return new WaitForSeconds(2f);

            state = SystemState.PLAYERTURN;
            PlayerTurn();
        }
        
    }

    IEnumerator PlayerAttackLow()
    {
        // Damage the enemy
        PlayerShoot.Play();
        yield return new WaitForSeconds(1f);

        bool isDead = enemyUnit.TakeDamage(playerUnit.lowDamage);
        bool overHeated = playerUnit.IncreaseOH(playerUnit.lowOH);
        //overheatCheck++;
        //Debug.Log(overheatCheck);

        EnemyDamage.Play();
        enemyHUD.SetHP(enemyUnit.currentHP);
        playerHUD.SetOverHeat(playerUnit.currentOverHeat);
        StateChangeText.text = "Successful Low attack";

        yield return new WaitForSeconds(2f);

        // Check if the enemy is dead

        if (isDead)
        {
            state = SystemState.WON;
            StartCoroutine(EndGame());
        }  
        else
        {
            state = SystemState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        if (overHeated)
        {
            isOverHeating = true;
            StateChangeText.text = "Oh No! You are overheating!";
            yield return new WaitForSeconds(2f);
            state = SystemState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            Debug.Log("OverHeat");

        }
        // Check state based on what happeded
    }

    IEnumerator PlayerAttackMedium()
    {
        // Damage the enemy
        PlayerShoot.Play();
        yield return new WaitForSeconds(1f);

        bool isDead = enemyUnit.TakeDamage(playerUnit.mediumDamage);
        bool overHeated = playerUnit.IncreaseOH(playerUnit.mediumOH);

        EnemyDamage.Play();

        enemyHUD.SetHP(enemyUnit.currentHP);
        playerHUD.SetOverHeat(playerUnit.currentOverHeat);
        StateChangeText.text = "Successful Medium attack";

        yield return new WaitForSeconds(2f);

        // Check if the enemy is dead

        if (isDead)
        {
            state = SystemState.WON;
            StartCoroutine(EndGame());
        }
        else
        {
            state = SystemState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        if (overHeated)
        {
            isOverHeating = true;
            StateChangeText.text = "Oh No! You are overheating!";
            yield return new WaitForSeconds(2f);
            state = SystemState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            Debug.Log("OverHeat");

        }
        // Check state based on what happeded
    }

    IEnumerator PlayerAttackHigh()
    {
        // Damage the enemy
        PlayerShoot.Play();
        yield return new WaitForSeconds(1f);

        bool isDead = enemyUnit.TakeDamage(playerUnit.highDamage);
        bool overHeated = playerUnit.IncreaseOH(playerUnit.highOH);

        EnemyDamage.Play();

        enemyHUD.SetHP(enemyUnit.currentHP);
        playerHUD.SetOverHeat(playerUnit.currentOverHeat);
        StateChangeText.text = "Successful High attack";

        yield return new WaitForSeconds(2f);

        // Check if the enemy is dead

        if (isDead)
        {
            state = SystemState.WON;
            StartCoroutine(EndGame());
        }
        else
        {
            state = SystemState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        if (overHeated)
        {
            isOverHeating = true;
            StateChangeText.text = "Oh No! You are overheating!";
            yield return new WaitForSeconds(2f);
            state = SystemState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            Debug.Log("OverHeat");

        }
        // Check state based on what happeded
    }

    IEnumerator EnemyTurn()
    {
        enemyisDefending = false;
        yield return new WaitForSeconds(0f);
        enemyDefendBubble.SetActive(false);
        randomEnemyAttack = Random.Range(1, 4);
        if (currentScene.name == "Stage1")
        {
            if (randomEnemyAttack == 1)
            {
                Debug.Log("Enemy Attacking");
                StartCoroutine(EnemyAttacking());

            }
            if (randomEnemyAttack == 2)
            {
                Debug.Log("Enemy Defending");
                StartCoroutine(EnemyDefend());
            }
            if (randomEnemyAttack == 3)
            {
                Debug.Log("Enemy Attacking");
                StartCoroutine(EnemyAttacking());
            }
        }
        else if (currentScene.name == "Stage2")
        {
            if (randomEnemyAttack == 1)
            {
                Debug.Log("Enemy Attacking");
                StartCoroutine(EnemyAttacking());
            }
            if (randomEnemyAttack == 2)
            {
                Debug.Log("Enemy Defending");
                StartCoroutine(EnemyDefend());
            }
            if (randomEnemyAttack == 3)
            {
                Debug.Log("Enemy Healing");
                StartCoroutine(EnemyHeal());
            }
        }
        else if (currentScene.name == "Stage3")
        {
            if (randomEnemyAttack == 1)
            {
                Debug.Log("Enemy Attacking");
                StartCoroutine(EnemyAttacking());
            }
            if (randomEnemyAttack == 2)
            {
                Debug.Log("Enemy Defending");
                StartCoroutine(EnemyDefend());
            }
            if (randomEnemyAttack == 3)
            {
                Debug.Log("Enemy Healing");
                StartCoroutine(EnemyHeal());
            }
        }
    }

    IEnumerator EndGame()
    {
        if (state == SystemState.WON)
        {
            if (currentScene.name == "Stage3")
            {
                //Add button to replay
                Debug.Log("Back to beginning");
                StateChangeText.text = "You defeated all the enemies!";
                enemy.SetActive(false);
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("EndGame");
            }
            else
            {
                Debug.Log("WONNNNNN");
                StateChangeText.text = "You won!";
                enemy.SetActive(false);
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }       
        }
        else if (state == SystemState.LOST)
        {
            StateChangeText.text = "You Lost!";
            player.SetActive(false);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void PlayerTurn()
    {
        isDefending = false;
        defendBubble.SetActive(false);
        StateChangeText.text = "Choose an Action:";

        if (isOverHeating)
        {
            state = SystemState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            isOverHeating = false;
            playerHUD.SetOverHeat(playerUnit.currentOverHeat = 0);
        } 
       
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(playerUnit.playerHeal);
        playerUnit.CoolDown(playerUnit.decreaseOH);
        Debug.Log(playerUnit.playerHeal);
        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetOverHeat(playerUnit.currentOverHeat);
        HealEffect.Play();
        StateChangeText.text = "Ahh more Health!";

        yield return new WaitForSeconds(2f);

        state = SystemState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyHeal()
    {
        enemyUnit.Heal(enemyUnit.enemyHeal);
        Debug.Log(enemyUnit.enemyHeal);
        enemyHUD.SetHP(enemyUnit.currentHP);
        EnemyHealEffect.Play();
        //HealEffect.Play();
        StateChangeText.text = "The enemy is healing!";

        yield return new WaitForSeconds(2f);

        state = SystemState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator EnemyAttacking()
    {
        
        StateChangeText.text = enemyUnit.unitName + " attacks! ";
        EnemyShoot.Play();

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.enemydamage);
        Debug.Log(enemyUnit.enemydamage);

        PlayerDamage.Play();
        //Instantiate(PlayerDamage, playerSpawnPoint.position, Quaternion.identity);

        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);


        if (isDead)
        {
            state = SystemState.LOST;
            StartCoroutine(EndGame());
        }
        else
        {
            state = SystemState.PLAYERTURN;
            PlayerTurn();

        }

    }

    IEnumerator PlayerDefend()
    {
        playerUnit.CoolDown(playerUnit.decreaseOH);
        playerHUD.SetOverHeat(playerUnit.currentOverHeat);
        isDefending = true;
        defendBubble.SetActive(true);
        StateChangeText.text = "Nice Defence!";
        yield return new WaitForSeconds(2f);

        state = SystemState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    } 
    IEnumerator EnemyDefend()
    {
        enemyisDefending = true;
        Debug.Log("Enemy Is Defending");
        enemyDefendBubble.SetActive(true);
        StateChangeText.text = "The Enemy is defending";
        yield return new WaitForSeconds(2f);

        state = SystemState.PLAYERTURN;
        PlayerTurn();

    }

    public void PlayerDmg()
    {
        if (enemyisDefending && buttonCheck == 1)
        {
            enemyUnit.currentHP -= 2;   
        }
        else
        {
            enemyUnit.currentHP -= playerUnit.lowDamage;
        }
        if (enemyisDefending && buttonCheck == 2)
        {
            enemyUnit.currentHP -= 2;
        }
        else
        {
            enemyUnit.currentHP -= playerUnit.mediumDamage;
        }
        if (enemyisDefending && buttonCheck == 3)
        {
            enemyUnit.currentHP -= 2;
        }
        else
        {
            enemyUnit.currentHP -= playerUnit.highDamage;
        }

    }

    public void EnemyDmg()
    {
        if (isDefending)
        {
            playerUnit.currentHP -= 2;
            Debug.Log("Player Defends" + enemyUnit.enemydamage);
        }
        else
        {
            playerUnit.currentHP -= enemyUnit.enemydamage;
        }
    }

    public void AttackButtonHigh()
    {
        buttonCheck = 3;
        if (state != SystemState.PLAYERTURN)
            return;

        clickcounterHigh--;
        if (clickcounterHigh == 0)
        {
            highButton.enabled = false;
            highButton.image.color = new Color(1, 1, 1, 0);
        }
        if (clickcounterHigh == 1)
        {
            highButton.image.color = new Color(1, 1, 1, 0.2f);
        }
        if (clickcounterHigh == 2)
        {
            highButton.image.color = new Color(1, 1, 1, 0.4f);
        }
        if (clickcounterHigh == 3)
        {
            highButton.image.color = new Color(1, 1, 1, 0.6f);
        }
        if (clickcounterHigh == 4)
        {
            highButton.image.color = new Color(1, 1, 1, 0.8f);
        }
        if (clickcounterHigh == 5)
        {
            highButton.image.color = new Color(1, 1, 1, 1);
        }

        StartCoroutine(PlayerAttackHigh());
    }

    public void AttackButtonLow()
    {
        buttonCheck = 1;
        if (state != SystemState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttackLow());
    }

    public void AttackButtonMedium()
    {
        buttonCheck = 2;
        if (state != SystemState.PLAYERTURN)
            return;

        clickcounterMedium --;
        if (clickcounterMedium == 0)
        {
            mediumButton.enabled = false;
            mediumButton.image.color = new Color(1, 1, 1, 0);
        }
        if (clickcounterMedium == 1)
        {
            mediumButton.image.color = new Color(1, 1, 1, 0.113f);
        }
        if (clickcounterMedium == 2)
        {
            mediumButton.image.color = new Color(1, 1, 1, 0.286f);
        }
        if (clickcounterMedium == 3)
        {
            mediumButton.image.color = new Color(1, 1, 1, 0.428f);
        }
        if (clickcounterMedium == 4)
        {
            mediumButton.image.color = new Color(1, 1, 1, 0.571f);
        }
        if (clickcounterMedium == 5)
        {
            mediumButton.image.color = new Color(1, 1, 1, 0.714f);
        }
        if (clickcounterMedium == 6)
        {
            mediumButton.image.color = new Color(1, 1, 1, 0.857f);
        }
        if (clickcounterMedium == 7)
        {
            mediumButton.image.color = new Color(1, 1, 1, 1);
        }

        StartCoroutine(PlayerAttackMedium());
    }

    public void HealButton()
    {
        if (state != SystemState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    public void DefendButton()
    {
        if (state != SystemState.PLAYERTURN)
            return;

        StartCoroutine(PlayerDefend());

        Debug.Log("Defending!!");
    }




}
