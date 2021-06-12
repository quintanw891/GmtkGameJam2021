using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float length;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(length / 60);
        int seconds = Mathf.FloorToInt(length % 60);
        GetComponent<Text>().text = minutes.ToString("00") + ":" + seconds.ToString("00");
        if ((length -= Time.deltaTime) < 0)
        {
            SceneManager.LoadScene("ResultsScene");
        }
    }
}
