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
        Debug.Log(string.Format("ScoreCalculator::CalculatScore()::{0} pixels not matching, -{1} points", pixelsNotMatching, (int)(pixelsNotMatching * 0.1)));
        finalScore -= (int)(pixelsNotMatching * 0.1);
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
                var oldPixel = tOld.GetPixel(x, y);
                var newPixel = tNew.GetPixel(x, y);
                if (oldPixel.a != newPixel.a) count++;
                else if (oldPixel == Color.black & newPixel != Color.white) count++;
            }
        }

        float margin = (float)count / (tOld.width * tOld.height);
        margin *= 100;

        // If more than 5 pct of pixels not matching...
        if (margin >= 5f) pixelsNotMatching = count;

        CalculateScore();
        finished = true;
    }

    private Sprite Paint(Texture2D old_texture, Color target, Color replacement)
    {
        Texture2D texture = new Texture2D(old_texture.width, old_texture.height);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, old_texture.width, old_texture.height), Vector2.zero);

        for (int y = 0; y < old_texture.height; y++)
        {
            for (int x = 0; x < old_texture.width; x++)
            {
                if (old_texture.GetPixel(x, y) == target)
                {
                    texture.SetPixel(x, y, replacement);
                }
                else
                {
                    texture.SetPixel(x, y, old_texture.GetPixel(x, y));
                }
            }
        }

        texture.Apply();
        return sprite;
    }
}
