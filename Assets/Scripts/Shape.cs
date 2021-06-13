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
        var point = Camera.main.ScreenToWorldPoint(eventData.position);
        point.z = 0f;
        transform.position = point;
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
}
