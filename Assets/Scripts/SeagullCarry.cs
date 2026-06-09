using UnityEngine;

public class SeagullCarry : MonoBehaviour
{
    public Transform carryPoint; // Empty object under the seagull where the ball should stick
   
    private GameObject carriedPlayer;
    private Rigidbody playerRb;

    public AudioClip seagullSFX;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && carriedPlayer == null && collision.gameObject.transform.localScale.x < 1.9f)
        {
            audioSource.clip = seagullSFX;
            audioSource.Play();

            carriedPlayer = collision.gameObject;
            playerRb = carriedPlayer.GetComponent<Rigidbody>();
            
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            playerRb.isKinematic = true;

            carriedPlayer.transform.SetParent(carryPoint);
            carriedPlayer.transform.localPosition = Vector3.zero;
            carriedPlayer.transform.localRotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropZone") && carriedPlayer != null)
        {
            carriedPlayer.transform.SetParent(null);
          
            playerRb.isKinematic = false;

            Invoke(nameof(RegrabTimer), 2f);
        }
    }

    private void RegrabTimer()
    {
        carriedPlayer = null;
        playerRb = null;
    }
}
