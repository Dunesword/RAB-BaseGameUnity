using UnityEngine;
using TMPro;

public class DisplayTimes : MonoBehaviour
{

    public TMP_Text[] levelTimes;
    public TMP_Text[] bestLevelTimes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < levelTimes.Length; i++)
        {
            string min = ((int)PlayerPrefs.GetFloat("LatestTimeLevel" + i) / 60).ToString();     // calculates minutes
            string sec = (PlayerPrefs.GetFloat("LatestTimeLevel" + i) % 60).ToString("f0");      // calculates seconds

            levelTimes[i].text = levelTimes[i].text.Substring(0, levelTimes[i].text.IndexOf(':') + 2) + min + ":" + sec;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
