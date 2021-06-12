using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SmallShapeSpawner : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private Sprite userImage;
    [SerializeField]
    private GameObject shapePrefab;

    private List<GameObject> shapeList;
    private GameObject selectedShape;

    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        image.sprite = userImage;
        shapeList = new List<GameObject>();
        selectedShape = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down");
        //GameObject shape = Instantiate(shapePrefab, transform.position, Quaternion.identity);
        GameObject shape = Instantiate(shapePrefab, transform.parent, false);
        shape.GetComponent<Image>().sprite = userImage;
        selectedShape = shape;
        shapeList.Add(shape);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Mouse Up");
        selectedShape = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        selectedShape.transform.position = eventData.position;
    }
}