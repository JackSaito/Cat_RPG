using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinnerScript : MonoBehaviour
{
    public float xSpeed = 0f;
    public float ySpeed = 0f;
    public float zSpeed = 0f;
    public float totalSpeed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(xSpeed, ySpeed, zSpeed);
        transform.Rotate(target * totalSpeed * Time.deltaTime);
    }
}
