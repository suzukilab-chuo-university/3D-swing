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
public class FileLoad : MonoBehaviour, IPointerDownHandler
{
    public static Texture texture;
    public ImageButton[] imageButton;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //

    // StandaloneFileBrowserのブラウザスクリプトプラグインから呼び出す
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    // ファイルを開く
    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".png, .jpg", true);
    }

    // ファイルアップロード後の処理
    public void OnFileUpload(string urls) {
        string[] url = urls.Split(',');
        for (int i = 0; i < url.Length; i++)
        {
            StartCoroutine( Load(url[i], i) );
        }
    }
#elif UNITY_EDITOR
    public void OnPointerDown(PointerEventData eventData) { }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => OpenFile());
    }

    // ファイルを開く
    public void OpenFile()
    {
        // 拡張子フィルタ
        var extensions = new[] {
            new ExtensionFilter("jpg", "jpg" ),
            new ExtensionFilter("png", "png" ),
        };

        // ファイルダイアログを開く
        //var paths = StandaloneFileBrowser.OpenFilePanel("画像を選択", "", extensions, false);
        string[] paths = new string[3];
        paths[0] = "/Users/kihara/Documents/01_大学/01_research/00_資料/3D-swing/20240630/texture_images/B_01.jpg";
        //paths[0] = "/Users/kihara/Desktop/tmp/B_01.jpg";
        
        for (int i = 0; i < 7; i++)
        {
            if (paths.Length > 0 && paths[0].Length > 0)
            {
                StartCoroutine(Load(new System.Uri(paths[0]).AbsoluteUri, i));
            }
        }
    }

#else
    //
    // OSビルド
    //

    public void OnPointerDown(PointerEventData eventData) { }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => OpenFile());
    }

    // ファイルを開く
    public void OpenFile()
    {
        // 拡張子フィルタ
        var extensions = new[] {
            new ExtensionFilter("jpg", "jpg" ),
            new ExtensionFilter("png", "png" ),
        };

        // ファイルダイアログを開く
        var paths = StandaloneFileBrowser.OpenFilePanel("画像を選択", "Desktop", extensions, true);
        for(int i = 0; i < paths.Length; i++)
        {
            if (i == 7) break;

            if (paths.Length > 0 && paths[i].Length > 0)
            {
                StartCoroutine(Load(new System.Uri(paths[i]).AbsoluteUri, i));
            }
        }
    }

#endif

    // ファイル読み込み
    private IEnumerator Load(string url, int i)
    {
        var loader = new WWW(url);
        yield return loader;

        texture = loader.texture;

        imageButton[i].UpdateImage();
    }
}