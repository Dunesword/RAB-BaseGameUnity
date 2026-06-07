using UnityEngine;
using System.Collections.Generic;

public class Crusher : MonoBehaviour
{

    private List<ContactPoint> contactPoints = new List<ContactPoint>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (other.gameObject.CompareTag("Crusher"))
        {
            UpdateContacts(collision);
        }      
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crusher"))
        {
            contactPoints.RemoveAll(contact =>
                contact.otherObject == collision.gameObject);

            UpdateContacts(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crusher"))
        {
            contactPoints.RemoveAll(contact =>
            contact.otherObject == collision.gameObject);
        }        
    }

    private void UpdateContacts(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            contactPoints.Add(contact);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
