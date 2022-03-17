using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public int numElements;
    public int depth;
    public GameObject[] modules;
    public GameObject LakeModule;
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

    public void overwriteFloor(Transform depth, Transform side)
    {
        Vector3 position = depth.transform.position;
        Quaternion rotation = Quaternion.identity;
        Destroy(depth.gameObject);
        if (side.name.Equals("Left"))
        {
            rotation = Quaternion.Euler(Vector3.up * 180);
        }
        GameObject noRepetitionModule= Instantiate(FirstDepthtWithoutLake, position, rotation);
        noRepetitionModule.name = "NoRepetition";
        noRepetitionModule.transform.SetParent(side.transform);
    }
    public int LakeGeneration(Transform depthTransform, Transform side, int i)
    {
        if (i + depth * 3 <= numElements)
        {
            for (int j = 0; j < depth * 3; j++)
            {
                if (j == 0 || j == 1 || j == 4 || j == 5 || j == 8 || j == 9)
                {

                    GameObject childDepths = side.transform.GetChild(i + j).gameObject;
                    GameObject childFloor = childDepths.transform.GetChild(0).gameObject;

                    if (j == 0)
                    {
                        Vector3 position = childFloor.transform.position;
                        Quaternion rotation = Quaternion.identity;
                        if (side.name.Equals("Left"))
                        {
                            rotation = Quaternion.Euler(Vector3.up * 180);
                            position += new Vector3(4, 0, 0);
                        }
                        GameObject Lake1 = Instantiate(LakeModule, position, rotation);
                        Lake1.transform.SetParent(childDepths.transform);
                    }
                    Destroy(childFloor);

                }
            }
            i += depth * 3;
            if (side.transform.GetChild(i).transform.GetChild(0).CompareTag("FloorLake"))
                overwriteFloor(depthTransform, side);
        }
        else
            overwriteFloor(depthTransform, side);
        return i;
    }
    public void noRepetition()
    {
       int numChilds = this.transform.GetChildCount();
       for (int l = 0; l < numChilds; l++)
       {
            Transform sideChild = this.transform.GetChild(l);
            int sideNumChild = sideChild.transform.GetChildCount();

            for (int i = 0; i < sideNumChild; i++)
            {
                Transform childDepth = sideChild.transform.GetChild(i);
                GameObject childModule = childDepth.transform.GetChild(0).gameObject;
                if (childModule.CompareTag("FloorLake"))
                    i = LakeGeneration(childDepth, sideChild, i);
                //else if (i + depth < sideNumChild && childDepth.CompareTag("FirstDepth") && childModule.tag.Equals(sideChild.transform.GetChild(i + depth).GetChild(0).tag))
                //    overwriteFloor(sideChild.transform.GetChild(i + depth), sideChild,false);
            }

        }
        oneTime = true;
    }
    
}

