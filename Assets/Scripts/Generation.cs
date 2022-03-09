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
            LakeGeneration();
       
    }

    public void LakeGeneration()
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
                {

                    if (i + depth * 3 >= numElements)
                    {
                        for (int j = 0; j < depth * 3; j++)
                        {
                            if (j == 0 || j == 1 || j == 4 || j == 5 || j == 8 || j == 9)
                            {

                                GameObject child1 = sideChild.transform.GetChild(i + j).gameObject;
                                GameObject childFloor = child1.transform.GetChild(0).gameObject;
                                Vector3 position = childFloor.transform.position;
                                Destroy(childFloor);
                                GameObject Lake1 = Instantiate(LakeModules[count], position, Quaternion.identity);
                                Lake1.transform.SetParent(child1.transform);
                                count += 1;

                            }
                        }
                        i += depth * 3 + 1;
                    }
                    else
                    {
                        Vector3 position = childDepth.transform.position;
                        Destroy(childDepth.gameObject);
                        GameObject noLakeDepth = Instantiate(FirstDepthtWithoutLake, position, Quaternion.identity);
                        noLakeDepth.transform.SetParent(sideChild.transform);
                    }

                }
            }

        }
        oneTime = true;
    }
    
}

