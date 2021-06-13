using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotHandler : MonoBehaviour
{
    private static ScreenShotHandler instance;
    private Camera _camera;
    private bool takeScreenshotOnNextFrame;
    private string fileName;
    [SerializeField]
    private GameObject UI;

    private void Awake()
    {
        instance = this;
        _camera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (takeScreenshotOnNextFrame)
        {
            UI.SetActive(false);
        }
    }

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = _camera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height,
                TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + string.Format("/{0}.png", this.fileName), byteArray);

            Debug.Log(string.Format("ScreenshotHandler::Saved screenshot to {0}", Application.dataPath));

            RenderTexture.ReleaseTemporary(renderTexture);
            _camera.targetTexture = null;
            UI.SetActive(true);
        }
    }

    private void _TakeScreenshot(int width, int height, string fileName)
    {
        _camera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        this.fileName = fileName;
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot(int width, int height, string fileName)
    {
        instance._TakeScreenshot(width, height, fileName);
    }
}
