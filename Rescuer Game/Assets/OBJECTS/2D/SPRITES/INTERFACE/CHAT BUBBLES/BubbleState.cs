using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleState : MonoBehaviour
{
    public GameObject Bubble;
    private Animator BubbleAnimator;

    public static bool IsOpened;
    public static bool IsClosed;

    private void Start()
    {
        BubbleAnimator = Bubble.GetComponent<Animator>();
    }
    private void Update()
    {
        if (BubbleAnimator.GetCurrentAnimatorStateInfo(0).IsName("Bubble_Open"))
        {
            IsOpened = true;
            IsClosed = false;
        }
        else 
        {
            IsOpened = false;
            IsClosed = true;
        }
    }
}
