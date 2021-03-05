using System.Collections;
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
    void Update()
    {
        if (!moving)
        {
            FindSelectableTiles();
            CheckClick();
        }
        else
        {

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
                        t.target = true;
                        moving = true;
                    }
                }
            }
        }
    }
}
