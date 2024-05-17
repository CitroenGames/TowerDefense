using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int Currency = 0;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        Currency = 100;
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
    }

    public bool RemoveCurrency(int amount)
    {
        if (amount <= Currency)
        {
            Currency -= amount;
            return true;
        }
        else 
        { 
            Debug.Log("Not enough currency"); 
            return false;
        }
    }
}
