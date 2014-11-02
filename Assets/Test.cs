using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public Controller con;

	public Texture2D[] rockPics = new Texture2D[5];
	public Texture2D[] npcPics = new Texture2D[4];

	// Use this for initialization
	void Start () {


        runTest();

		


	}

    private void runTest()
    {
        con = gameObject.AddComponent<Controller>();

        //con.map = new Map(new Point(20, 20));
        con.map.ClearMap();
        con.map.AddSolidRockBoarder();

        NPC n = gameObject.AddComponent<NPC>();
        n.location = new Point(5, 5);

        con.npcs.Add(n);

        con.map.grid[5, 5].rock = rockType.BUILDING;
        con.map.grid[7, 7].rock = rockType.WATER;
        con.map.grid[7, 8].rock = rockType.WATER;
        con.map.grid[8, 7].rock = rockType.WATER;

        con.map.grid[7, 3].rock = rockType.LAVA;
        con.map.grid[8, 3].rock = rockType.LAVA;
        con.map.grid[9, 3].rock = rockType.LAVA;
        con.map.grid[8, 4].rock = rockType.LAVA;
        con.map.grid[7, 2].rock = rockType.LAVA;
        con.map.grid[8, 2].rock = rockType.LAVA;
        con.map.grid[9, 2].rock = rockType.LAVA;
        con.map.grid[10, 2].rock = rockType.LAVA;
        con.map.grid[9, 4].rock = rockType.LAVA;

        con.map.AddBuilding(new Point(6, 5), Building.buildings.PAD);

        con.map.grid[2, 2].rock = rockType.LOOSE;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

        //TempMapGui();

        GUI.Label(new Rect(0, 0, 500, 100), "  v0.0.1 Pre-Alpha ");

	}

    private void TempMapGui()
    {

        // TEMP GUI MAP DISPLAY
        for (int y = 0; y < con.map.size.Y; y++)
        {

            for (int x = 0; x < con.map.size.X; x++)
            {

                if (con.map.grid[x, y].rock == rockType.EMPTY)
                {
                    GUI.DrawTexture(new Rect(x * 64, y * 64, 64, 64), rockPics[0]);
                }
                else if (con.map.grid[x, y].rock == rockType.RUBBLE)
                {
                    GUI.DrawTexture(new Rect(x * 64, y * 64, 64, 64), rockPics[2]);
                }
                else if (con.map.grid[x, y].rock == rockType.LOOSE)
                {
                    GUI.DrawTexture(new Rect(x * 64, y * 64, 64, 64), rockPics[3]);
                }
                else if (con.map.grid[x, y].rock == rockType.SOLID)
                {
                    GUI.DrawTexture(new Rect(x * 64, y * 64, 64, 64), rockPics[4]);
                }
                else
                {
                    GUI.DrawTexture(new Rect(x * 64, y * 64, 64, 64), rockPics[1]);
                }

            }

        }

        //temp display npcs
        foreach (NPC n in con.npcs)
        {

            GUI.DrawTexture(new Rect(n.location.X * 64, n.location.Y * 64, 64, 64), npcPics[0]);
        }

        // Early gui test, uses ints as rock types
        //GUI.Label(new Rect(0,0,Screen.width,Screen.height),map.ToString());

    }
}
