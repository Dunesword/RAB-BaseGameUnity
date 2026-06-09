using UnityEngine;
using TMPro;

public class DisplayTimes : MonoBehaviour
{

    public TMP_Text[] levelTimes;
    public TMP_Text[] bestLevelTimes;
    public TMP_Text totalTimeText;
    private float totalTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalTime = 0f;
        for (int i = 1; i <= levelTimes.Length; i++)
        {
            if (PlayerPrefs.HasKey("LatestTimeLevel" + i))
            {
                levelTimes[i].text = TimerToString(PlayerPrefs.GetFloat("LatestTimeLevel" + i));
                totalTime += PlayerPrefs.GetFloat("LatestTimeLevel" + i);
            }

            if (PlayerPrefs.HasKey("BestTimeLevel" + i))
            {
                bestLevelTimes[i].text = bestLevelTimes[i].text.Substring(0, bestLevelTimes[i].text.IndexOf(':') + 2) + TimerToString(PlayerPrefs.GetFloat("BestTimeLevel" + i));
            }
        }
        totalTimeText.text = totalTimeText.text.Substring(0, totalTimeText.text.IndexOf(':') + 2) + TimerToString(totalTime);
    }

    string TimerToString(float time)
    {

        string min = ((int)time / 60).ToString();     // calculates minutes
        string sec = (time % 60).ToString("00");

        return min + ":" + sec;
    }
}
