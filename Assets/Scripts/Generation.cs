using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public int numElements;
    public int depth;
    public GameObject[] modules;
    public GameObject[] LakeModules;
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
                //Debug.Log(rightDepth.transform.Find("FloorLake(Clone)"));

                //int numchild = rightDepth.transform.GetChildCount();
                //Debug.Log(numchild);

                //Transform child = rightDepth.transform.GetChild(numchild);
                //if (child.GetComponent<Procedural>().GetIsLake())
                //{
                //    Debug.Log(rightDepth.GetComponent<Procedural>().GetIsLake());
                //    rightDepth.GetComponent<Procedural>().SetIsLake(true);
                //}
                //if (child)
                //{
                //    if (child.CompareTag("FloorLake"))
                //    {

                //        Debug.Log("entra");
                //        j = depth;


                //    }
                //}
                    
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

    //public void SetIsLake(bool isShown)
    //{
    //    awa = isShown;
    //}
}

