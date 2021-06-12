using System.Collections;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private bool calculating;
    public bool finished;
    private int pixelsNotMatching;
    private int shapesAboveThreshold;
    private int finalScore;

    private void Awake()
    {
        calculating = false;
        finished = false;
        pixelsNotMatching = 0;
        shapesAboveThreshold = 0;
        finalScore = 1000;
    }

    // Don't call until finished == true
    public int GetFinalScore()
    {
        return finalScore;
    }

    private void Update()
    {
        if (!calculating)
        {
            calculating = true;
            StartCoroutine(GetPixelsNotMatching());
        }
    }

    private void CalculateScore()
    {
        int shapesUsed = PlayerPrefs.GetInt("SmallShapesUsed");
        shapesAboveThreshold = PlayerPrefs.GetInt("ShapeThreshold") - shapesUsed;
        if (shapesAboveThreshold < 0)
        {
            finalScore -= 50 * Mathf.Abs(shapesAboveThreshold);
            Debug.Log(string.Format("ScoreCalculator::CalculatScore()::{0} more shapes used than necessary, -{1} points", Mathf.Abs(shapesAboveThreshold), 50 * Mathf.Abs(shapesAboveThreshold)));

        }
        Debug.Log(string.Format("ScoreCalculator::CalculatScore()::{0} pixels not matching, -{1} points", pixelsNotMatching, pixelsNotMatching));
        finalScore -= pixelsNotMatching;
        Debug.Log(string.Format("ScoreCalculator::CalculatScore()::Final score is {0}", finalScore));

    }

    private IEnumerator GetPixelsNotMatching()
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

        pixelsNotMatching = count;

        CalculateScore();
        finished = true;
    }
}
