using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GUI_Map3D : MonoBehaviour
{
    public GameObject[] rocks = new GameObject[5];


    public Controller con;
    public MapSpace[,] map;

    public Texture[] textures = new Texture[9];

    private List<GameObject> movingTiles = new List<GameObject>();
    private Vector2 curOffset;



    // Use this for initialization
    void Start()
    {

        // get instance of controller and required components
        con = gameObject.GetComponent<Controller>() as Controller;
        map = con.map.grid;

        //set shared materials
        SetTextures();

        // build 3d map
        BuildMap();

    }

    private void SetTextures()
    {

        // Sets textures
        textures[0] = Resources.Load("Textures/Ground") as Texture;
        textures[1] = Resources.Load("Textures/Water") as Texture;
        textures[2] = Resources.Load("Textures/Lava") as Texture;
        textures[3] = Resources.Load("Textures/Rubble") as Texture;
        textures[4] = Resources.Load("Textures/Loose") as Texture;
        textures[5] = Resources.Load("Textures/OreSeam") as Texture;
        textures[6] = Resources.Load("Textures/CrystalSeam") as Texture;
        textures[7] = Resources.Load("Textures/Hard") as Texture;
        textures[8] = Resources.Load("Textures/Solid") as Texture;

    }

    private void BuildMap()
    {

        List<GameObject> cubes = new List<GameObject>();

        GameObject mapBase = new GameObject();
        mapBase.name = "Ground Tiles";

        //temp cube tile holder
        GameObject cube;





        // Build Ground Tiles
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {

                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.tag = "Ground";
                cube.transform.name = x + "," + y +",Ground";
                

                switch (map[x, y].rock)
                {
                    case rockType.WATER:
                        cube.renderer.material.mainTexture = textures[1];
                        movingTiles.Add(cube);
                        break;
                    case rockType.LAVA:
                        // add light
                        GameObject light = new GameObject();
                        light.AddComponent<Light>();
                        light.light.color = Color.white;
                        light.transform.position = new Vector3(x,1,y);

                        cube.renderer.material.mainTexture = textures[2];
                        movingTiles.Add(cube);
                        break;
                    default:
                        cube.renderer.sharedMaterial.mainTexture = textures[0];
                        break;
                }



                cube.gameObject.transform.position = new Vector3(x, 0, y);

                cube.transform.parent = mapBase.transform;

                cubes.Add(cube);


            }
        }

        // batch ground tiles into single mesh
        StaticBatchingUtility.Combine(cubes.ToArray(), mapBase);
        cubes.Clear();


        // Build Rock Tiles
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {

                if (!map[x, y].rock.Equals(rockType.EMPTY) && 
                    !map[x, y].rock.Equals(rockType.LAVA) && 
                    !map[x, y].rock.Equals(rockType.WATER))
                {

                    cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Rock";
                    cube.transform.name = x + "," + y;



                    if (map[x, y].rock == rockType.RUBBLE)
                    {
                        cube.renderer.material.mainTexture = textures[3];
                        cube.gameObject.transform.position = new Vector3(x, 0.01f, y);
                    }
                    else if (map[x, y].rock == rockType.LOOSE)
                    {
                        cube.renderer.material.mainTexture = textures[4];
                        cube.gameObject.transform.position = new Vector3(x, 1, y);
                    }
                    else if (map[x, y].rock == rockType.HARD)
                    {
                        cube.renderer.material.mainTexture = textures[7];
                        cube.gameObject.transform.position = new Vector3(x, 1, y);
                    }
                    else if (map[x, y].rock == rockType.SOLID)
                    {
                        cube.renderer.material.mainTexture = textures[8];
                        cube.gameObject.transform.position = new Vector3(x, 1, y);
                    }

                    cube.transform.parent = mapBase.transform;
                    cubes.Add(cube);

                    con.map.grid[x, y].g = cube;

                }

            }

        }

        /////////////////// BATCHES BUT DOESNT SAVE DRAW CALLS, FIX LATER FOR PERFORMANCE BOOST ////////////////////
        // batch ground tiles into single mesh
        StaticBatchingUtility.Combine(cubes.ToArray(), mapBase);
        cubes.Clear();


        /*
        ///////////////////     BROKEN       ////////////////////////////
        // Build Rock Tiles with proper geometry and direction
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {


                //calculate number of free sides - Top right bottom left
                int[] freeSides = {0,0,0,0};


                // check for edge conditions then check other sides
                // right
                if (x >= map.GetLength(0) - 1)
                {
                    freeSides[1] = 1;
                }
                else
                {
                    // get right rock
                    switch (map[x + 1, y].rock)
                    {

                        case rockType.EMPTY:
                            break;
                        case rockType.WATER:
                            break;
                        case rockType.LAVA:
                            break;
                        case rockType.RUBBLE:
                            break;
                        default:
                            freeSides[0] = 1;
                            break;
                    }
                }

                // left
                if (x < 1)
                {
                    freeSides[3] = 1;
                }
                else
                {

                    // get left rock
                    switch (map[x - 1, y].rock)
                    {

                        case rockType.EMPTY:
                            break;
                        case rockType.WATER:
                            break;
                        case rockType.LAVA:
                            break;
                        case rockType.RUBBLE:
                            break;
                        default:
                            freeSides[0] = 1;
                            break;
                    }
                }


                // bottom
                if (y >= map.GetLength(1) - 1)
                {
                    freeSides[2] = 1;
                }
                else
                {
                    // get bottom rock
                    switch (map[x, y+1].rock)
                    {

                        case rockType.EMPTY:
                            break;
                        case rockType.WATER:
                            break;
                        case rockType.LAVA:
                            break;
                        case rockType.RUBBLE:
                            break;
                        default:
                            freeSides[0] = 1;
                            break;
                    }
                }

                // top
                if (y < 1)
                {
                    freeSides[0] = 1;
                }
                else
                {
                    // get top rock
                    switch (map[x, y - 1].rock)
                    {

                        case rockType.EMPTY:
                            break;
                        case rockType.WATER:
                            break;
                        case rockType.LAVA:
                            break;
                        case rockType.RUBBLE:
                            break;
                        default:
                            freeSides[0] = 1;
                            break;
                    }
                }

                int totalSides = 0;

                for (int i = 0; i < freeSides.Length; i++)
                {
                    totalSides += freeSides[i];

                }


                switch(totalSides){

                    case 0:
                        cube = Resources.Load("Prefabs/Cube_Middle") as GameObject;
                        break;
                    case 1:
                        cube = Resources.Load("Prefabs/Cube_Middle") as GameObject;
                        break;
                    case 2:
                        cube = Resources.Load("Prefabs/Cube_OutsideCorner") as GameObject;
                        break;
                    case 3:
                        cube = Resources.Load("Prefabs/Cube_Side") as GameObject;
                        break;
                    case 4:
                        cube = Resources.Load("Prefabs/Cube_Middle") as GameObject;
                        break;
                    default: 
                        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        break;


                }

                //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject.Instantiate(cube);



                    if (map[x, y].rock == rockType.RUBBLE)
                    {
                        cube.renderer.material.mainTexture = Resources.Load("Textures/Rubble") as Texture;
                        cube.gameObject.transform.position = new Vector3(x, 0.01f, y);
                    }
                    else if (map[x, y].rock == rockType.LOOSE)
                    {
                        cube.renderer.material.mainTexture = Resources.Load("Textures/Loose") as Texture;
                        cube.gameObject.transform.position = new Vector3(x, 1, y);
                    }
                    else if (map[x, y].rock == rockType.HARD)
                    {
                        cube.renderer.material.mainTexture = Resources.Load("Textures/Hard") as Texture;
                        cube.gameObject.transform.position = new Vector3(x, 1, y);
                    }
                    else if (map[x, y].rock == rockType.SOLID)
                    {

                        cube.renderer.material.mainTexture = Resources.Load("Textures/Solid") as Texture;
                        cube.gameObject.transform.position = new Vector3(x, 1, y);
                        
                    }

                    //cube.transform.parent = mapBase.transform;
                    cubes.Add(cube);
                

            }
        }*/



    }

    // Update is called once per frame
    void Update()
    {

        DynamicMaterialsUpdate();

    }

    /*
     * Make Water and Lava shift texture corrdinates to make an illusion of movement
     */
    private void DynamicMaterialsUpdate()
    {

        // offset each texture in moving tiles
        foreach (GameObject g in movingTiles)
        {
            //get current offset
            curOffset = g.transform.renderer.material.GetTextureOffset("_MainTex");

            //set new offset
            g.transform.renderer.material.SetTextureOffset("_MainTex", new Vector2(curOffset.x + 0.1f * Time.deltaTime, curOffset.y + 0.1f * Time.deltaTime));

            //set new scale
            g.transform.renderer.material.SetTextureScale("_MainTex", new Vector2(0.7f + Mathf.PingPong(Time.time, 0.3f) * 0.01f, 0.7f + Mathf.PingPong(Time.time, 0.3f) * 0.01f));

        }

    }
}
