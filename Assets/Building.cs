using UnityEngine;
using System.Collections;



public class Building : MonoBehaviour
{

    public enum buildings { PAD, TOOL, TELE_S, POWER, SUPPORT, DOCKS, TELE_L, ORE_REF, LASER, GEO };

    // define Building shapes using 2d arrays
    int[,] Building_PAD = { { 1 } };
    int[,] Building_TOOL = { { 2 }, { 1 } };
    int[,] Building_TELE_S = { { 2 }, { 1 } };
    int[,] Building_POWER = { { 2, 2 }, { 1, 0 } };
    int[,] Building_SUPPORT = { { 2 }, { 1 } };
    int[,] Building_DOCKS = { { 2 }, { 1 }, { 1 } };
    int[,] Building_TELE_L = { { 2, 2 }, { 1, 1 } };
    int[,] Building_ORE_REF = { { 2 }, { 2 }, { 1 } };
    int[,] Building_LASER = { { 1 } };
    int[,] Building_GEO = { { 2 }, { 1 } };



    public buildings buildingType = buildings.PAD;
    public Point pos = new Point(0, 0);

    // 0 = 0 degrees, 1 = 90 degrees, 2 = 180 degrees, 3 = 270 degrees
    public int rotation = 0;


    // Empty Constructor
    public Building()
    {

    }

    // Constructor
    public Building(buildings b, Point p, int rot)
    {

        buildingType = b;
        pos = p;
        rotation = rot;
    }











    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
