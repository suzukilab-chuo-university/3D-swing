using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class GenerateMesh : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Renderer meshRenderer;
    private float meshSize = 8f;
    private bool flag;
    private float depthValue = 0.75f;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<Renderer>();

        flag = true;
    }


    public void UpdateMesh(Sprite textureSprite)
    {
        // メッシュを生成
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        // 画像の幅と高さを取得
        int w = (int)textureSprite.textureRect.width;
        int h = (int)textureSprite.textureRect.height;

        // 1画素当たりの画像の大きさを取得
        float ws = meshSize / w, hs = meshSize / h;

        // 横長の画像の場合 メッシュの横のサイズを6fにする
        if (ws < hs)
        {
            transform.localScale = new Vector3(ws, ws, 1f);
            transform.localPosition = new Vector3(0, -meshSize / 2 * h / w, 0);
        }
        // 縦長の画像の場合 メッシュの縦のサイズを6fにする
        else
        {
            transform.localScale = new Vector3(hs, hs, 1f);
            transform.localPosition = new Vector3(0, -meshSize / 2, 0);
        }

        // 頂点リストを生成
        List<Vector3> vertices = new List<Vector3>();
        
        // 深度情報を取得
        Color[] depths = textureSprite.texture.GetPixels(0, 0, w, h);

        for (int j = 0; j < h; j++)
        {
            for (int i = 0; i < w; i++)
            {
                if (i == 10)
                {
                    Debug.Log(depths[i + j * w].grayscale);
                }
                vertices.Add(new Vector3((float)(i - w / 2), (float)j, depths[i + j * w].grayscale / depthValue));
            }
        }

        // 面を構成するインデックスリストを作成
        List<int> triangles = new List<int>();

        // メッシュを構成する三角形を作成
        for (int j = 0; j < h - 1; j++)
        {
            for (int i = 0; i < w - 1; i++)
            {
                triangles.Add(i + j * w);
                triangles.Add((i + 1) + (j + 1) * w);
                triangles.Add((i + 1) + j * w);

                triangles.Add(i + j * w);
                triangles.Add(i + (j + 1) * w);
                triangles.Add((i + 1) + (j + 1) * w);
            }
        }

        // メッシュに頂点を登録
        mesh.SetVertices(vertices);

        // メッシュに面を構成するインデックスリストを登録
        mesh.SetTriangles(triangles, 0);

        // uv情報を登録
        Vector2[] uvs = new Vector2[(h) * (w)];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(0.5f + vertices[i].x / w, vertices[i].y / h);
        }
        mesh.uv = uvs;

        // 作成したメッシュをメッシュフィルターに設定
        meshFilter.mesh = mesh;

        // メッシュに画像を設定
        meshRenderer.material.SetTexture("_MainTex", textureSprite.texture);
        meshRenderer.material.shader = Shader.Find("Unlit/mesh");

        SwingManager.beforeTime = Time.time;

        if (flag)
        {
            flag = false;
            UpdateMesh(textureSprite);
        }

    }

    public void DestroyMesh()
    {
        meshFilter.mesh.Clear();
    }
}
