using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public Rigidbody2D RB;

    public bool IsSinkable;

    public float Mass;
    public float Volume;
    public float Density;
    public float BuoyantForce;
    public float Inertia;

    private float GravityAcceleration;
    private float WaterDensity;
    private float MassNormalizer;

    public FloatingObject(GameObject Object)
    {
        RB = Object.GetComponent<Rigidbody2D>();
        GravityAcceleration = Physics2D.gravity.y;
        WaterDensity = 2000;

        Inertia = RB.inertia;
        Mass = RB.mass;
        Volume = Object.transform.localScale.x * Object.transform.localScale.y;
        Density = Mass / Volume * 100;
        BuoyantForce = (WaterDensity * Volume * GravityAcceleration) / 100;

        FloatingStatus();
    }
    private bool FloatingStatus()
    {
        if(Density < WaterDensity) { return IsSinkable = false; } else { return IsSinkable = true; }
    }
}
