using UnityEngine;
public class WaveSimulator : OceanMeshInfo
{
    public float WaveCount = 3;
    public float WaveAmplitude = 3;
    public float WaveSpeed = 1;
    public float WaveWobble = 3;
    public float WaveAmplitudeWobble = 3;
    public float WaveOffsetWobble = 3;
    public float ScrollSpeed = 0.05f;

    private Vector3[] Surface_Layer1_Positions;
    private Vector3[] Surface_Layer2_Positions;

    private int Incr = 0;
    private int PointsNumber = 0;

    private Renderer Renderer;

    private void Start()
    {
       Renderer = GetComponent<Renderer>();

        PointsNumber = OceanMeshInfo.Vertices.Length / 3;

        Surface_Layer1_Positions = new Vector3[PointsNumber];
        Surface_Layer2_Positions = new Vector3[PointsNumber];

        Surface_Layer1_Positions[0] = OceanMeshInfo.Vertices[2]; 
        Surface_Layer1_Positions[1] = OceanMeshInfo.Vertices[3];

        Surface_Layer2_Positions[0] = OceanMeshInfo.Vertices[0];
        Surface_Layer2_Positions[1] = OceanMeshInfo.Vertices[1];

        for (int i = 0; i < PointsNumber - 3; i++)
        {
            Surface_Layer1_Positions[i] = OceanMeshInfo.Vertices[7 + Incr];
            Surface_Layer2_Positions[i] = OceanMeshInfo.Vertices[6 + Incr]; 
            Incr += 3;
        }
    
        Incr = 0;
    }
    private void Update()
    {
        float offset = Time.time * ScrollSpeed;
        Renderer.material.SetTextureOffset("_Surface", new Vector2(offset, -24.14f));
    }
    private void FixedUpdate()
    {
        Incr = 0;

        for (int i = 0; i < PointsNumber - 3; i++)
        {
            Surface_Layer1_Positions[i].y = Mathf.Cos(i - WaveOffsetWobble * Time.time * WaveWobble) * (WaveAmplitudeWobble / 100) + (WaveAmplitude / 100) * Mathf.Sin(((WaveCount / 10) * i + Time.time * -WaveSpeed));
            Surface_Layer2_Positions[i].y = Mathf.Cos(i - WaveOffsetWobble * Time.time * WaveWobble) * (WaveAmplitudeWobble / 100) + (WaveAmplitude / 100) * Mathf.Sin((2 * 3.14f / 2 + (WaveCount / 10) * i + Time.time * -WaveSpeed));

            OceanMeshInfo.Vertices[7 + Incr] = new Vector3(Surface_Layer1_Positions[i].x, Surface_Layer1_Positions[i].y + 1f, Surface_Layer1_Positions[i].z);
            OceanMeshInfo.Vertices[6 + Incr] = new Vector3(Surface_Layer2_Positions[i].x, Surface_Layer2_Positions[i].y + 0.15f, Surface_Layer2_Positions[i].z);
            Incr += 3;
        }
        OceanMeshInfo.Mesh.vertices = OceanMeshInfo.Vertices;
    }
 }

 