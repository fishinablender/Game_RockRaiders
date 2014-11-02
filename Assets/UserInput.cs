using UnityEngine;
using System.Collections;
using System;

public class UserInput : MonoBehaviour {

    public GameObject selGO;
    public bool selRock = false;
    public bool selGround = false;
    public bool selNPC = false;
    public Point selPoint;
    private Texture selTexture;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        CheckSel();
	
	}

    /*
     * Check if a rock is selected on the 3d map
     */
    private void CheckSel()
    {

        // Check user selection
        if (Input.GetMouseButtonDown(0))
        {

            //revert rock texture if mouse is clicked, guilty until proven innocent
            if (selRock || selGround)
            {
                selRock = false;
                selGround = false;
                selGO.transform.renderer.material.color = new Color32(255, 255, 255, 0);
                selGO.transform.renderer.material.mainTexture = selTexture;
                
            }

            //get raycast from mouse pointer and clicked gameobject
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if ray hits a gameobject
            if (Physics.Raycast(ray, out hit, 200))
            {

                // if clicked rock
                if (hit.transform.gameObject.CompareTag("Rock"))
                {

                    selRock = true;
                    selGO = hit.transform.gameObject;

                    // get coordinates from name
                    string[] str = selGO.name.Split(',');

                    selPoint = new Point(int.Parse(str[0]), int.Parse(str[1]));

                    selTexture = selGO.transform.renderer.material.mainTexture;

                    // edit selected texture to make look selected
                    selGO.transform.renderer.material.color = new Color32(0, 255, 0, 0);
                    selGO.transform.renderer.material.mainTexture = selTexture;

                }
                else if (hit.transform.gameObject.CompareTag("Ground"))
                {

                    selGround = true;
                    selGO = hit.transform.gameObject;

                    // get coordinates from name, ground tiles have a 3rd field not used
                    string[] str = selGO.name.Split(',');

                    selPoint = new Point(int.Parse(str[0]), int.Parse(str[1]));

                    selTexture = selGO.transform.renderer.material.mainTexture;

                    //selGO.transform.renderer.material.mainTexture = new Texture();


                    // edit selected texture to make look selected
                    selGO.transform.renderer.material.color = new Color32(0,255,0,0);
                    selGO.transform.renderer.material.mainTexture = selTexture;
                }
                else
                {
                    //something non-rock clicked
                    if (selRock)
                    {
                        selRock = false;
                        selGO.transform.renderer.material.mainTexture = selTexture;
 
                    }

                    

                }
            }
        }

    }
}
