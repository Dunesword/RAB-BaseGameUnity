using Unity.Collections;
using UnityEngine;

public class CircularPath : MonoBehaviour
{
    public Transform nest; //Traget object to circle around
    public float speed;
    public float radius; 
    public float angle; // where the object is on the path

    // Update is called once per frame
    void Update()
    {
        Movement();   
    }

    void Movement()
    {

        // Calculates the new position of the object using Mathf.Cos and Mathf.Sin functions
        float x = nest.position.x + Mathf.Cos(angle) * radius;
        float y = nest.position.y;
        float z = nest.position.z + Mathf.Sin(angle) * radius; 

        // Updates the object's position
        transform.position = new Vector3(x, y, z);

        // Increments object angle
        angle += speed * Time.deltaTime;
    }
}
