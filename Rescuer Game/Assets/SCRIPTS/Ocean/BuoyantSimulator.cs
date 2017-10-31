using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyantSimulator : OceanMeshInfo
{ 
    public GameObject FloatingObject;
    public float StartPosition;

    private Rigidbody2D RB;
    private BoxCollider2D OceanCollider;

    private bool IsSubmerged;
    private bool CanFloat;
    private bool isKinematic;

    public float Offset;

    [Range(0, 1)]public float BuoyantPrecision;
    [Range(0, 1)]public float AnglePrecision;
    [Range(1, 10)]public float WobbleFactor;

    void Start ()
    {

        //PointsNumber = (OceanMeshInfo.Vertices.Length / 3);
        RB = FloatingObject.GetComponent<Rigidbody2D>();
        StartPosition = FloatingObject.transform.localPosition.y;

        OceanCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < 200 - 3; i++)
        {     
            if(FloatingObject.transform.localPosition.x <= WaveSimulator.Surface_Layer2_Positions[i].x && FloatingObject.transform.localPosition.x >= WaveSimulator.Surface_Layer2_Positions[i + 1].x)
            {
                Vector2 PointA = WaveSimulator.Surface_Layer2_Positions[i];
                Vector2 PointB = WaveSimulator.Surface_Layer2_Positions[i+1];

                float SlopePosition = PointA.y / PointA.x;
                float SlopeAngle = (PointB.y - PointA.y) / (PointB.x - PointA.x);

                float YBuoyant = SlopePosition * FloatingObject.transform.localPosition.x;
                Vector3 TargetPosition = new Vector3(FloatingObject.transform.localPosition.x, YBuoyant + StartPosition, FloatingObject.transform.localPosition.z);

                float ZBuoyant = Mathf.Tan(SlopeAngle) * Mathf.Rad2Deg;
                Quaternion TargetRotation = Quaternion.Euler(new Vector3(0, 0, ZBuoyant * WobbleFactor));

                StartCoroutine(BuoyantForce(TargetPosition, TargetRotation));
            }            
        }
    }
    IEnumerator BuoyantForce(Vector3 TargetPosition, Quaternion TargetRotation)
    {
        FloatingObject.transform.localPosition = Vector3.MoveTowards(FloatingObject.transform.localPosition, TargetPosition, BuoyantPrecision * Time.fixedDeltaTime);
        FloatingObject.transform.rotation = Quaternion.RotateTowards(FloatingObject.transform.rotation, TargetRotation, AnglePrecision * 100 * Time.fixedDeltaTime);
        yield return null;
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>().isKinematic == false)
        {
            float YVel = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, YVel / -2);
            isKinematic = false;
        }
    }*/
}
