using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class controls all classes needed to run a game after main screen
public class Controller : MonoBehaviour
{

    // MAP
    public Map map;// = new Map();

    // Buildings
    public List<Building> buildings;

    // NPCS - player
    public List<NPC> npcs;

    // NPCS - Enemies
    public List<NPC> enimies;
    // Vehicles
    // public List<Vehicle> vehicles = new List<Vehicle>();

    // Current resources
    public Resource resource;

    // GUI
    public GUI_Map guiMap;
    public GUI_Info guiInfo;
    public GUI_Resources guiResources;
    public GUI_Toolbar guiToolbar;
    public GUI_ViewControls guiViewControls;
    public GUI_Map3D guiMap3D;

    // User input


    // Constructor
    public Controller()
    {


    }

    void Awake()
    {

        // init varibles
        Setup();

    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    void Setup()
    {

        buildings = new List<Building>();
        npcs = new List<NPC>();
        enimies = new List<NPC>();
        resource = gameObject.AddComponent<Resource>();

        // GUI
        guiMap = gameObject.AddComponent<GUI_Map>();
        guiInfo = gameObject.AddComponent<GUI_Info>();
        guiResources = gameObject.AddComponent<GUI_Resources>();
        guiToolbar = gameObject.AddComponent<GUI_Toolbar>();
        guiViewControls = gameObject.AddComponent<GUI_ViewControls>();
        guiMap3D = gameObject.AddComponent<GUI_Map3D>();

    }
}
