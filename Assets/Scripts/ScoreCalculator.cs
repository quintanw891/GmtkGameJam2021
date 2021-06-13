using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{
    private bool calculating;
    public bool finished;
    private int pixelsNotMatching;
    private int shapesAboveThreshold;
    private int finalScore;

    [SerializeField]
    private float extraShapePenalty;
    [SerializeField]
    private GameObject results;

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

    /*private void CalculateScore()
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

    }*/

    private IEnumerator GetPixelsNotMatching()
    {
        var oldPath = string.Format("{0}/original.png", Application.dataPath);
        var newPath = string.Format("{0}/latest.png", Application.dataPath);
        yield return new WaitUntil(() => System.IO.File.Exists(oldPath) & System.IO.File.Exists(newPath));

        int totalPossible = 0;
        int filled = 0;
        int spilled = 0;

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
                /*if (oldPixel.a != newPixel.a)
                {
                    count++;
                }
                else if (oldPixel == Color.black & newPixel != Color.white)
                {
                    count++;
                }*/
                if (oldPixel == Color.black)
                {
                    totalPossible++;
                    if (newPixel == Color.white)
                    {
                        filled++;
                    }
                }
                else
                {
                    if (newPixel == Color.white)
                    {
                        spilled++;
                    }
                }
            }
        }

        Debug.Log("filled " + filled);
        Debug.Log("spiled " + spilled);
        Debug.Log("totalPossible " + totalPossible);

        float fillPercent = ((float)filled / (float)totalPossible) * 100f;         //percent filled
        fillPercent = (fillPercent >= 95) ? 100 : fillPercent;  //...with leniency

        float spillPercent = ((float)spilled / (float)totalPossible) * 100f;     //percent spilled
        spillPercent = (spillPercent <= 5) ? 0 : spillPercent;  //...with leniency

        Debug.Log("Fill " + fillPercent + "%");
        Debug.Log("Spill " + spillPercent + "%");
        int extra = PlayerPrefs.GetInt("SmallShapesUsed") - PlayerPrefs.GetInt("ShapeThreshold");
        extra = (extra < 0) ? 0 : extra;
        Debug.Log("Extra " + extra);
        float score = (fillPercent - spillPercent) - extra * extraShapePenalty;
        score = (score < 0) ? 0 : score;

        results.transform.Find("Text").gameObject.GetComponent<Text>().text = string.Format("Your creation...\n\nFilled the image by:\t\t\t\t\t\t{0}%\nSpilled outside the image by:\t\t\t{1}%\nUsed extra shapes:\t\t\t\t\t\t{2}\n\nYour score is:\t\t\t\t\t\t{3}",
            fillPercent.ToString("000"), spillPercent.ToString("000"), extra, score.ToString("000"));   
        //string.Format("HP:  {0}", player.getHealth());

        /*
        float margin = (float)count / (tOld.width * tOld.height);
        margin *= 100;

        // If more than 5 pct of pixels not matching...
        if (margin >= 5f) pixelsNotMatching = count;

        CalculateScore();
        finished = true;
        */
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
