using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TacticAttack : MonoBehaviour
{
    public int range = 1;
    public int hp = 10;

    bool show = false;

    GameObject[] temp;

    public List<TacticMove> unitsToAttack = new List<TacticMove>();
    List<TacticMove> enemySigns = new List<TacticMove>();

    public void CheckAttack()
    {
        //temp = TurnManager.Instance.tiles.Where(t => t.GetComponent<Tile>().distance > 0 && t.GetComponent<Tile>().distance <= range && t.GetComponent<Tile>().haveCharOnIt).ToArray();
        temp = TurnManager.Instance.tiles.Where(t => t.GetComponent<Tile>().haveCharOnIt).ToArray();
        if (temp.Length > 0)
        {
            foreach (var item in temp)
            {
                if ( item.GetComponent<Tile>().distance>0 && item.GetComponent<Tile>().distance<=range)
                {
                    if (!unitsToAttack.Contains(item.GetComponent<Tile>().character))
                    {
                        item.GetComponent<Tile>().canBeAttacked = true;
                        unitsToAttack.Add(item.GetComponent<Tile>().character);
                    }

                }

            }
            TurnManager.Instance.selectedEnemy = temp[0];
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

    public void Attack()
    {
        TacticMove unitToAttack = TurnManager.Instance.selectedEnemy.GetComponent<TacticMove>();
        Debug.Log(unitToAttack);
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
                Debug.Log("damage done " + (d.dices[0].LastValue - 2) + " by " + gameObject.name + " hp remain " + unitToAttack.myAttack.hp);
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
