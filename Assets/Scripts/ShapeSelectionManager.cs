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
        selectedShape = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject AddShape(Sprite sprite, Vector3 position)
    {
        var point = Camera.main.ScreenToWorldPoint(position);
        point.z = 0f;
        GameObject shape = Instantiate(shapePrefab, point, Quaternion.identity, transform);
        Shape shapeScript = shape.GetComponent<Shape>();
        shapeScript.SetSprite(sprite);
        shapeList.Add(shape);
        ChangeSelection(shape);
        return shape;
    }

    public void RemoveShape()
    {
        if (selectedShape)
        {
            shapeList.Remove(selectedShape);
            Destroy(selectedShape);
            ChangeSelection(null);
        }
    }

    //TODO maybe this can just disable the previous and enable the current
    public void ChangeSelection(GameObject selectedShape)
    {
        this.selectedShape = selectedShape;
        foreach ( GameObject shape in shapeList)
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

    public GameObject GetSelectedShape()
    {
        return selectedShape;
    }

    public int GetShapeCount()
    {
        return shapeList.Count;
    }
}
