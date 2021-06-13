using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down");
        GameObject shapeSelectionManager = transform.parent.gameObject;
        ShapeSelectionManager managerScript = shapeSelectionManager.GetComponent<ShapeSelectionManager>();
        managerScript.ChangeSelection(transform.gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Mouse Up");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = eventData.position;
        //Debug.Log("Drag");
    }

    public void SetSprite(Sprite sprite)
    {
        transform.Find("Highlight").GetComponent<Image>().sprite = sprite;
        //transform.Find("Highlight").GetComponent<Image>().sprite = Paint(transform.Find("Highlight").GetComponent<Image>().sprite.texture, Color.black, Color.yellow);
        transform.Find("Image").GetComponent<Image>().sprite = sprite;
        //transform.Find("Image").GetComponent<Image>().sprite = Paint(transform.Find("Image").GetComponent<Image>().sprite.texture, Color.black, Color.white);
    }

    public void DisableHighlight()
    {
        transform.Find("Highlight").gameObject.SetActive(false);
    }

    public void EnableHighlight()
    {
        transform.Find("Highlight").gameObject.SetActive(true);
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
