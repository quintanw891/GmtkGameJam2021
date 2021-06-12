using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trash : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private ShapeSelectionManager shapeSelectionManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down");
        shapeSelectionManager.RemoveShape();
    }
}
