using UnityEngine;
using UnityEditor;

public class EnableGPUInstancingAllMaterials
{
    [MenuItem("Tools/Enable GPU Instancing For All Materials")]
    public static void EnableInstancing()
    {
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");
        int count = 0;

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null && !mat.enableInstancing)
            {
                mat.enableInstancing = true;
                EditorUtility.SetDirty(mat);
                count++;
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"✅ Đã bật GPU Instancing cho {count} materials!");
    }
}
