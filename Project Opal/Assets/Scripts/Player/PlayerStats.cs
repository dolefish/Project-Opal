using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    #region Singleton
    public static PlayerStats singleton;
    void Awake()
    {
        singleton = this;
    }
    #endregion

    [Header("Stats")]
    public Stat charisma;
    public Stat intellect;
    public Stat love;

    [Header("Energy")]
    public float energy = 100f;
    public float depletion = 0.1f;


    public void DepleteEnergy()
    {
        if (energy > depletion)
        {
            energy -= depletion;

        }
    }

}

[System.Serializable]
public class Stat
{
    public int level;
    public int maxExp;
    public int currentExp;

    public Sprite statIcon;

    public bool AddExp(int amount)
    {
        if (currentExp + amount > maxExp)
        {
            level++;
            //Work out new maxExp
            //LevelUp UI
            return false;
        }

        currentExp += amount;
        UpdateUIBar();
        return true;
    }


    public void UpdateUIBar()
    {
            
    }

}
