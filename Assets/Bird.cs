using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameObject LeftWing;
    public GameObject RightWing;
    // Start is called before the first frame update
    void Start()
    {
        LeftWing.GetComponent<Animation>().Play();
        RightWing.GetComponent<Animation>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z + 5 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Penguin" || other.tag == "Bear")
        {
            Destroy(gameObject);
        }
    }
}
