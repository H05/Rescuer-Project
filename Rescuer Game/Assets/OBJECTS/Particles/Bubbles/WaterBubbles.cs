using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubbles : MonoBehaviour {

    public Transform[] BubbleAttachments;
    public GameObject BubbleObject;
    public Vector3 tensor;
    private ParticleSystem[] PS;
    public Rigidbody2D rb;
    private void Start()
    {
        PS = new ParticleSystem[2];

        PS[0] = BubbleAttachments[0].GetComponent<ParticleSystem>();
        //PS[1] = BubbleAttachments[1].GetComponent<ParticleSystem>();
    }

    private void Update ()
    {
        if (transform.localEulerAngles.z > 1f || transform.localEulerAngles.z < -1)
        {
            BubbleAttachments[0].transform.rotation = Quaternion.Euler(0, 0, transform.localRotation.z * -1);
            //BubbleAttachments[1].transform.rotation = Quaternion.Euler(0, 0, transform.localRotation.z * -1);
        }
        if (transform.localPosition.y < -2 && BubbleObject.active == false) { BubbleObject.SetActive(true); }
        else if(transform.localPosition.y > -2 && BubbleObject.active == true) { PS[0].Play(false);}
    }
}
