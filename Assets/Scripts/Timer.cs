using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float length;
    public bool triggered { get; private set; }
    private bool started;

    void Awake()
    {
        started = false;
        triggered = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            int minutes = Mathf.FloorToInt(length / 60);
            int seconds = Mathf.FloorToInt(length % 60);
            GetComponent<Text>().text = minutes.ToString("00") + ":" + seconds.ToString("00");
            if ((length -= Time.deltaTime) < 0)
            {
                triggered = true;
                started = false;
            }
        }
    }

    public void StartTimer(float length)
    {
        this.length = length;
        started = true;
    }
}
