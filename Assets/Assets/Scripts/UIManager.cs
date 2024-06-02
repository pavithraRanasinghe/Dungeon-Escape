using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance 
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager us null!!!!");
            }
            return _instance;
        }
    }

    public Text gemCount;
    public GameObject toolTip;
    public Image[] healthBars;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateGemCount(int count)
    {
        gemCount.text = "" + count;
    }

    public void HandleToolTip(bool state)
    {
        toolTip.SetActive(state);
    }

    public void UpdateLives(int livesRemaining)
    {
        for(int i = 0; i <= livesRemaining; i++)
        {
            if(i == livesRemaining)
            {
                healthBars[i].enabled = false;
            }
        }
    }
}
