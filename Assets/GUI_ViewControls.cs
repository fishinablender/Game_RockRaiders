using UnityEngine;
using System.Collections;

public class GUI_ViewControls : MonoBehaviour {

    public Camera c = Camera.main;
    public float speed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            c.transform.position = new Vector3((c.transform.position.x) + speed*Time.deltaTime, c.transform.position.y, c.transform.position.z);

        }
        else if (Input.GetAxis("Horizontal") < -0.1f)
        {
            c.transform.position = new Vector3((c.transform.position.x) - speed * Time.deltaTime, c.transform.position.y, c.transform.position.z);
        }

        if (Input.GetAxis("Vertical") > 0.1f)
        {
            c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, (c.transform.position.z) + speed * Time.deltaTime);

        }
        else if (Input.GetAxis("Vertical") < -0.1f)
        {
            c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, (c.transform.position.z) - speed * Time.deltaTime);
        }

	}
}
