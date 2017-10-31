using UnityEngine;

public class OceanMeshInfo : MonoBehaviour
{
    public static int VertexCountTotal;
    public static int VertexCountSurface;
    public static int TrisCount;
    public static float VertexStep;

    //Mesh Info
    public static Vector3[] Vertices, Normals;
    public static Vector2[] UVs;
    public static int[] Tris;

    public static Mesh Mesh;

    private void Awake()
    {  
        OceanConfigurator OceanConfigurator = GetComponent<OceanConfigurator>();

        if (OceanConfigurator.GetMeshInfoFromObject == false)
        {
            Mesh = GetComponent<MeshFilter>().mesh;

            // We add 3 and later convert to float so we can achieve a result divisible by 4 [Remove them if not needed!]
            VertexCountSurface = OceanConfigurator.WaterFidelity;
            VertexCountTotal = (VertexCountSurface * 3); // 1 Quad has a Total of 4 Vertices
            TrisCount = ((VertexCountTotal / 6) * 2 * 12) - 12;  // 1 Quad has 2 Triangles and every Triangle has 3 Vertices or a Total of 2 Triangles and 6 Vertices per Quad
            VertexStep = OceanConfigurator.OceanWidth / OceanConfigurator.WaterFidelity;

            Vertices = new Vector3[VertexCountTotal];
            Tris = new int[TrisCount];
            Normals = new Vector3[VertexCountTotal];
            UVs = new Vector2[VertexCountTotal];
        }

    }
}
