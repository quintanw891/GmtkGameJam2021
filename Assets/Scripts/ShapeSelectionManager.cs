using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSelectionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shapePrefab;
    private List<GameObject> shapeList;
    private GameObject selectedShape;

    // Start is called before the first frame update
    void Start()
    {
        shapeList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject AddShape(Sprite sprite)
    {
        GameObject shape = Instantiate(shapePrefab, transform, false);
        Shape shapeScript = shape.GetComponent<Shape>();
        shapeScript.SetSprite(sprite);
        selectedShape = shape;
        shapeList.Add(shape);
        ChangeSelection(shape);
        return shape;
    }

    public void ChangeSelection(GameObject selectedShape)
    {
        foreach( GameObject shape in shapeList)
        {
            Shape shapeScript = shape.GetComponent<Shape>();
            if (shape == selectedShape)
            {
                shapeScript.EnableHighlight();
            }
            else
            {
                shapeScript.DisableHighlight();
            }
        }
    }
}
