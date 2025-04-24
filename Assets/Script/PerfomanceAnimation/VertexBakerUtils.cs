using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;

public static class VertexBakerUtils
{
    public static async Task<List<Texture2D>> BakeClip(Animator animator, SkinnedMeshRenderer skin, float fps)
    {
        await Task.Yield();
        var skinnedMeshTrans = skin.transform;
        skinnedMeshTrans.position = Vector3.zero;
        skinnedMeshTrans.rotation = Quaternion.identity;
        skinnedMeshTrans.localScale = Vector3.one;

        List<Texture2D> result = new List<Texture2D>();
        Mesh mesh = new Mesh();
        var clips = animator.runtimeAnimatorController.animationClips;

        for (int i = 0; i < clips.Length; i++)
        {
            AnimationClip clip = clips[i];
            int vertexCount = skin.sharedMesh.vertexCount;
            int frameCount = Mathf.FloorToInt(clip.length * fps) + 1;
            float deltaTime = 1f / fps;

            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> tmpVertices = new List<Vector3>();

            for (int k = 0; k < frameCount; k++)
            {
                clip.SampleAnimation(animator.gameObject, k * deltaTime);
                skin.BakeMesh(mesh);
                mesh.GetVertices(tmpVertices);
                vertices.AddRange(tmpVertices);
            }

            vertices = vertices.Select(x => animator.transform.InverseTransformPoint(x)).ToList();

            Texture2D vPosTex = new Texture2D(vertexCount, frameCount, TextureFormat.RGBAHalf, false, true)
            {
                name = $"{animator.gameObject.name}_{clip.name}",
                filterMode = FilterMode.Bilinear,
                wrapMode = TextureWrapMode.Repeat
            };

            vPosTex.SetPixels(vertices.Select(x => new Color(x.x, x.y, x.z)).ToArray());
            result.Add(vPosTex);
        }

        return result;
    }
}

