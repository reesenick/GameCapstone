using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Tooltip("Offset from the player we'll follow at (based on the camera's position and the player's position)")]
    [SerializeField] private Vector3 offset;

    [Tooltip("Character we are following")]
    [SerializeField] private GameObject following;


    private void Awake()
    {
       if (following == null) {
           Debug.LogError("You didn't assign something to follow!");
       }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (following == null) {
            Debug.LogError("You didn't assign something to follow!");
        }    
        transform.position = new Vector3(following.transform.position.x, following.transform.position.y, transform.position.z); 
    }

    // // Update is called once per frame
    // private void Update()
    // {
    // transform.position = new Vector3(following.transform.position.x, following.transform.position.y, transform.position.z);

    // }
}


