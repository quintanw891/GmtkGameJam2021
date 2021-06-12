using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum RotateDirection { Clockwise, CounterClockwise };

public class Rotate : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private ShapeSelectionManager shapeSelectionManager;
    [SerializeField]
    private RotateDirection direction;

    private GameObject shapeToTransform;
    [SerializeField]
    private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        shapeToTransform = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (shapeToTransform)
        {
            int directionModifier = (direction == RotateDirection.Clockwise) ? -1 : 1;
            shapeToTransform.transform.Rotate(new Vector3(0, 0, rotateSpeed * directionModifier) * Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down");
        shapeToTransform = shapeSelectionManager.GetSelectedShape();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Mouse Up");
        shapeToTransform = null;
    }
}
