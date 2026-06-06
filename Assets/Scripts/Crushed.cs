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
        {
            crusherRb = other.gameObject.GetComponent<Rigidbody>();
            touchingCrusher = true;
        }
            

        if (other.CompareTag("Wall"))
        {
            touchingWall = true;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            touchingCrusher = false;
        }
            

        if (other.CompareTag("Wall"))
        {
            touchingWall = false;
        }
            
    }

    private void FixedUpdate()
    {
        if (!touchingCrusher || !touchingWall)
            return;

        

        Vector3 directionToPlayer = (transform.position - crusherRb.position).normalized;
        float speedTowardPlayer = Vector3.Dot(crusherRb.linearVelocity, directionToPlayer);

        if (speedTowardPlayer > minCrushSpeed)
        {
            pc.deathText.text = "You got crushed";
            pc.Death();
            Destroy(gameObject);
        }
    }
}


/*if (crusherRb == null)
        {
            pc.deathText.text = "You got crushed";
            pc.Death();
            Destroy(gameObject);
            return;
        }*/