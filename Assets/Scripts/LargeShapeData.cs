using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newLargeShape", menuName = "Data/LargeShapeData")]
public class LargeShapeData : ScriptableObject
{
    [Header("Properties")]
    [Tooltip("The sprite rendered for this large shape.")]
    public Sprite sprite;
    [Tooltip("The amount of time (in seconds) to solve this shape.")]
    public float timeToComplete = 300f;
    [Tooltip("The max amount of shapes you can use before you start to get penalized.")]
    public int shapeThreshold = 3;
}
