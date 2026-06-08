using UnityEngine;
using TMPro;

public class DisplayTimes : MonoBehaviour
{

    public TMP_Text[] levelTimes;
    public TMP_Text[] bestLevelTimes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 1; i <= levelTimes.Length; i++)
        {
            if (PlayerPrefs.HasKey("LatestTimeLevel" + i))
            {
                levelTimes[i].text = levelTimes[i].text.Substring(0, levelTimes[i].text.IndexOf(':') + 2) + TimerToString(PlayerPrefs.GetFloat("LatestTimeLevel" + i));
            }

            if (PlayerPrefs.HasKey("BestTimeLevel" + i))
            {
                bestLevelTimes[i].text = bestLevelTimes[i].text.Substring(0, bestLevelTimes[i].text.IndexOf(':') + 2) + TimerToString(PlayerPrefs.GetFloat("BestTimeLevel" + i));
            }
        }
    }

    string TimerToString(float time)
    {

        string min = ((int)time / 60).ToString();     // calculates minutes
        string sec = (time % 60).ToString("00");

        return min + ":" + sec;
    }
}
