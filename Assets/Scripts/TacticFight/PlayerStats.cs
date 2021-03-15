using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Character
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Statistics.stats.Dexterity.StatValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
