using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TacticAttack : MonoBehaviour
{
    public int range = 1;
    public int hp = 10;

    GameObject[] temp;
    TacticMove unitToAttack;

    public bool CheckAttack()
    {
        temp = TurnManager.Instance.tiles.Where(t => t.GetComponent<Tile>().distance > 0 && t.GetComponent<Tile>().distance <= range && t.GetComponent<Tile>().haveCharOnIt).ToArray();
        if (temp.Length > 0)
        {
            
            foreach (var item in temp)
            {
                if (item.GetComponent<Tile>().character != gameObject.GetComponent<TacticMove>())
                {
                    item.GetComponent<Tile>().character.canBeAttacked.SetActive(true);
                    unitToAttack = item.GetComponent<Tile>().character;
                }

            }
            return true;
        }
        return false;
    }

    public void Attack()
    {
        if (unitToAttack != null)
        {
            Debug.Log("Attack");
            
            DiceFormule d = new DiceFormule(3, DiceType.Cube, -2);
            int t = d.Roll();
            Debug.Log("Dices");
            foreach (var item in d.dices)
            {
                Debug.Log(item.LastValue);
            }
            if (t < 8)
            {
                Debug.Log("Hit");
                d = null;
                d = new DiceFormule(1, DiceType.Cube, -2);               
                unitToAttack.myAttack.hp -= d.Roll();
                Debug.Log("damage done " + (d.dices[0].LastValue-2)+" by "+gameObject.name + " hp remain " + unitToAttack.myAttack.hp);
                d = null;
            }
            else
            {
                Debug.Log("Miss");
            }
            Debug.Log("------------------------------------------");
            TurnManager.Instance.EndTurn();
        }
        unitToAttack = null;
    }
}
