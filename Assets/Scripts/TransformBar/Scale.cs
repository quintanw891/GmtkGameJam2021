using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum ScaleDirection { Down, Up };

public class Scale : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private ShapeSelectionManager shapeSelectionManager;
    [SerializeField]
    private ScaleDirection direction;
    [SerializeField]
    private float minScaleMagnitude;
    [SerializeField]
    private float maxScaleMagnitude;

    private GameObject shapeToTransform;
    [SerializeField]
    private float scaleSpeed;

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
            int directionModifier = (direction == ScaleDirection.Down) ? -1 : 1;
            if ((direction == ScaleDirection.Down && shapeToTransform.transform.localScale.magnitude > minScaleMagnitude) || 
                    (direction == ScaleDirection.Up && shapeToTransform.transform.localScale.magnitude < maxScaleMagnitude))
            {
                shapeToTransform.transform.localScale = shapeToTransform.transform.localScale + (Vector3.one * scaleSpeed * directionModifier * Time.deltaTime);
            }
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
