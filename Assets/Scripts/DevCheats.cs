using UnityEngine;
using UnityEngine.SceneManagement;

public class DevCheats : MonoBehaviour
{

    public PlayerController pc;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(2);
        }

        if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(4);
        }

        if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown("c") && pc != null)
        {
            pc.CoinCollected();
        }

        if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown("z") && pc != null)
        {
            pc.GrowPotionCollected();
        }

        if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown("x") && pc != null)
        {
            pc.ShrinkPotionCollected();
        }
    }
}
