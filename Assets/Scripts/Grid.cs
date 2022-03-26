using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;

    public static Transform[,] grid = new Transform[w, h];

   public static Vector2 RoundVect3(Vector3 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public static bool InsideBorder(Vector2 pos) {
        return (int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0;
    }

    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public static void DeleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y].gameObject);
                grid[x, y] = null;
            }
        }
    }
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public static void DecreaseRowAbove(int y)
    {
        for (int i = y; i < h; ++i)
        {
            DecreaseRow(i);
        }
    }
    public static int DeleteFullRows()
    {
        int deletedNum = 0;
        for (int y = 0; y < h;)
        {
            if (IsRowFull(y))
            {
                ++deletedNum;
                DeleteRow(y);
                DecreaseRowAbove(y + 1);
            } else
            {
                ++y;
            }
        }
        return deletedNum;
    }
}
