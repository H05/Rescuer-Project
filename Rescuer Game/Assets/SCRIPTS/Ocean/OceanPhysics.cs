/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanPhysics : MonoBehaviour
{
    private GameObject OceanObj;
    public GameObject Detector;

    public float WaterResistance;
    public float WaterDensity = 20;
    private float WaterHeight;
    private float WaterPressure;
    public float BuoyancyDampening = 0.02f;

    public float FloatUnstability;
    public float FloatPrecision;

    private float BuoyantForce;
    private float UpwardForce = 0;

    private bool CanSubmerge;

    float WeightForce;
    float ObjectDensity;

    public GameObject Cube;

    private Vector3 ObjectPositions;
    private Vector3 SurfacePosition;
    private Vector3[] ObjectPoints;

    Vector3 Trns;

    bool IsInOfWater;

    float FractionSubMerged;
    float SubMergeArea;
    float NewPoint;

    bool ObjectCanFloat;

    float ObjectVelocityY;

    private void Awake()
    {
        OceanObj = GameObject.Find("Ocean");
        Collider2D OceanCollider = gameObject.GetComponent<Collider2D>();
        Ocean Ocean = OceanObj.GetComponent<Ocean>();
        OceanBorders = this.gameObject.GetComponent<OceanBorders>();
        WaterHeight = Ocean.OceanDepth;

        SurfacePosition = new Vector3(0, transform.position.y + OceanCollider.bounds.extents.y);
        Debug.Log(SurfacePosition);
    }
    private void LateUpdate()
    {
        ObjectVelocityY = Cube.GetComponent<Rigidbody2D>().velocity.y;
    }
    private void Update()
    {
        FractionSubMerged = ObjectDensity / WaterDensity;
        NewPoint = (Cube.transform.position.y + Cube.GetComponent<Collider2D>().bounds.extents.y) * FractionSubMerged ;
        Trns = new Vector3(Cube.transform.position.x, (Cube.transform.position.y - NewPoint) + 3, Cube.transform.position.z);
        Detector.transform.position = Trns;

        ObjectPoints = new Vector3[4];

        ObjectPoints[0] = Cube.gameObject.transform.TransformPoint(new Vector3(0.5f, 0.5f, 0));
        ObjectPoints[1] = Cube.gameObject.transform.TransformPoint(new Vector3(-0.5f, 0.5f, 0));
        ObjectPoints[2] = Cube.gameObject.transform.TransformPoint(new Vector3(0.5f, -0.5f, 0));
        ObjectPoints[3] = Cube.gameObject.transform.TransformPoint(new Vector3(-0.5f, -0.5f, 0));

        ObjectPositions = Cube.transform.localPosition;
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.GetComponent<Collider2D>() != null)
        {
            WeightForce = hit.gameObject.GetComponent<Rigidbody2D>().mass * Physics2D.gravity.y;
            float ObjectVolume = hit.gameObject.transform.localScale.x * hit.gameObject.transform.localScale.y * hit.gameObject.transform.localScale.z;
            ObjectDensity = hit.gameObject.GetComponent<Rigidbody2D>().mass / ObjectVolume;

            BuoyantForce = WaterDensity * ObjectVolume * Physics2D.gravity.y;
           
            if (ObjectDensity < WaterDensity)
            {
                CanSubmerge = false;
                Debug.Log("Object is floating!");
      
            }
            else { CanSubmerge = true; Debug.Log("Object is going below water!"); }   
        }     
    }
    void OnTriggerStay2D(Collider2D hit)
    {
        IsInOfWater = true;
 
        Vector2 ObjectVelocity = hit.gameObject.GetComponent<Rigidbody2D>().velocity;
        WaterPressure = WaterDensity * Physics2D.gravity.y * (WaterHeight - hit.gameObject.transform.position.y);
   
            if (CanSubmerge == true)
            {
                float NetForce;
                NetForce = (WeightForce - BuoyantForce) * BuoyancyDampening;
                ObjectVelocity = new Vector2((hit.gameObject.GetComponent<Rigidbody2D>().velocity.x * -1) * WaterResistance, NetForce);
            }
            else
            {
            float UpWardForce = BuoyantForce * BuoyancyDampening;
            
            if(ObjectCanFloat == false)
                hit.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((hit.gameObject.GetComponent<Rigidbody2D>().velocity.x * -1) * WaterResistance, -UpWardForce));       
            }
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        IsInOfWater = false;
        ObjectCanFloat = !ObjectCanFloat;
        StartCoroutine(DecreaseVelocity(hit.gameObject));
    }
    IEnumerator DecreaseVelocity(GameObject hit)
    {
        hit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(hit.gameObject.GetComponent<Rigidbody2D>().velocity.x, Mathf.Lerp(hit.gameObject.GetComponent<Rigidbody2D>().velocity.y, -60, Time.deltaTime * 0.2f));
        yield return null;
    }
    public void WaveFollow(Vector3 Object, GameObject ObjectHit)
    {
        if(ObjectCanFloat == true)
            ObjectHit.GetComponent<Rigidbody2D>().MovePosition(new Vector2(ObjectHit.transform.position.x, Mathf.Lerp(ObjectHit.transform.position.y, Object.y * FloatUnstability - Trns.y, Time.fixedDeltaTime * FloatPrecision)));
    }
    void OnDrawGizmos()
    {
        if (IsInOfWater == false)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(ObjectPositions, new Vector3(ObjectPositions.x, ObjectPositions.y + 25, ObjectPositions.z));
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(ObjectPositions, new Vector3(ObjectPositions.x, ObjectPositions.y - 25, ObjectPositions.z));
        }
    }
}*/

