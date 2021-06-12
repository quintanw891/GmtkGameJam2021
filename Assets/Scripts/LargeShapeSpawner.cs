using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeShapeSpawner : MonoBehaviour
{
    public List<Sprite> largeShapeSprites;
    public bool spawned = false;

    void Start()
    {
        // Set the random seed
        Random.InitState(System.DateTime.Now.Millisecond);
        // Get the count of all large shape sprites
        int maxShapes = largeShapeSprites.Count;
        // Get a random number between 1, (count of all large shapes)
        int randNum = Random.Range(1, maxShapes+1);
        Debug.Log(string.Format("LargeShapeSpawner::Count is {0} | RandNum is {1}", maxShapes, randNum));

        // Load the large shape of the random number index
        GetComponent<Image>().sprite = largeShapeSprites[randNum - 1];

        spawned = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
            ScreenShotHandler.TakeScreenshot(500, 500);
    }

    // Just a test function to experiment with pixel counting
    private void PaintAlphaRed()
    {
        Texture2D old_texture = GetComponent<Image>().sprite.texture;
        Texture2D texture = new Texture2D(old_texture.width, old_texture.height);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, old_texture.width, old_texture.height), Vector2.zero);
        GetComponent<Image>().sprite = sprite;

        int area = old_texture.height * old_texture.width;
        int blk_counter = 0;
        for (int y = 0; y < old_texture.height; y++)
        {
            for (int x = 0; x < old_texture.width; x++)
            {
                if (old_texture.GetPixel(x, y) == Color.black)
                {
                    blk_counter++;
                    texture.SetPixel(x, y, Color.black);
                }
                else texture.SetPixel(x, y, Color.red);
            }
        }

        texture.Apply();
        Debug.Log(string.Format("LargeShapeSpawner::{0}/{1} pixels are black. Painted the rest red.", blk_counter, area));
    }
}
