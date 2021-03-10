using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticMove : MonoBehaviour
{
    public bool turn = false;

    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public GameObject canBeAttacked;

    [Header("Character Prefs")]
    public bool moving = false;
    public int move = 5;
    public float jumpHeight = 2;
    public float moveSpeed = 2;
    public float jumpVelocity = 4.5f;
    public TacticAttack myAttack;

    [Header("JumpState")]
    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    public Tile actualTargetTile;

    protected void Init()
    {
        canBeAttacked.SetActive(false);

        tiles = TurnManager.Instance.tiles;

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        TurnManager.Instance.AddUnit(this);

        GetCurrentTile();
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;

        currentTile.haveCharOnIt = true;
        currentTile.character = this;
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, Vector3.down, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists(float jumpHeight, Tile target)
    {
        //tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(jumpHeight, target);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(jumpHeight, null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;

        while (process.Count > 0)
        {

            Tile t = process.Dequeue();
            if (!t.haveCharOnIt || t.current)
            {
                selectableTiles.Add(t);
                t.selectable = true;
            }
            if (t.distance < move)
            {
                foreach (Tile tile in t.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        if (!t.haveCharOnIt || t.current)
                        {
                            tile.parent = t;
                            tile.visited = true;
                            tile.distance = 1 + t.distance;
                            process.Enqueue(tile);
                        }
                        else
                        {
                            tile.distance = 1 + t.distance;
                        }
                    }
                }

            }
            foreach (Tile tile in t.tilesWithCharsList)
            {
                if (!tile.visited)
                {
                    tile.visited = true;
                    tile.distance = 1 + t.distance;

                }
            }
        }
    }

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }


    }

    public void Move()
    {
        currentTile.haveCharOnIt = false;
        currentTile.character = null;
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                bool jump = transform.position.y != target.y;
                if (jump)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }


                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;
            GetCurrentTile();
            TurnManager.Instance.EndTurn();
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (Tile tile in selectableTiles)
        {
            tile.TileReset();
        }
        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    void Jump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if (jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;

        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2f;
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2f);
        }
    }

    void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;
        if (transform.position.y <= target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            Vector3 p = transform.position;

            p.y = target.y;
            transform.position = p;

            velocity = new Vector3();
        }
    }

    void JumpUpward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;
        if (transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }

    }

    void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5f;
            velocity.y = 1.5f;
        }
    }
    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];

        foreach (Tile t in list)
        {
            if (t.f < lowest.f && !t.haveCharOnIt)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);



        return lowest;
    }

    protected Tile FindEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();

        Tile next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= move)
        {
            return t.parent;
        }

        Tile endTile = null;
        for (int i = 0; i <= move; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

    protected void FindPath(Tile target)
    {
        ComputeAdjacencyLists(jumpHeight, target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);
        //currentTile.parent

        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;
        while (openList.Count > 0)
        {

            Tile t = FindLowestF(openList);

            closedList.Add(t);
            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }
            foreach (Tile item in t.adjacencyList)
            {
                if (closedList.Contains(item))
                {

                }
                else if (openList.Contains(item))
                {
                    float tempG = t.g + Vector3.Distance(item.transform.position, t.transform.position);

                    if (tempG < item.g)
                    {
                        item.parent = t;

                        item.g = tempG;
                        item.f = item.g + item.h;
                    }
                    /*if (item.haveCharOnIt)
                    {
                        item.g = Mathf.Infinity;
                    }*/
                }
                else
                {
                    item.parent = t;

                    item.g = t.g + Vector3.Distance(item.transform.position, t.transform.position);

                    item.h = Vector3.Distance(item.transform.position, target.transform.position);
                    item.f = item.h + item.g;

                    openList.Add(item);

                }
            }
        }

        Debug.LogError("Path not found");
    }

    public virtual void Think()
    {

    }

    public virtual void EndThink()
    {

    }

    public void BeginTurn()
    {
        turn = true;
        FindSelectableTiles();
        Think();
    }

    public void EndTurn()
    {
        turn = false;
        EndThink();
    }

}
