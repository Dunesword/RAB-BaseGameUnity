using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //needed to restart the game when the player enters the death zone (trigger event)
using TMPro;

public class PlayerController : MonoBehaviour
{

    //These public variables are initialized in the Inspector
    public float speed;
    public float maxSpeed = 8.0f;
    public TMP_Text countText;
    public TMP_Text winText;
    public TMP_Text timeText;  //  variable to display the timer text in Unity
    public float startingTime;  // variable to hold the game's starting time
    public string min;
    public string sec;
    public GameObject gameOverPanel;
    public Camera mainCamera;

    //These private variables are initialized in the Start
    private Rigidbody rb;
    private int count;
    private bool gameOver; //  bool to define game state on or off.

    // Audio
    public AudioClip coinSFX;
    public AudioClip potionSFX;
    public AudioClip waterSFX;
    public AudioClip potionUseSFX;
    public AudioClip boxBreakSFX;
    private AudioSource audioSource;

    //Items
    public int[] potions;
    public TMP_Text[] potionsText;

    //Animators
    public Animator doorAnimator;
    public Animator gameOverPanelAnimator; // fade-in animation

    //For movement based on camera
    Transform cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winText.text = "";
        startingTime = Time.time;
        gameOver = false;
        potions = new int[] {0, 0};
        cam = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();  // access the audio source component of player
        Time.timeScale = 1;
        SetCountText();

    }
    private void Update()
    {
        PlayerInput();

        if (gameOver) // condition that the game is NOT over; returns the false value
            return;
        float timer = Time.time - startingTime;     // local variable to updated time
        min = ((int)timer / 60).ToString();     // calculates minutes
        sec = (timer % 60).ToString("f0");      // calculates seconds

        timeText.text = min + ":" + sec;     // update UI time text
    }

    void FixedUpdate()
    {
        Vector3 camPosition = new Vector3(cam.position.x, transform.position.y, cam.position.z);
        Vector3 direction = (transform.position - camPosition).normalized;

        Vector3 forwardMovement = direction * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = cam.right * Input.GetAxis("Horizontal");

        Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);

        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(movement * speed, ForceMode.Force);
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //This event/function handles trigger events (collsion between a game object with a rigid body)
   
        if (other.gameObject.tag == "PickUp")
        {
            Destroy(other.gameObject);
            count++;
            SetCountText();

            //PLAY SOUND EFFECT
            audioSource.clip = coinSFX;
            audioSource.Play();

        }

        if (other.gameObject.CompareTag("DeathZone"))
        {
            rb.linearVelocity = new Vector3(0f, 0f, 0f);
            audioSource.clip = waterSFX;
            audioSource.Play();
            Destroy(mainCamera.GetComponent<CameraController>());
            Destroy(gameObject, 2.6f);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameOverPanel.SetActive(true);
        }

        if (other.gameObject.CompareTag("Shrink"))
        {
            potions[0]++;
            PlayPotionoAudio();
            SetCountText();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Grow"))
        {
            potions[1]++;
            PlayPotionoAudio();
            SetCountText();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Net"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * -1.2f, rb.linearVelocity.z);
            }

            else
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * -0.8f, rb.linearVelocity.z);
            }
        }

        if (other.gameObject.CompareTag("BreakBox") && (transform.localScale.x > 1.2f))
        {
            audioSource.PlayOneShot(boxBreakSFX, 0.1f);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("LevelTwoEntrance"))
        {
            SceneManager.LoadScene("WIN");
        }

    }


    //Updates UI
    void SetCountText()
    {
        countText.text = count.ToString() + " / 10";
        if(count >= 10)
        {
            gameOver = true; // returns true value to signal game is over
            timeText.color = Color.green;  // changes timer's color
            doorAnimator.SetTrigger("OpenDoor");

            PlayerPrefs.SetFloat("LatestTimeLevel" + SceneManager.GetActiveScene().buildIndex, Time.time - startingTime);

            if (PlayerPrefs.GetFloat("LatestTimeLevel" + SceneManager.GetActiveScene().buildIndex) < PlayerPrefs.GetFloat("BestTimeLevel" + SceneManager.GetActiveScene().buildIndex, float.MaxValue))
            {
                PlayerPrefs.SetFloat("BestTimeLevel" + SceneManager.GetActiveScene().buildIndex, PlayerPrefs.GetFloat("LatestTimeLevel" + SceneManager.GetActiveScene().buildIndex));
            }

            PlayerPrefs.Save();
            
            //winText.text = "You win!";
            //Time.timeScale = 0;
        }

        for (int i = 0; i < potions.Length; i++)
        {
            potionsText[i].text = potionsText[i].text.Substring(0, potionsText[i].text.IndexOf(' ') + 1) + potions[i].ToString();
        }
    }

    void PlayerInput()
    {

        //Shrink
        if (Input.GetKeyDown("q") && (potions[0] > 0) && (transform.localScale.x > 0.6f))
        {           
            potions[0]--;
            transform.localScale = new Vector3((float)Math.Round(transform.localScale.x - 0.7f, 1), (float)Math.Round(transform.localScale.y - 0.7f, 1), (float)Math.Round(transform.localScale.z - 0.7f, 1));
            PlayPotionoUseAudio();
            SetCountText();            
        }

        //Grow
        if (Input.GetKeyDown("e") && (potions[1] > 0) && (transform.localScale.x < 1.8f))
        {
            potions[1]--;
            transform.localScale = new Vector3((float)Math.Round(transform.localScale.x + 0.7f, 1), (float)Math.Round(transform.localScale.y + 0.7f, 1), (float)Math.Round(transform.localScale.z + 0.7f, 1));
            PlayPotionoUseAudio();
            SetCountText();
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.1f))
        {
            rb.AddForce(new Vector3(0.0f, 6.75f, 0.0f), ForceMode.Impulse);
        }
    }

    void PlayPotionoAudio()
    {
        audioSource.clip = potionSFX;
        audioSource.Play();
    }

    void PlayPotionoUseAudio()
    {
        audioSource.clip = potionUseSFX;
        audioSource.Play();
    }
}
