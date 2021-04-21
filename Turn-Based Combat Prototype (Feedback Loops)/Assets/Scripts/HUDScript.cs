using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public Text nameText;
    public Text lvlText;
    public Slider hpSlider;

    public Slider overheatSlider;

    public void SetUP(Unit unit)
    {
        nameText.text = unit.unitName;
        lvlText.text = "Lvl" + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        overheatSlider.maxValue = unit.maxOverHeat;
        overheatSlider.value = unit.currentOverHeat;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetOverHeat(int oh)
    {
        overheatSlider.value = oh;
    }
}
