using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int enemydamage;
    public int playerHeal;
    public int enemyHeal;
    public int lowDamage;
    public int mediumDamage;
    public int highDamage;

    public int highOH;
    public int mediumOH;
    public int lowOH;

    public int maxHP;
    public int currentHP;

    public int maxOverHeat;
    public int currentOverHeat;

    public int minValueEnemy;
    public int maxValueEnemy;

    public int decreaseOH;

    public void Update()
    {
        enemydamage = Random.Range(minValueEnemy, maxValueEnemy);
        playerHeal = Random.Range(10, 16);
        enemyHeal = Random.Range(10, 13);
        decreaseOH = 10;
    }

    public bool TakeDamage(int dam)
    {
        currentHP -= dam;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool IncreaseOH(int OH)
    {
        currentOverHeat += OH;

        if (currentOverHeat >= 50)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void CoolDown(int amount)
    {
        currentOverHeat -= amount;
        if (currentOverHeat > maxOverHeat)
            currentOverHeat = maxOverHeat;
    }
}
