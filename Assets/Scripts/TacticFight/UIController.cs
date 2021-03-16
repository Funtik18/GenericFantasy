using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("EnemySigns")]
    public GameObject enemySign;
    public Transform enemySignsTP;

    public GameObject AttackInfo;


    public List<EnemySigns> enemies =new List<EnemySigns>();

    private static UIController instance;
    public static UIController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIController>();

            }
            return instance;
        }
    }

    public void DisableSelections()
    {
        foreach (var item in enemies)
        {
            item.Selection.SetActive(false);
            item.enemy.canBeAttacked.SetActive(false);
            TurnManager.Instance.selectedEnemy = null;
            
        }
        AttackInfo.SetActive(false);
    }

    public void Select(EnemySigns enemy)
    {
        DisableSelections();
        AttackInfo.SetActive(!AttackInfo.activeInHierarchy);
        enemy.Selection.SetActive(AttackInfo.activeInHierarchy);
        enemy.enemy.canBeAttacked.SetActive(AttackInfo.activeInHierarchy);
        TurnManager.Instance.selectedEnemy = enemy.enemy.gameObject;
    }

    public void ShowDefaultAttackInfo()
    {
        Select(enemies[0]);
        
    }

    public void ShowAttackInfo(EnemySigns enemy)
    {
        if (TurnManager.Instance.selectedEnemy == null)
        {           
            Select(enemy);
        }

    }

}
