using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;
    Transform nextWrap;
    int nextIndex = -1;
    GUIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindObjectOfType<GUIManager>();
        nextWrap = GameObject.Find("NextBlock").transform;
        SpawnNext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNext()
    {
        int index;
        if (nextIndex >= 0)
        {
            index = nextIndex;
        }
        else
        {
            index = Random.Range(0, groups.Length);
        }
        GameObject obj = Instantiate(groups[index], transform.position, Quaternion.identity);
        obj.GetComponent<Groups>().speed = uiManager.GetGameSpeed();

        nextIndex = Random.Range(0, groups.Length);
        if (nextWrap.childCount > 0)
        {
            Destroy(nextWrap.GetChild(0).gameObject);
        }
        obj = Instantiate(groups[nextIndex], nextWrap);
        obj.GetComponent<Groups>().enabled = false;
    }
}
