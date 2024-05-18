using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class generateMesh : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Renderer meshRenderer;
    private float meshSize = 6f;
    private float beforeDepthValue = 0.75f;
    private bool flag;

    public static float depthValue = 0.75f;
    public static Texture textureTexture;
    public static Texture depthTexture;

    int cnt = 0;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<Renderer>();

        beforeDepthValue = depthValue + 0.4f;
        flag = false;
    }

    void Update()
    {
        if(beforeDepthValue != depthValue && flag)
        {
            beforeDepthValue = depthValue;
            UpdateMesh();
        }
    }


    public void UpdateMesh()
    {
        flag = true;
        //画像が選択されているか
        if (textureTexture == null)
        {
            errorManager.errorReason = "Select image.";
            errorManager.errorFlag = true;
            return;
        }
        else if (depthTexture == null)
        {
            errorManager.errorReason = "Select image.";
            errorManager.errorFlag = true;
            return;
        }

        // Textureを読み込み
        Texture2D textureTexture2D = ToTexture2D(textureTexture);
        Sprite texture = Sprite.Create(textureTexture2D, new Rect(0, 0, textureTexture2D.width, textureTexture2D.height), Vector2.zero);

        // Depthを読み込み
        Texture2D depthTexture2D = ToTexture2D(depthTexture);
        Sprite depth = Sprite.Create(depthTexture2D, new Rect(0, 0, depthTexture2D.width, depthTexture2D.height), Vector2.zero);

        // メッシュを生成
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        // 画像の幅と高さを取得
        int w = (int)texture.textureRect.width;
        int h = (int)texture.textureRect.height;

        float ws = meshSize / w, hs = meshSize / h;
        if (ws < hs)  // 横長の画像の場合 メッシュの横のサイズを6fにする
        {
            transform.localScale = new Vector3(ws, ws, 1f);
            transform.position = new Vector3(0, - meshSize / 2 * h / w, 0);
        }
        else  // 縦長の画像の場合 メッシュの縦のサイズを6fにする
        {
            transform.localScale = new Vector3(hs, hs, 1f);
            transform.position = new Vector3(0, - meshSize / 2, 0);
        }

        // 頂点リストを生成
        List<Vector3> vertices = new List<Vector3>();

        // 面を構成するインデックスリストを作成
        List<int> triangles = new List<int>();

        Color[] colors = depth.texture.GetPixels(0, 0, w, h);
        Texture2D m_MainTexture = texture.texture;

        for (int j = 0; j < h; j++)
        {
            for (int i = 0; i < w; i++)
            {
                vertices.Add(new Vector3((float)(i - w / 2), (float)j, -colors[i + j * w].grayscale / depthValue));
            }
        }

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

        // メッシュに面を構成するインデックスリストを作成
        mesh.SetTriangles(triangles, 0);

        Vector2[] uvs = new Vector2[(h) * (w)];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(0.5f + vertices[i].x / w, vertices[i].y / h);
        }
        mesh.uv = uvs;

        // 作成したメッシュをメッシュフィルターに設定
        meshFilter.mesh = mesh;

        meshRenderer.material.SetTexture("_MainTex", m_MainTexture);
        meshRenderer.material.shader = Shader.Find("Sprites/Default");

        if (cnt == 0)
        {
            cnt++;
            UpdateMesh();
        }
    }

    Texture2D ToTexture2D(Texture self)
    {
        var sw = self.width;
        var sh = self.height;
        var format = TextureFormat.RGBA32;
        var result = new Texture2D(sw, sh, format, false);
        var currentRT = RenderTexture.active;
        var rt = new RenderTexture(sw, sh, 32);
        Graphics.Blit(self, rt);
        RenderTexture.active = rt;
        var source = new Rect(0, 0, rt.width, rt.height);
        result.ReadPixels(source, 0, 0);
        result.Apply();
        RenderTexture.active = currentRT;
        return result;
    }
}

