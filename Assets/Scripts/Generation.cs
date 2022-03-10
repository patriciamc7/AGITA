using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public int numElements;
    public int depth;
    public GameObject[] modules;
    public GameObject[] LakeModules;
    public GameObject FirstDepthtWithoutLake;
    public bool oneTime = false;

    void Start()
    {
        GameObject left = this.transform.GetChild(0).gameObject;
        GameObject right = this.transform.GetChild(1).gameObject;

        for (int i = 0; i < numElements; i++)
       {
            for (int j = 0; j < depth; j++)
            {
                Vector3 rightPosition = new Vector3(2 * i, 0, -1 - 2 * j);
                Vector3 leftPosition = new Vector3(2 * i, 0, 1 + 2 * j);

                modules[j].GetComponent<Procedural>().isLeft = false;
                GameObject rightDepth = Instantiate(modules[j], rightPosition, Quaternion.identity);
                rightDepth.transform.SetParent(right.transform);
                modules[j].GetComponent<Procedural>().isLeft = true;
                GameObject leftObject = Instantiate(modules[j], leftPosition, Quaternion.identity);
                leftObject.transform.SetParent(left.transform);

            }
        }
        
    }
    void Update()
    {
        if(!oneTime)
            noRepetition();
       
    }

    public void overwriteFloor(Transform depth, Transform side, bool isLake)
    {
        GameObject aux = null;
        Vector3 position = depth.transform.position;
        Destroy(depth.gameObject);
        if (isLake)
            aux = FirstDepthtWithoutLake;
        else
            aux = modules[0];
        GameObject noLakeDepth = Instantiate(aux, position, Quaternion.identity);
        noLakeDepth.transform.SetParent(side.transform);
    }
    public int LakeGeneration(Transform depth, Transform side, int i, int count, int actDepth)
    {
        if (i + actDepth * 3 <= numElements)
        {
            for (int j = 0; j < actDepth * 3; j++)
            {
                if (j == 0 || j == 1 || j == 4 || j == 5 || j == 8 || j == 9)
                {

                    GameObject childDepths = side.transform.GetChild(i + j).gameObject;
                    GameObject childFloor = childDepths.transform.GetChild(0).gameObject;
                    Vector3 position = childFloor.transform.position;
                    Destroy(childFloor);
                    GameObject Lake1 = Instantiate(LakeModules[count], position, Quaternion.identity);
                    Lake1.transform.SetParent(childDepths.transform);
                    count += 1;

                }
            }
            i += actDepth * 3 + 1;
            if (side.transform.GetChild(i).transform.GetChild(0).CompareTag("FloorLake"))
                overwriteFloor(depth, side,true);

        }
        else
            overwriteFloor(depth, side,true);
        return i;
    }
    public void noRepetition()
    {
        int numChilds = this.transform.GetChildCount();
        int count = 0;
        for (int l = 0; l < numChilds; l++)
        {
            Transform sideChild = this.transform.GetChild(l);
            int sideNumChild = sideChild.transform.GetChildCount();

            for (int i = 0; i < sideNumChild; i++)
            {
                count = 0;
                Transform childDepth = sideChild.transform.GetChild(i);
                GameObject childModule = childDepth.transform.GetChild(0).gameObject;
                if (childModule.CompareTag("FloorLake"))
                    i = LakeGeneration(childDepth, sideChild, i, count, depth);
                if (childDepth.CompareTag("FirstDepth") && childModule.tag.Equals(sideChild.transform.GetChild(i + 1).GetChild(0).tag) && !childModule.CompareTag("Floor"))
                    overwriteFloor(sideChild.transform.GetChild(i + 1).GetChild(0), sideChild,false);
            }

        }
        oneTime = true;
    }
    
}

