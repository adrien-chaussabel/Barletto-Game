using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName;
    public int maxHP;
    public int currentHP;

    public int damage1;
    public int damage2;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        // return true if dead, false if not
        if(currentHP <= 0)
        {
            return true;
        }
        return false;
    }
}
