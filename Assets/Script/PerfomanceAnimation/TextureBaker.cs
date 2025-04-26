using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

public class TextureBaker : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinned;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _fps;

    [ContextMenu("Bake Object")]
    public async void Bake()
    {
        var listTextures = await VertexBakerUtils.BakeClip(_animator, _skinned, _fps);

        for (int i = 0; i < listTextures.Count; i++)
        {
            Texture2D texture = listTextures[i];

            string folderPath = "Assets/VatBakerOutput";
            string assetPath = Path.Combine(folderPath, $"{texture.name}.asset");
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder("Assets", "VatBakerOutput");
            }

            AssetDatabase.CreateAsset(texture, assetPath);
        }

        AssetDatabase.SaveAssets();
        Debug.LogError("Bake completed");
    }
}
#endif

