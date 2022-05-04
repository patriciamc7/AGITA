using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGeneration : MonoBehaviour
{
    public GameObject Bubble;
    public int MAX_BUBBLES;
    public List<GameObject> Bubbles;
    public float min;
    public float max;
    private int BubbleCount = 0;
    
    //Start is called before the first frame update
    void Start()
    {
        Bubbles = new List<GameObject>();
    }

    //Update is called once per frame
    void Update()
    {
        if (BubbleCount < MAX_BUBBLES)
        {
            Vector3 pos = new Vector3(this.transform.position.x + AddNoise(0, 1),  AddNoise(0.5f,1.2f), this.transform.position.z);
            Bubbles.Add(Instantiate(Bubble, pos, Quaternion.Euler(90, 0, 0)));
            Bubbles[BubbleCount].transform.localScale = new Vector3(0.02f,0.02f,0.02f);
            Bubbles[BubbleCount].transform.SetParent(this.gameObject.transform);
            BubbleCount++;
        }

        if (Bubbles != null)
        {
            for (int i = 0; i < Bubbles.Count; i++)
            {
                float positionx = Vector3.Lerp(Bubbles[i].transform.position, new Vector3(0, 0, 0), Time.deltaTime * 0.008f).x;
                float positiony = Vector3.Lerp(Bubbles[i].transform.position, new Vector3(AddNoise(min, max), AddNoise(min, max), AddNoise(min, max)), Time.deltaTime * 0.01f).y;
                Bubbles[i].transform.position = new Vector3(positionx, positiony, this.transform.position.z);
                if (Bubbles[i].GetComponentInChildren<BubbleBurst>().destroy)
                {
                    Bubbles[i].GetComponent<ParticleSystem>().Play();
                    Bubbles[i].GetComponent<BubbleBurst>().destroyed = true;
                    Bubbles.Remove(Bubbles[i]);
                }
            }
        }
    }

    public float AddNoise(float min, float max)
    {
        return Random.Range(min, max);
    }

}
