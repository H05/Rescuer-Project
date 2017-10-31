using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleController : MonoBehaviour
{
    public bool Toggle;
    public bool InRange;

    public GameObject Bubble;
    public GameObject Parent;
    private Text TextObject;

    private GameObject Object;

    public float Range;
    public float LetterDuration;
    public float BubbleOpenDuration;
    public float BubbleTransitionDuration;

    private float ElapsedTime = 0;
    public static Animator ObjAnimator;
    private string LastLine;
    private int Num;

    private void Start()
    {
        LastLine = "";

        if (Toggle)
        {
            Object = Instantiate(Bubble, new Vector3(transform.position.x + 0.5F, transform.position.y + 0.8F, Parent.transform.position.z), Quaternion.identity);
            Object.transform.SetParent(Parent.transform, false);
            ObjAnimator = Object.GetComponentInChildren<Animator>();
            TextObject = Object.GetComponentInChildren<Text>();
            Toggle = false;
        }
    }

    private void Update()
    {
        Object.transform.position = new Vector3(transform.position.x + 0.5F, transform.position.y + 0.8F, Parent.transform.position.z);
        DisplayBubble();
    }

    private void DisplayBubble()
    {
        if (InRange && !Object.activeSelf)
        {
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime >= BubbleOpenDuration)
            {
                ElapsedTime = 0;
                Object.SetActive(true);
                ObjAnimator.SetBool("IsBubbleOpened", true);
                GenerateText();
                StartCoroutine(WaitForAnimation(ObjAnimator.GetCurrentAnimatorStateInfo(0).length));
            }
        }
        if (Object.activeSelf)
        {  
            if (ElapsedTime >= BubbleTransitionDuration)
            {
                Object.GetComponentInChildren<Text>().enabled = false;
                ObjAnimator.SetBool("IsBubbleOpened", false);
       
                if (BubbleState.IsClosed){
                    ElapsedTime = 0;
                    Object.SetActive(false);
                }
            }
            else
            {
                ElapsedTime += Time.deltaTime;
            }
        }
    }
    IEnumerator WaitForAnimation(float AnimationLength)
    {
        yield return new WaitForSeconds(AnimationLength);
        Object.GetComponentInChildren<Text>().enabled = true;
    }
    private void GenerateText()
    {
        do { Num = Random.Range(0, TextLineManager.TextLine.Length); TextObject.text = TextLineManager.TextLine[Num]; }
        while (TextObject.text == LastLine);

        LastLine = TextObject.text;
    }
}
