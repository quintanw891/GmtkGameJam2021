using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private ShapeSelectionManager ssManager;

    private void Awake()
    {
        if (System.IO.File.Exists(string.Format("{0}/original.png", Application.dataPath)))
            System.IO.File.Delete(string.Format("{0}/original.png", Application.dataPath));

        if (System.IO.File.Exists(string.Format("{0}/latest.png", Application.dataPath)))
            System.IO.File.Delete(string.Format("{0}/latest.png", Application.dataPath));

        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    public void Update()
    {
        if (timer.triggered)
        {
            GoToResultsScreen();
        }
    }

    public void GoToResultsScreen()
    {
        PlayerPrefs.SetInt("SmallShapesUsed", ssManager.GetShapeCount());
        //ssManager.ChangeSelection(null);
        ScreenShotHandler.TakeScreenshot(500, 500, "latest");
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif
        SceneManager.LoadScene("ResultsScene");
    }

}
