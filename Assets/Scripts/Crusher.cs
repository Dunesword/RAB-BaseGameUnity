using UnityEngine;
using System.Collections.Generic;

public class Crusher : MonoBehaviour
{

    public PlayerController pc;

    private List<ContactPoint> contactPoints = new List<ContactPoint>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crusher"))
        {
            UpdateContacts(collision);
        }      
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crusher"))
        {
            RemoveContacts(collision);
            UpdateContacts(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crusher"))
        {
            RemoveContacts(collision);
        }        
    }

    private void RemoveContacts(Collision collision)
    {
        contactPoints.RemoveAll(contact =>
            contact.otherCollider == collision.collider);

        /*contactPoints.RemoveAll(contact =>
                contact.otherCollider == collision.gameObject);*/
    }

    private void UpdateContacts(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            contactPoints.Add(contact);
        }
    }

    void FixedUpdate()
    {
        if (CrushCheck())
        {
            pc.deathText.text = "You got crushed";
            pc.Death();
            Destroy(gameObject);
        }
    }

    bool CrushCheck()
    {
        Vector3 playerCenter = transform.position;

        for (int i = 0; i < contactPoints.Count; i++)
        {
            Vector3 dirA = (contactPoints[i].point - playerCenter).normalized;

            for (int j = i + 1; j < contactPoints.Count; j++)
            {
                Vector3 dirB = (contactPoints[j].point - playerCenter).normalized;

                float dot = Vector3.Dot(dirA, dirB);

                // -1 means perfectly opposite
                //  0 means perpendicular
                //  1 means same direction
                if (dot < -0.75f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
