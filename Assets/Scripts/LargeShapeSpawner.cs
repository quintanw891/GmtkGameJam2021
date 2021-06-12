using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeShapeSpawner : MonoBehaviour
{
    public List<LargeShapeData> largeShapes;
    public bool spawned = false;

    void Start()
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

        ScreenShotHandler.TakeScreenshot(500, 500, "original");

    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            ScreenShotHandler.TakeScreenshot(500, 500, "latest");
            StartCoroutine(getPixelsOutOfBounds());          
        }
    }

    private IEnumerator getPixelsOutOfBounds()
    {
        var oldPath = string.Format("{0}/original.png", Application.dataPath);
        var newPath = string.Format("{0}/latest.png", Application.dataPath);
        yield return new WaitUntil(() => System.IO.File.Exists(oldPath) & System.IO.File.Exists(newPath));

        int count = 0;
        var bOld = System.IO.File.ReadAllBytes(oldPath);
        var bNew = System.IO.File.ReadAllBytes(newPath);
        Texture2D tOld = new Texture2D(500, 500);
        tOld.LoadImage(bOld);
        Texture2D tNew = new Texture2D(500, 500);
        tNew.LoadImage(bNew);

        for (int y = 0; y < tOld.height; y++)
        {
            for (int x = 0; x < tOld.width; x++)
            {
                if (tOld.GetPixel(x, y) != tNew.GetPixel(x, y)) count++;
            }
        }

        Debug.Log(string.Format("ScreenShotHandler::getPixelsOutOfBounds()::{0} pixels not matching", count));
        yield return count;
    }
}
