using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicAnimationPlayer : MonoBehaviour
{
    private Animator animator;
    public KinctMovePlayer kinctMovePlayer; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("SpeedPlayer", kinctMovePlayer.speedPlayer); 
        animator.SetBool("mortalBool", kinctMovePlayer.mortalBool);
        animator.SetFloat("Time", Time.time);
    }
}
