using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groups : MonoBehaviour
{
    float lastFall;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsValidGridPos())
        {
            Debug.Log("Game over!");
            Destroy(gameObject);
            GameObject.FindObjectOfType<GUIManager>().ShowGameover();
        }
        UpdateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!IsValidGridPos())
            {
                transform.position -= new Vector3(-1, 0, 0);
            } 
            else
            {
                UpdateGrid();
            }
        } 
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!IsValidGridPos())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
            else
            {
                UpdateGrid();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
            if (!IsValidGridPos())
            {
                transform.Rotate(0, 0, 90);
            }
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||  Time.time - lastFall >= speed)
        {
            transform.position += new Vector3(0, -1, 0);
            lastFall = Time.time;

            if (!IsValidGridPos())
            {
                transform.position -= new Vector3(0, -1, 0);
                int deletedNum = Grid.DeleteFullRows();
                GameObject.FindObjectOfType<GUIManager>().AddScore(deletedNum);
                GameObject.FindObjectOfType<Spawner>().SpawnNext();
                enabled = false;
            }
            else
            {
                UpdateGrid();
            }
        }
        
    }

    bool IsValidGridPos()
    {
        foreach(Transform child in transform)
        {
            Vector2 pos = Grid.RoundVect3(child.position);
            if (!Grid.InsideBorder(pos))
            {
                return false;
            }
            if (Grid.grid[(int)pos.x, (int)pos.y] != null && Grid.grid[(int)pos.x, (int)pos.y].parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    void UpdateGrid()
    {
        for (int i = 0; i < Grid.w; ++i)
        {
            for (int j = 0; j < Grid.h; ++j)
            {
                if (Grid.grid[i, j] != null && Grid.grid[i, j].parent == transform)
                {
                    Grid.grid[i, j] = null;
                }
            }
        }

        foreach (Transform child in transform)
        {
            Vector2 pos = Grid.RoundVect3(child.position);
            Grid.grid[(int)pos.x, (int)pos.y] = child;
        }
    }
}
