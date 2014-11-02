using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum rockType { EMPTY, BUILDING, RUBBLE, LOOSE, HARD, SOLID, WATER, LAVA, SEAM_ORE, SEAM_CRYSTAL };
public enum itemType { EMPTY, ORE, CRYSTAL };

public class Map
{

    public Point size;
    public MapSpace[,] grid;

    public Map()
    {
    }

    public Map(Point s)
    {

        this.size = s;
        grid = new MapSpace[this.size.X, this.size.Y];
    }




    // set all rocks to empty
    public void ClearMap()
    {

        for (int y = 0; y < size.Y; y++)
        {
            for (int x = 0; x < size.X; x++)
            {

                grid[x, y] = new MapSpace(new Point(x, y), rockType.EMPTY);
                grid[x, y].rock = rockType.EMPTY;
                grid[x, y].items = new List<itemType>();

            }
        }

    }

    // Change rock r at position (x,y)
    public void ChangeRock(int x, int y, rockType r)
    {

        grid[x, y].rock = r;

    }

    // Change rock r at position p
    public void ChangeRock(Point p, rockType r)
    {

        grid[p.X, p.Y].rock = r;

    }

    //NEED TO FILL OUT
    public Map LoadMap(string mapName)
    {

        return new Map(new Point(10, 10));
    }

    // Add a solid rock boarder around current map
    public void AddSolidRockBoarder()
    {

        for (int y = 0; y < size.Y; y++)
        {
            for (int x = 0; x < size.X; x++)
            {

                if (x == 0 || x == 1 || x == size.X - 1 || x == size.X - 2 || y == 0 || y == 1 || y == size.Y - 1 || y == size.Y - 2)
                {

                    grid[x, y].rock = rockType.SOLID;
                }

            }
        }


    }



    // ToString override function, prints a grid of the map as ints
    public override string ToString()
    {

        string ret = "";

        for (int y = 0; y < size.Y; y++)
        {

            for (int x = 0; x < size.X; x++)
            {
                ret += (int)grid[x, y].rock + "  ";
            }

            ret += "\n";
        }

        return ret;
    }

    //check if rock can be mined
    public bool CheckMine(Point p)
    {

        MapSpace tempSel = grid[p.X, p.Y];

        if (tempSel.rock == rockType.LOOSE ||
            tempSel.rock == rockType.SEAM_ORE ||
            tempSel.rock == rockType.SEAM_CRYSTAL)
        {

            return true;
        }

        return false;
    }

    //check if rock can be exploded
    public bool CheckExplode(Point p)
    {

        MapSpace tempSel = grid[p.X, p.Y];

        if (tempSel.rock == rockType.LOOSE ||
            tempSel.rock == rockType.HARD ||
            tempSel.rock == rockType.SEAM_ORE ||
            tempSel.rock == rockType.SEAM_CRYSTAL)
        {

            return true;
        }

        return false;
    }

    //Mine the vector location and change rock type and items appropriately
    public void Mine(Point p)
    {

        MapSpace tempSel = grid[p.X, p.Y];

        if (tempSel.rock == rockType.LOOSE)
        {
            tempSel.rock = rockType.RUBBLE;
        }
        else if (tempSel.rock == rockType.HARD)
        {
            tempSel.rock = rockType.RUBBLE;
        }
        else if (tempSel.rock == rockType.SEAM_ORE)
        {
            tempSel.rock = rockType.RUBBLE;

            //add three ore for an ore seam
            tempSel.items.Add(itemType.ORE);
            tempSel.items.Add(itemType.ORE);
            tempSel.items.Add(itemType.ORE);

        }
        else if (tempSel.rock == rockType.SEAM_CRYSTAL)
        {
            tempSel.rock = rockType.RUBBLE;

            //add three crystals for a crystal seam
            tempSel.items.Add(itemType.CRYSTAL);
            tempSel.items.Add(itemType.CRYSTAL);
            tempSel.items.Add(itemType.CRYSTAL);

        }
        else
        {

            //cant mine rcok

        }


    }

    public void AddBuilding(Point p, Building.buildings b)
    {

        this.grid[p.X, p.Y].rock = rockType.BUILDING;


    }

    // check add grid B to grid A at position v
    public bool CheckCombineGrids(int[,] a, int[,] b, Point p)
    {

        int rowLength = b.GetLength(0);
        int colLength = b.GetLength(1);
        int x = p.X;
        int y = p.Y;

        try
        {

            for (int i = y; i < rowLength + y; i++)
            {
                for (int j = x; j < colLength + x; j++)
                {

                    if (a[i, j] != 0)
                    {
                        return false;
                    }

                }
            }

        }
        catch (System.IndexOutOfRangeException ex)
        {

            Debug.Log(ex);
            return false;
        }

        return true;

    }

    // Add grid B to grid A at position p
    public int[,] CombineGrids(int[,] a, int[,] b, Point p)
    {

        int rowLength = b.GetLength(0);
        int colLength = b.GetLength(1);
        int x = p.X;
        int y = p.Y;
        int[,] ret = a;

        for (int i = y; i < rowLength + y; i++)
        {
            for (int j = x; j < colLength + x; j++)
            {

                ret[i, j] = b[j - x, i - y];

            }
        }

        return ret;


    }

    //rotate grid 90 degrees CW num times
    public int[,] RotateGrid(int[,] a, int num)
    {

        int[,] ret = new int[1, 1];
        int rowLength;
        int colLength;

        for (int k = 0; k < num; k++)
        {

            ret = new int[a.GetLength(1), a.GetLength(0)];
            rowLength = a.GetLength(0);
            colLength = a.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {

                    // swap direction and row
                    ret[j, i] = a[rowLength - 1 - i, j];

                }
            }

            a = ret;

        }


        return ret;


    }

    // output a string based on an int[,]
    public string ToString(int[,] a)
    {
        string ret = "";


        int rowLength = a.GetLength(0);
        int colLength = a.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                ret += a[i, j] + "\t";
            }

            ret += "\n";
        }



        return ret;
    }
}

// Class for map unit container
public class MapSpace
{

    public Point coordinates;
    public rockType rock;
    public List<itemType> items;
    public GameObject g;

    public MapSpace(Point c, rockType r)
    {

        coordinates = c;
        rock = r;
        items = new List<itemType>();

    }


}


