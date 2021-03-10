<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : TacticMove
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward);
        if (!turn)
        {
            return;
        }
        if (!moving)
        {
            FindSelectableTiles();
            CheckClick();
        }
        else
        {
            TurnManager.Instance.ClearCal();
            Move();        
        }
        myAttack.CheckAttack();
    }

    void CheckClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                   
                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : TacticMove
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward);
        if (!turn)
        {
            return;
        }
        if (!moving)
        {
            CheckClick();
        }
        else
        {
            Move();
            
        }
        
        
    }

    void CheckClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                   
                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }

    public override void Think()
    {
        UIController.Instance.DisableSelections();
        myAttack.CheckAttack();
        myAttack.ShowUnitsToAttack();
    }

    public override void EndThink()
    {
        myAttack.DeleteShownUnits();
    }

}
>>>>>>> Develop
