using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmergeLogic : MonoBehaviour
{
    public float WaterDensity = 2000;
    public float WaterResistance = 1;

    [HideInInspector]
    public float GravityAcceleration;

    private List<FloatingObject> FloatingObjects;
    private Vector2 TargetVelocity;

    private void Start()
    {
        GravityAcceleration = Physics2D.gravity.y;
        FloatingObjects = new List<FloatingObject>();
    }

    private void FixedUpdate()
    {
        foreach (FloatingObject Object in FloatingObjects)
        {
            if (Object.IsSinkable)
            {
                TargetVelocity = new Vector2((Object.RB.velocity.x * -1) * WaterResistance, -100 / Object.Mass * (Object.RB.velocity.y * Object.Volume));
                Object.RB.velocity = Vector2.Lerp(Object.RB.velocity, TargetVelocity, 2 * Time.deltaTime);
                Object.RB.inertia = Object.Inertia * 3;
            }
            else 
            {
                Object.RB.AddForce(new Vector2((Object.RB.velocity.x * -1) * WaterResistance, -Object.BuoyantForce));
                Object.RB.transform.rotation = Quaternion.RotateTowards(Object.RB.transform.rotation, Quaternion.identity, 1 * Time.fixedDeltaTime);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        GameObject ObjectHit = hit.gameObject;

        if (ObjectHit.GetComponent<Collider2D>() != null && ObjectHit.GetComponent<Rigidbody2D>().isKinematic == false)
        {
            FloatingObjects.Add(new FloatingObject(ObjectHit));
        }
    }
}
