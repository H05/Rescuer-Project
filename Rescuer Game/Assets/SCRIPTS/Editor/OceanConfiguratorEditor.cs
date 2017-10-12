#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OceanConfigurator))]
public class OceanConfiguratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var OceanConfiguratorScript = target as OceanConfigurator;
        OceanConfiguratorScript.GetMeshInfoFromObject = EditorGUILayout.Toggle("Get Mesh From Object", OceanConfiguratorScript.GetMeshInfoFromObject);

        if (!OceanConfiguratorScript.GetMeshInfoFromObject)
        {
            OceanConfiguratorScript.WaterFidelity = EditorGUILayout.IntField("Water Fidelity", OceanConfiguratorScript.WaterFidelity);
            OceanConfiguratorScript.OceanWidth = EditorGUILayout.FloatField("Ocean Width", OceanConfiguratorScript.OceanWidth);
            OceanConfiguratorScript.OceanDepth = EditorGUILayout.FloatField("Ocean Depth", OceanConfiguratorScript.OceanDepth);

            GUILayout.Label("Wave Offset:");
            OceanConfiguratorScript.WaveOffset = EditorGUILayout.IntSlider(OceanConfiguratorScript.WaveOffset, 1, (int)OceanConfiguratorScript.OceanDepth);

            GUILayout.Label("Size Multiplier:");
            OceanConfiguratorScript.SizeMultiplier = EditorGUILayout.IntSlider(OceanConfiguratorScript.SizeMultiplier, 1 , 20);
        }
        else
        {
            OceanConfiguratorScript.OceanMesh  = (GameObject)EditorGUILayout.ObjectField("Ocean Mesh", OceanConfiguratorScript.OceanMesh, typeof(GameObject), true);
        }
    }
}
#endif
