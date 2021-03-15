using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TacticAttack : MonoBehaviour
{


    public int range = 1;
    public int hp = 10;
    [HideInInspector]
    public Character myStats;
    public bool dodgedThisTurn = false;

    bool show = false;

    GameObject[] temp;

    public List<TacticMove> unitsToAttack = new List<TacticMove>();
    List<TacticMove> enemySigns = new List<TacticMove>();

    public void CheckAttack()
    {
        unitsToAttack.Clear();
        //temp = TurnManager.Instance.tiles.Where(t => t.GetComponent<Tile>().distance > 0 && t.GetComponent<Tile>().distance <= range && t.GetComponent<Tile>().haveCharOnIt).ToArray();
        temp = TurnManager.Instance.tiles.Where(t => t.GetComponent<Tile>().haveCharOnIt).ToArray();
        if (temp.Length > 0)
        {
            foreach (var item in temp)
            {
                if (item.GetComponent<Tile>().distance > 0 && item.GetComponent<Tile>().distance <= range)
                {
                    if (!unitsToAttack.Contains(item.GetComponent<Tile>().character))
                    {
                        item.GetComponent<Tile>().canBeAttacked = true;
                        unitsToAttack.Add(item.GetComponent<Tile>().character);
                    }

                }

            }
            if (unitsToAttack.Count > 0)
                TurnManager.Instance.selectedEnemy = unitsToAttack[0].gameObject;
        }
    }

    public void ShowUnitsToAttack()
    {
        DeleteShownUnits();
        if (unitsToAttack.Count > 0)
        {
            for (int i = 0; i < unitsToAttack.Count; i++)
            {
                GameObject go = Instantiate(UIController.Instance.enemySign, UIController.Instance.enemySignsTP);
                go.GetComponent<EnemySigns>().enemy = unitsToAttack[i];
                enemySigns.Add(unitsToAttack[i]);
                UIController.Instance.enemies.Add(go.GetComponent<EnemySigns>());
                go.transform.position = new Vector3(go.transform.position.x - 120 * i, go.transform.position.y, go.transform.position.z);
            }
        }

    }

    public void DeleteShownUnits()
    {
        if (UIController.Instance.enemies.Count > 0)
        {
            foreach (var item in UIController.Instance.enemies)
            {
                Destroy(item.gameObject);
            }
            UIController.Instance.enemies.Clear();
            enemySigns.Clear();
        }

    }

    public bool Dodge()
    {
        if (dodgedThisTurn)
            return false;
        DiceFormule d = new DiceFormule(3, DiceType.Cube, 0);
        int t = d.Roll();
        if (t < myStats.myDodge)
        {
            dodgedThisTurn = true;
            return true;
        }
        else
        {
            Debug.Log("dodge failed");
            return false;
        }

    }

    public void Attack()
    {
        TacticMove unitToAttack = null;
        if (TurnManager.Instance.selectedEnemy)
        {
             unitToAttack = TurnManager.Instance.selectedEnemy.GetComponent<TacticMove>();
        }
        if (unitToAttack != null && unitToAttack.tag!=tag)
        {
            DiceFormule d = new DiceFormule(3, DiceType.Cube, -2);
            
            int t = d.Roll();
            d = null;
            if (t < myStats.myDexterity)
            {
                if (!unitToAttack.myAttack.Dodge())
                {
                    Debug.Log("Hit");
                    d = new DiceFormule(1, DiceType.Cube, -2);
                    int damage = d.Roll();
                    if (damage <= 0)
                    {
                        damage = 1;
                    }
                    unitToAttack.myAttack.hp -= damage;
                    Debug.Log("damage done " + (damage) + " by " + gameObject.name + " hp remain " + unitToAttack.myAttack.hp);
                    d = null;
                }
                else
                {
                    Debug.Log("Foe dodged");
                }

            }
            else
            {
                Debug.Log("Miss");
            }
            TurnManager.Instance.EndTurn();
        }
        
    }
}
