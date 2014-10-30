using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour {


	public enum NPCType {PLAYER, ENEMY};
	
	public NPCType type = NPCType.PLAYER;
	public string unitName = "";
	public Point location = new Point(0,0);
	public Queue<Task> tasks = new Queue<Task>();
	public bool inVehicle = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	// removes all current tasks
	public void ClearQueue(){

		tasks.Clear();

	}

}
