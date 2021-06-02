using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCardScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI nameText;
    public Text hpText;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpText.text = unit.currentHP + " / " + unit.maxHP.ToString();
    }

    public void setHP(int hp, int maxHP)
    {
        hpSlider.value = hp;
        hpText.text = hp + " / " + maxHP.ToString();
    }
}
