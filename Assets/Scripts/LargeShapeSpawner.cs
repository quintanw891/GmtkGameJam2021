using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeShapeSpawner : MonoBehaviour
{
    public List<LargeShapeData> largeShapes;
    public bool spawned = false;
    [SerializeField]
    private Timer timer;

    private void Start()
    {
        // Set the random seed
        Random.InitState(System.DateTime.Now.Millisecond);
        // Get the count of all large shape sprites
        int maxShapes = largeShapes.Count;
        // Get a random number between 1, (count of all large shapes)
        int randNum = Random.Range(1, maxShapes+1);
        Debug.Log(string.Format("LargeShapeSpawner::Count is {0} | RandNum is {1}", maxShapes, randNum));

        // Load the large shape of the random number index
        GetComponent<Image>().sprite = largeShapes[randNum - 1].sprite;
        spawned = true;

        // Save data to be passed to other scenes
        PlayerPrefs.SetString("ShapeName", largeShapes[randNum - 1].name);
        PlayerPrefs.SetFloat("TimeToComplete", largeShapes[randNum - 1].timeToComplete);
        PlayerPrefs.SetInt("ShapeThreshold", largeShapes[randNum - 1].shapeThreshold);

        timer.StartTimer(largeShapes[randNum - 1].timeToComplete);

        ScreenShotHandler.TakeScreenshot(500, 500, "original");
    }
    
}
