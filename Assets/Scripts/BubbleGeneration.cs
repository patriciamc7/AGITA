using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGeneration : MonoBehaviour
{
    public GameObject Bubble;
    public int MAX_BUBBLES;

    private List<GameObject> Bubbles;
    private int BubbleCount = 0;
    private List<float> velocity;
    private List<float> height;

    //Start is called before the first frame update
    void Start()
    {
        Bubbles = new List<GameObject>();
        velocity = new List<float>();
        height = new List<float>();
    }

    //Update is called once per frame
    void Update()
    {
        if (BubbleCount < MAX_BUBBLES)
        {
            Vector3 pos = new Vector3(this.transform.position.x + AddNoise(0, 1),  AddNoise(0.5f,1.2f), this.transform.position.z);
            Bubbles.Add(Instantiate(Bubble, pos, Quaternion.Euler(90, 0, 0)));
            Bubbles[BubbleCount].transform.localScale = new Vector3(2f, 2f, 2f);
            Bubbles[BubbleCount].transform.SetParent(this.gameObject.transform);
            velocity.Add(AddNoise(0.006f, 0.01f));
            height.Add(AddNoise(0, 3));
            BubbleCount++;
        }

        if (Bubbles != null)
        {
            for (int i = 0; i < Bubbles.Count; i++)
            {
                GameObject bubble = Bubbles[i].transform.gameObject;
                float vel = velocity[i];
                float posy = height[i];

                float positionx = Vector3.Lerp(bubble.transform.position, new Vector3(0, 0, 0), Time.deltaTime *vel).x;
                if(bubble.transform.position.z == posy)
                {
                    height[i] = AddNoise(0, 3);
                    posy = height[i];
                }
                float positiony = Vector3.Lerp(bubble.transform.position, new Vector3(0, posy, 0), Time.deltaTime * vel).y;
                bubble.transform.position = new Vector3(positionx, positiony, this.transform.position.z);

                if (bubble.GetComponentInChildren<BubbleBurst>().destroy)
                {
                    bubble.GetComponentInChildren<ParticleSystem>().Play();
                    bubble.GetComponentInChildren<BubbleBurst>().destroyed = true;
                    Bubbles.Remove(bubble);
                }
            }
        }
    }

    public float AddNoise(float min, float max)
    {
        return Random.Range(min, max);
    }

}
