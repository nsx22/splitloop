using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    
    [Header("Timer Settings")]
    public float currentTime;
    public float startTime;
    public bool countDown = true;
    public bool isCounting = false;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeformats = new Dictionary<TimerFormats, string>();

    public Animator timerAnim;
    public LavaController lc;
    public AudioSource timerResetSound;

    void Start()
    {
        PlayerPrefs.SetInt("LastSave", SceneManager.GetActiveScene().buildIndex);
        timeformats.Add(TimerFormats.Whole, "0");
        timeformats.Add(TimerFormats.TenthDecimal, "0.0");
        timeformats.Add(TimerFormats.HundrethsDecimal, "0.00");
        currentTime = startTime;
    }


    void Update()
    {
        if (isCounting)
        {
            currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        }

        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            enabled = false;

            StartCoroutine(lc.Death());
        }
        
        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeformats[format]) : currentTime.ToString();
    }
    
    public void TimerStop()
    {
        isCounting = false;
    }
    public void TimerStart()
    {
        enabled = true;
        isCounting = true;
    }
    public void TimerReset()
    {
        currentTime = startTime;
        TimerStart();
    }
    public void TimerRanOut()
    {
        
    }
    
    
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethsDecimal
}