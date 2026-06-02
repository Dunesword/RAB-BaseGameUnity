using UnityEngine;

public class SeagullCarry : MonoBehaviour
{
    public Transform carryPoint; // Empty object under the seagull where the ball should stick
   
    private GameObject carriedPlayer;
    private Rigidbody playerRb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && carriedPlayer == null)
        {
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
        }
    }
}
