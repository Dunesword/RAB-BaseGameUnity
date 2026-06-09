using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuTransitionController : MonoBehaviour
{
    public Animator transitionAnimator;
    public GameObject transitionImage;
    AudioSource audioSource;
    public AudioClip continueSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transitionImage.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickStart()
    {
        transitionImage.SetActive(true); // On press start the fade in image with be set active and thefade in anim will play
    }

    void OnSpaceBarPress()
    {
        transitionAnimator.SetTrigger("SpaceBarPress");  // sets fade out anim trigger
        audioSource.clip = continueSound; 
        audioSource.Play(); // Plays transition sound
        Invoke("SceneLoad", 1.5f); // loads scene after text has faded out

    }

    void SceneLoad()
    {
        SceneManager.LoadScene("LevelOne");
    }


    // Update is called once per frame
    void Update()
    {
        if (transitionImage) // checks if transition image is set active (I think)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSpaceBarPress(); // if player presses space and image is active, this will be called
            }
        }
    }
}
