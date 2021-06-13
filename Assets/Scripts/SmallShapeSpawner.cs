using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SmallShapeSpawner : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private ShapeSelectionManager shapeSelectionManager;

    private GameObject selectedShape;

    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        image.sprite = sprite;
        selectedShape = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down");
        selectedShape = shapeSelectionManager.AddShape(sprite, eventData.pressPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var point = Camera.main.ScreenToWorldPoint(eventData.position);
        point.z = 0f;
        selectedShape.transform.position = point;
        //Debug.Log("Drag");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Mouse Up");
        selectedShape = null;
    }
}