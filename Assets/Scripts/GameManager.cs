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
        SceneManager.LoadScene("ResultsScene");
    }

}
