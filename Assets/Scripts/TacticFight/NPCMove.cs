using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticMove
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        if (!turn)
        {
            return;
        }
        if (!moving)
        {

        }
        else
        {
            Move();
        }
    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);

        FindPath(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject item in targets)
        {
            float d = Vector3.Distance(transform.position, item.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = item;
            }
        }

        target = nearest;
    }

    public override void Think()
    {
        FindNearestTarget();
        CalculatePath();
        FindSelectableTiles();
        actualTargetTile.target = true;
    }

    public override void EndThink()
    {

    }
}
