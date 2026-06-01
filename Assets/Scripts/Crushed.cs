using UnityEngine;

public class Crushed : MonoBehaviour
{

    public PlayerController pc;

    private Rigidbody crusherRb;
    private float minCrushSpeed = 0.2f;

    private bool touchingCrusher;
    private bool touchingWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
            touchingCrusher = true;

        if (other.CompareTag("Wall"))
            touchingWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
            touchingCrusher = false;

        if (other.CompareTag("Wall"))
            touchingWall = false;
    }

    private void FixedUpdate()
    {
        if (!touchingCrusher || !touchingWall)
            return;

        if (crusherRb == null)
        {
            pc.Death();
            Destroy(gameObject);
            return;
        }

        Vector3 directionToPlayer = (transform.position - crusherRb.position).normalized;
        float speedTowardPlayer = Vector3.Dot(crusherRb.linearVelocity, directionToPlayer);

        if (speedTowardPlayer > minCrushSpeed)
        {
            pc.Death();
            Destroy(gameObject);
        }
    }
}
