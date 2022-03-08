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
    void Start()
    {
       for (int i = 0; i < numElements; i++)
       {
            for (int j = 0; j < depth; j++)
            {
                Vector3 rightPosition = new Vector3(2 * i, 0, -1 - 2 * j);
                Vector3 leftPosition = new Vector3(2 * i, 0, 1 + 2 * j);

                //modules[j].GetComponent<Procedural>().isLeft = false;
                GameObject rightDepth = Instantiate(modules[j], rightPosition, Quaternion.identity);
                rightDepth.transform.SetParent(this.transform);
                
            }
        //modules[j].GetComponent<Procedural>().isLeft = true;
        //GameObject leftObject = Instantiate(modules[j], leftPosition, Quaternion.identity);
        //this.transform.SetParent(leftObject.transform);
        //if (isLake)
        //{

        //    lakeElement = i;
        //    lakeDepth = j;
        //    j += 2;
        //    i += 3;
        //    //for (int k = 0; k < LakeModules.Length; k++)
        //    //{
        //    //    GameObject rightObject  = Instantiate(LakeModules[k], rightPosition, Quaternion.identity);

        //    //}
        //}
    

        }
    }
    void Update()
    {
        int numChilds = this.transform.GetChildCount();
        int count = 0;

        for (int i = 0; i < numChilds; i++)
        {
            count = 0;
            Transform childDepth = this.transform.GetChild(i);
            if (childDepth)
            {
                GameObject childModule = childDepth.transform.GetChild(0).gameObject;
                if (childModule)
                {
                    if (childModule.CompareTag("FloorLake"))
                    {
                        for (int j = 0; j < depth*3; j++)
                        {
                            if(j==0|| j == 1|| j == 4||j == 5 || j == 8 || j == 9)
                            {
                                if(i+ depth*3 >= numElements)
                                {
                                    GameObject child1 = this.transform.GetChild(i + j).gameObject;
                                    GameObject childFloor = child1.transform.GetChild(0).gameObject;
                                    Vector3 position = childFloor.transform.position;
                                    Destroy(childFloor);
                                    GameObject Lake1 = Instantiate(LakeModules[count], position, Quaternion.identity);
                                    Lake1.transform.SetParent(child1.transform);
                                    count += 1;
                                }
                                else
                                {
                                    Vector3 position = childDepth.transform.position;
                                    Destroy(childDepth);
                                    GameObject rightDepth = Instantiate(FirstDepthtWithoutLake, position, Quaternion.identity);
                                }


                            }
                        }
                        i += depth * 3+1;
                    }
                }
            }

        }
    }
 
}

