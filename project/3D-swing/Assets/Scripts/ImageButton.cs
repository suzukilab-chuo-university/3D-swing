using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    private bool isClick = false;
    [SerializeField]
    private GameObject SelectMarkImage;
    private Sprite textureSprite;

    public GenerateMesh generateMesh;
    public SelectManager selectManager;
    //public int meshNumber;

    void Start()
    {
        SelectMarkImage.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        isClick = !isClick;

        if (isClick)
        {
            SelectManager.cnt++;
            selectManager.ChangeMesh();
            SelectMarkImage.gameObject.SetActive(true);
            this.GetComponentInChildren<Image>().color = Color.gray;
            generateMesh.UpdateMesh(textureSprite);
        }
        else
        {
            SelectManager.cnt--;
            selectManager.ChangeMesh();
            SelectMarkImage.gameObject.SetActive(false);
            this.GetComponentInChildren<Image>().color = Color.white;
            generateMesh.DestroyMesh();
        }
    }

    public void UpdateImage()
    {
        Texture2D texture2D = ToTexture2D(FileLoad.texture);

        int newWidth = 512, newHeight = 512;
        if (texture2D.width > newWidth || texture2D.height > newHeight)
        {
            if (texture2D.width > texture2D.height) newHeight = (int)((float)newHeight * texture2D.height / texture2D.width);
            else newWidth = (int)((float)newWidth * texture2D.width / texture2D.height);
            texture2D = GetResized(texture2D, newWidth, newHeight);
        }

        //var resizedTexture = new Texture2D(newWidth, newHeight);
        //Graphics.ConvertTexture(texture2D, resizedTexture);
        //texture2D = resizedTexture;

        //textureSprite = Sprite.Create(FileLoad.texture, new Rect(0, 0, FileLoad.texture.width, FileLoad.texture.height), Vector2.zero);

        textureSprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);

        this.GetComponent<Image>().sprite = textureSprite;
    }

    private Texture2D ToTexture2D(Texture self)
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

    private Texture2D GetResized(Texture2D texture, int width, int height)
    {
        // リサイズ後のサイズを持つRenderTextureを作成して書き込む
        var rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(texture, rt);

        // リサイズ後のサイズを持つTexture2Dを作成してRenderTextureから書き込む
        var preRT = RenderTexture.active;
        RenderTexture.active = rt;
        var ret = new Texture2D(width, height);
        ret.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        ret.Apply();
        RenderTexture.active = preRT;

        RenderTexture.ReleaseTemporary(rt);
        return ret;
    }
}
