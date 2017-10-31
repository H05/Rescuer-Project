using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class OceanMeshGenerator : MonoBehaviour
{
    private static int[] Quad1TrisPattern = new int[6] { 0, 2, 1, 2, 3, 1 };
    private static int[] Quad2TrisPattern = new int[6] { 4, 0, 5, 0, 1, 5 };

    //Mesh Initialization
    private static void InitializeMesh(Vector3[] Vertices, int[] Tris, float OceanWidth, float OceanDepth, float WaveOffset, float SizeMultiplier)
    {
        //Vertex initialization Upper
        Vertices[0] = new Vector3(0, -WaveOffset, 0);
        Vertices[1] = new Vector3(OceanWidth, -WaveOffset, 0);
        Vertices[2] = new Vector3(0, 0, 0);
        Vertices[3] = new Vector3(OceanWidth , 0, 0);

        //Vertex initialization Base
        Vertices[4] = new Vector3(0, -OceanDepth, 0);
        Vertices[5] = new Vector3(OceanWidth, -OceanDepth, 0);

        //Tris initialization
        //First Triangle
        Tris[0] = Quad1TrisPattern[0];
        Tris[1] = Quad1TrisPattern[1];
        Tris[2] = Quad1TrisPattern[2];

        //Second Triangle
        Tris[3] = Quad1TrisPattern[3];
        Tris[4] = Quad1TrisPattern[4];
        Tris[5] = Quad1TrisPattern[5];

        //Tris initialization
        //First Triangle
        Tris[6] = Quad2TrisPattern[0];
        Tris[7] = Quad2TrisPattern[1];
        Tris[8] = Quad2TrisPattern[2];

        //Second Triangle
        Tris[9] = Quad2TrisPattern[3];
        Tris[10] = Quad2TrisPattern[4];
        Tris[11] = Quad2TrisPattern[5];
    }

    //Create Vertices
    public static Vector3[] SetVertices(Vector3[] Vertices, int[] Tris, float OceanWidth, float OceanDepth, float WaveOffset, float SizeMultiplier)
    {
        OceanWidth *= SizeMultiplier * 10;
        OceanDepth *= SizeMultiplier * 10;
        WaveOffset *= SizeMultiplier * 10;

        InitializeMesh(Vertices, Tris, OceanWidth, OceanDepth, WaveOffset, SizeMultiplier);

        float VertexIncr = OceanWidth;

        for (int i = 6; i < Vertices.Length; i += 3)
        {
            VertexIncr += OceanWidth; //Add Vertex every Width-step

            //We add two vertices each iteration, instead of 4, because we want to merge them
            //Upper Vertices
            Vertices[i] = new Vector3(VertexIncr, -WaveOffset, 0); //6
            Vertices[i + 1] = new Vector3(VertexIncr, 0, 0); 

            //Bottom Vertices
            Vertices[i + 2] = new Vector3(VertexIncr, -OceanDepth, 0);
        }

        return Vertices;
    }

    //Create Triangles
    public static int[] SetTriangles(int[] Tris)
    {
        for (int i = 12; i < Tris.Length; i += 12) //Create Triangles
        {
            //Tris Sequence Top
            Quad1TrisPattern[0] = Quad1TrisPattern[2];
            Quad1TrisPattern[1] = Quad1TrisPattern[4];
            Quad1TrisPattern[2] = Quad2TrisPattern[2] + 1;

            Quad1TrisPattern[3] = Quad1TrisPattern[1];
            Quad1TrisPattern[4] = Quad2TrisPattern[2] + 2;
            Quad1TrisPattern[5] = Quad2TrisPattern[2] + 1;

            //First Triangle
            Tris[i] = Quad1TrisPattern[0];
            Tris[i + 1] = Quad1TrisPattern[1];
            Tris[i + 2] = Quad1TrisPattern[2];
            //Second Triangle
            Tris[i + 3] = Quad1TrisPattern[3];
            Tris[i + 4] = Quad1TrisPattern[4];
            Tris[i + 5] = Quad1TrisPattern[5];

            //////////////////////
            //Tris Sequence Bottom
            Quad2TrisPattern[0] = Quad2TrisPattern[2];
            Quad2TrisPattern[1] = Quad1TrisPattern[0];
            Quad2TrisPattern[2] = Quad2TrisPattern[2] + 3;

            Quad2TrisPattern[3] = Quad1TrisPattern[0];
            Quad2TrisPattern[4] = Quad1TrisPattern[2];
            Quad2TrisPattern[5] = Quad2TrisPattern[2];

            //First Triangle
            Tris[i + 6] = Quad2TrisPattern[0];
            Tris[i + 7] = Quad2TrisPattern[1];
            Tris[i + 8] = Quad2TrisPattern[2];

            //Second Triangle
            Tris[i + 9] = Quad2TrisPattern[3];
            Tris[i + 10] = Quad2TrisPattern[4];
            Tris[i + 11] = Quad2TrisPattern[5];
        }

        return Tris;
    }

    //Create Normals
    public static Vector3[] SetNormals(Vector3[] Normals)
    {
        //Create the Normals by Initializing the whole array with normals - facing forward
        Normals = Enumerable.Repeat(new Vector3(0, 0, -1), Normals.Length).ToArray();

        return Normals;
    }

    //Create UVs
    public static Vector2[] SetUVs(Vector2[] UVs)
    {
        for (int i = 0; i < UVs.Length; i ++) //Create UVs
        {
            UVs[i] = new Vector2(0, 0);    
        }

        return UVs;
    }
}

