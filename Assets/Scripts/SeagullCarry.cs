using UnityEngine;

public class SeagullCarry : MonoBehaviour
{
    public Transform carryPoint; // Empty object under the seagull where the ball should stick

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            playerRb.isKinematic = true;

            collision.gameObject.transform.SetParent(carryPoint);
            collision.gameObject.transform.localPosition = Vector3.zero;
            collision.gameObject.transform.localRotation = Quaternion.identity;
        }
    }
}
