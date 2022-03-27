using UnityEngine;

public class Generation : MonoBehaviour
{
    public int numElements;
    public int depth;
    public GameObject[] modules;
    public GameObject LakeModule;
    public GameObject FirstDepthtWithoutLake;
    private bool oneTime = false;

    void Start()
    {
        GameObject left = this.transform.GetChild(0).gameObject;
        GameObject right = this.transform.GetChild(1).gameObject;

        for (int i = 0; i < numElements; i++)
        {
            for (int j = 0; j < depth; j++)
            {
                Vector3 rightPosition = new Vector3(2*i, 0, -1-2*j);
                Vector3 leftPosition = new Vector3(2*i, 0, 1+2*j);

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
        if (!oneTime)
            noRepetition();
    }

    public void overwriteFloor(Transform depth, Transform side, Transform SecondDepth = null)
    {
        string tag = depth.GetChild(0).tag;
        Vector3 position = depth.transform.position;
        Quaternion rotation = Quaternion.identity;

        Destroy(depth.GetChild(0).gameObject);
        Destroy(depth.gameObject);

        if (side.name.Equals("Left"))
        {
            rotation = Quaternion.Euler(Vector3.up * 180);
        }

        GameObject noRepetitionModule = Instantiate(FirstDepthtWithoutLake, position, Quaternion.identity);
        
        //Debug.Log(tag);
        //Debug.Log(noRepetitionModule.transform.GetChildCount());

        //&& noRepetitionModule.transform.GetChild(0).CompareTag(SecondDepth.gameObject.tag)
        //   && !noRepetitionModule.transform.GetChild(0).CompareTag("Floor"))

        //while (noRepetitionModule.transform.GetChild(0).CompareTag(tag))
        //{
        //    noRepetitionModule = Instantiate(FirstDepthtWithoutLake, position, Quaternion.identity);
        //    Debug.Log(noRepetitionModule.tag);
        //}
        noRepetitionModule.transform.rotation = rotation;
        noRepetitionModule.name = "NoRepetition";
        noRepetitionModule.transform.SetParent(side.transform);
    }
    public int LakeGeneration(Transform depthTransform, Transform side, int i)
    {
        int count0 = 0;
        int count1 = 1;
        if (i + depth * 3 <= numElements)
        {
            for (int j = 0; j < 3; j++)
            {
               
                GameObject childDepths0 = side.transform.GetChild(i + count0).gameObject;
                GameObject childDepths1 = side.transform.GetChild(i + count1).gameObject;
                GameObject childFloor0 = childDepths0.transform.GetChild(0).gameObject;
                GameObject childFloor1 = childDepths1.transform.GetChild(0).gameObject;

                if (j == 2)
                {
                    Vector3 position = childFloor1.transform.position;
                    Quaternion rotation = Quaternion.identity;
                    if (side.name.Equals("Left"))
                    {
                        rotation = Quaternion.Euler(Vector3.up * 180);
                        position += new Vector3(-4, 0, 0);
                    }
                    GameObject Lake = Instantiate(LakeModule, position, rotation);
                    Lake.transform.SetParent(childDepths1.transform);
                }
                Destroy(childFloor0);
                Destroy(childFloor1);
                count0 += depth;
                count1 += depth;
            }
            i += depth * 3;
            if (side.transform.GetChild(i).transform.GetChild(0).CompareTag("FloorLake"))
            {
                overwriteFloor(depthTransform, side);
                Debug.Log(i);
            }
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
                else if (i + depth < sideNumChild
                    && childDepth.CompareTag("FirstDepth")
                    && !childModule.CompareTag("Floor")
                    && childModule.tag.Equals(sideChild.transform.GetChild(i + depth).GetChild(0).tag))
                {
                    overwriteFloor(sideChild.transform.GetChild(i + depth), sideChild, sideChild.transform.GetChild(i + 2 * depth));

                }
            }

        }
        oneTime = true;
    }

}

