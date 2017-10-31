using UnityEngine;

[RequireComponent(typeof(OceanMeshInfo))]
public class OceanConfigurator : MonoBehaviour
{
    public bool GetMeshInfoFromObject = true;

    public int WaterFidelity = 200;
    public float OceanWidth = 100;
    public float OceanDepth = 60;
    public int WaveOffset = 5;
    public int SizeMultiplier = 2;

    public GameObject OceanMesh;

    private delegate void GetMeshDataDelegate();
    private void Start()
    {
        if (GetMeshInfoFromObject == true)
        {
            //GetData
            GetMeshData(GetInputDataFromMesh);
        }
        else
        {
            //GetData
            GetMeshData(GetInputDataFromScript);
            //Assign the Mesh data
            CreateOceanMesh(OceanMeshInfo.Mesh, OceanMeshInfo.Vertices, OceanMeshInfo.Tris, OceanMeshInfo.Normals, OceanMeshInfo.UVs);
        }
    }
    private void GetMeshData(GetMeshDataDelegate GetInputData) { GetInputData(); }
    private void GetInputDataFromMesh()
    {
        OceanMeshInfo.Mesh = OceanMesh.GetComponent<MeshFilter>().mesh;

        OceanMeshInfo.Vertices = new Vector3[OceanMeshInfo.Mesh.vertexCount];
        OceanMeshInfo.Tris = new int[OceanMeshInfo.Mesh.triangles.Length];
        OceanMeshInfo.Normals = new Vector3[OceanMeshInfo.Mesh.normals.Length];
        OceanMeshInfo.UVs = new Vector2[OceanMeshInfo.Mesh.uv.Length];

        OceanMeshInfo.Vertices = OceanMeshInfo.Mesh.vertices;
        OceanMeshInfo.Tris = OceanMeshInfo.Mesh.triangles;
        OceanMeshInfo.Normals = OceanMeshInfo.Mesh.normals;
        OceanMeshInfo.UVs = OceanMeshInfo.Mesh.uv;
    }
    private void GetInputDataFromScript()
    {
        OceanMeshInfo.Vertices = OceanMeshGenerator.SetVertices(OceanMeshInfo.Vertices, OceanMeshInfo.Tris, OceanMeshInfo.VertexStep, OceanDepth, WaveOffset, SizeMultiplier);
        OceanMeshInfo.Tris = OceanMeshGenerator.SetTriangles(OceanMeshInfo.Tris);
        OceanMeshInfo.Normals = OceanMeshGenerator.SetNormals(OceanMeshInfo.Normals);
        OceanMeshInfo.UVs = OceanMeshGenerator.SetUVs(OceanMeshInfo.UVs);
    }
    private void CreateOceanMesh(Mesh Mesh, Vector3[] Vertices, int[] Tris, Vector3[] Normals, Vector2[] UVs)
    {
        Mesh.vertices = Vertices;
        Mesh.triangles = Tris;
        Mesh.normals = Normals;
        Mesh.uv = UVs;
        Mesh.RecalculateBounds();
    }
}
