<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] tiles;

    private static TurnManager instance;
    public static TurnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TurnManager>();

            }
            return instance;
        }
    }

    static Dictionary<string, List<TacticMove>> units = new Dictionary<string, List<TacticMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticMove> turnTeam = new Queue<TacticMove>();

    // Start is called before the first frame update
    void Awake()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    public void InitTeamTurnQueue()
    {
        List<TacticMove> teamList = units[turnKey.Peek()];

        foreach (TacticMove item in teamList)
        {
            turnTeam.Enqueue(item);
        }
        StartTurn();
    }

    public void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public void EndTurn()//////Если скипаит ходы бот тупит
    {
        TacticMove unit = turnTeam.Dequeue();
        unit.EndTurn();
        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }

        ClearCal();

    }

    public void ClearCal()///////////ПОЛНЫЙ
    {
        foreach (var item in units)////////КАЛ
        {
            foreach (var item2 in item.Value)
            {
                item2.canBeAttacked.SetActive(false);
            }
        }
    }

    public void AddUnit(TacticMove unit)
    {
        List<TacticMove> list;
        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticMove>();
            units[unit.tag] = list;
            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }
        list.Add(unit);
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] tiles;
    //[HideInInspector]
    public GameObject selectedEnemy;

    private static TurnManager instance;
    public static TurnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TurnManager>();

            }
            return instance;
        }
    }

    static Dictionary<string, List<TacticMove>> units = new Dictionary<string, List<TacticMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticMove> turnTeam = new Queue<TacticMove>();

    // Start is called before the first frame update
    void Awake()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    public void InitTeamTurnQueue()
    {
        List<TacticMove> teamList = units[turnKey.Peek()];
        foreach (TacticMove item in teamList)
        {
            turnTeam.Enqueue(item);
        }
        StartTurn();
    }

    public void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public void EndTurn()//////Если скипаит ходы бот тупит
    {
        TacticMove unit = turnTeam.Dequeue();
        unit.EndTurn();
        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }


    }

    public void AddUnit(TacticMove unit)
    {
        List<TacticMove> list;
        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticMove>();
            units[unit.tag] = list;
            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }
        list.Add(unit);
    }
}
>>>>>>> Develop
