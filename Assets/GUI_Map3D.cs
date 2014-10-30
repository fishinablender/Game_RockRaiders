using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GUI_Map3D : MonoBehaviour
{
    public GameObject[] rocks = new GameObject[5];


    public Controller con;
    public MapSpace[,] map;

    // Use this for initialization
    void Start()
    {

        // get instance of controller and required components
        con = gameObject.GetComponent<Controller>() as Controller;
        map = con.map.grid;

        // build 3d map
        BuildMap();

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

                switch (map[x, y].rock)
                {
                    case rockType.LAVA:
                        //cube.renderer.sharedMaterial.mainTexture = Resources.Load("Textures/Lava") as Texture;
                        cube.renderer.material.mainTexture = Resources.Load("Textures/Lava") as Texture;
                        break;
                    case rockType.WATER:
                        cube.renderer.material.mainTexture = Resources.Load("Textures/Water") as Texture;
                        break;
                    default:
                        cube.renderer.sharedMaterial.mainTexture = Resources.Load("Textures/Ground") as Texture;
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


        /*

        // Build Rock Tiles
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

    }
}
