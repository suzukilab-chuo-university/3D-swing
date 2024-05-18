using System.Collections;
using UnityEngine;
using SFB;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine.Networking;

[RequireComponent(typeof(Button))]
public class fileLoad : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private RawImage outputImage;
    Vector2 initialSize;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //

    // StandaloneFileBrowserのブラウザスクリプトプラグインから呼び出す
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    // ファイルを開く
    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".", false);
    }

    // ファイルアップロード後の処理
    public void OnFileUpload(string url) {
        StartCoroutine(Load(url));
    }

    void Start()
    {
        initialSize = outputImage.rectTransform.rect.size;
    }

#else
    //
    // OSビルド & Unity editor上
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => OpenFile());
        initialSize = outputImage.rectTransform.rect.size;
    }

    // ファイルを開く
    public void OpenFile()
    {
        // 拡張子フィルタ
        var extensions = new[] {
            new ExtensionFilter("png", "png" ),
            new ExtensionFilter("jpg", "jpg" ),
        };

        // ファイルダイアログを開く
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
        if (paths.Length > 0 && paths[0].Length > 0)
        {
            StartCoroutine( Load(new System.Uri(paths[0]).AbsoluteUri) );
        }
    }

#endif

    // ファイル読み込み
    private IEnumerator Load(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        outputImage.texture = loader.texture;
        outputImage.FixAspect(initialSize);

        if (this.gameObject.name == "TextureButton")
        {
            generateMesh.textureTexture = loader.texture;
        }
        else if (this.gameObject.name == "DepthButton")
        {
            generateMesh.depthTexture = loader.texture;
        }
    }
}