using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Stats
{
    
    private int _health;
    private int _damage;
    private int _defense;

    public Stats(int health, int damage, int defense)
    {
        _health = health;
        _damage = damage;
        _defense = defense;
    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if(value <= 0)
            {
                this._health = 0;
                if(this._health == 0)
                {
                    Die();
                }
            }
            else
            {
                this._health = value;
            }
        }
    }

    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            if(value <= 0)
            {
                this._damage = 0;
            }
            else
            {
                this._damage = value;
            }
        }
    }

    public int Defense
    {
        get
        {
            return _defense;
        }
        set
        {
            if(value <= 0)
            {
                this._defense = 0;
            }
            else
            {
                this._defense = value;
            }
        }
    }

    private void Die()
    {
        Debug.Log("Died!");
    }

}