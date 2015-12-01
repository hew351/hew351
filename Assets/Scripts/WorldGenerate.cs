﻿using UnityEngine;
using System.Collections;

public class WorldGenerate : MonoBehaviour {
	public GameObject wall;
	public GameObject rocks;
	public GameObject obstacle;
	public GameObject spawn;
	//add for generate items
	public GameObject Bow;
	public GameObject Sword;
	public GameObject Armor;
	//set up room numbers
	public int XroomNum;
	public int YroomNum;

	public float wallDimensions;
	public int mapWidth = 1;
	public int mapHeight = 1;
	public int type = 0;
	public float mapOffsetX = 0f; // Used to determine where the algorithm will start from
	public float mapOffsetY = 0f; // Used to determine where the algorithm will start from
	public int wallsInGroup = 1;
	public int minWallsInLevel = 1;
	public int maxWallsInLevel = 1;
	public int minSpawnsInLevel = 1;
	public int maxSpawnsInLevel = 1;
	public int minObstaclesInLevel =1;
	public int maxObstaclesInLevel =1;
	private float realSize = 0.6f;
    




	private GameObject[ , ] wallSet; // Storage for walls

	//0 for empty
	//1 for lava pool
	//2 for items
	//3 for enemy spwan
	//4 for tunnel( can not put anything on it)
	//5 for obstical 
	private int[ , ] typeSet;


	// Use this for initialization
	void Start () {

		typeSet = new int[mapWidth, mapHeight];

		wallSet = new GameObject[mapWidth, mapHeight]; // This array now represents the entire  map of the level
		for(int i = 0; i < mapWidth; i++) {
			for(int j = 0; j < mapHeight; j++){
				wallSet[i, j] = null;
				typeSet[i, j] =0;


			}
		}


		GenerateRoom ();


		GenerateWalls ();  //Generates random walls and adds them into the wallSet array
		GenerateMaps ();
		GenerateSpawns (); // Generates random enemy spawns and adds them into the wallSet array
		GenerateSword (); //Generates random positon items into wallSet array
		GenerateBow ();
		GenerateArmor ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//check is position(x,y) a path
	bool notPath(int x, int y){

		if (typeSet [x, y] == 4) {
			return false;
		} else
			return true;
	
	}

	// Tests if x,y is within the bounds of the map
	bool WithinBounds (int x, int y) {
		return (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight);
	}

	// Returns the the wall at x,y if one exists and was 
	// created by this object. Otherwise returns null
	GameObject GetWall (int x, int y){
		if (WithinBounds(x, y)) {
			return wallSet [x, y];
		} else {
			return null;
		}
	}

	 // Places a wall object at x,y if one that was created 
	// by this object does not already exist	 
	void PutWall (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null&&typeSet[x, y]==0) {
				GameObject tempWall = Instantiate (wall) as GameObject;
				tempWall.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = tempWall;
				typeSet[x, y] = 1;
			}
		}
	}

	
	// Places a wall object at x,y if one that was created 
	// by this object does not already exist	 
	void PutRocks (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null&&typeSet[x, y]==0) {
				GameObject tempRock = Instantiate (rocks) as GameObject;
				tempRock.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = tempRock;
				typeSet[x, y] = 2;
			}
		}
	}

	// Places a wall object at x,y if one that was created 
	// by this object does not already exist	 
	void PutObstacles (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null&&typeSet[x, y]==0) {
				GameObject tempObstacle = Instantiate (obstacle) as GameObject;
				tempObstacle.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = tempObstacle;
				typeSet[x, y] = 5;
			}
		}
	}
	
	// Places an enemy spawn point at x,y if it is within bounds and null
	void putSpawn (int x, int y) {
		if (WithinBounds (x, y)&&typeSet[x, y]==0) {
			if (wallSet [x, y] == null) {
				GameObject tempSpawn = Instantiate (spawn) as GameObject;
				tempSpawn.tag = "Spawn";
				tempSpawn.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = tempSpawn;
				typeSet[x, y] = 3;
			}
		}
	}

	// place a item object at x,y if it in bounds and nothing here
	void PutItem (int x, int y, GameObject item) {
		if (WithinBounds (x, y)&&typeSet[x, y]==0) {
			if (wallSet [x, y] == null) {
				GameObject temp = Instantiate (item) as GameObject;
				temp.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = temp;
				typeSet[x, y] = 2;
			}
		}
	}


	// Removes a wall object at x,y if one exists
	// and was created by this object
	void RemoveWall (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] != null) {
				Destroy (wallSet[x, y]);
				wallSet[x, y] = null;
			}
		}
	}




	// Generates a squiggly line of wall objects starting at the position startX,startY
	// Generates a maximum of numWall number of walls
	// If the line would go off the map, the function will return to the starting point 
	void GenerateWallGroup (int startX, int startY, int numWalls) {


		int x = startX; //the "pointer" x coord
		int y = startY; //the "pointer" y coord
		int direction = Random.Range (0, 4);

		for (int i = 0; i < numWalls; i++) {
			switch(direction) {	// Moves the "pointer" position to the next location
			case 0: //going right
				while(WithinBounds (x,y) && GetWall(x, y) != null) {
					x++;
				}
				break;
			case 1: //going up
				while(WithinBounds (x,y) && GetWall(x, y) != null) {
					y--;
				}
				break;
			case 2: //going left
				while(WithinBounds (x,y) && GetWall(x, y) != null) {
					x--;
				}
				break;
			case 3: //going down
				while(WithinBounds (x,y) && GetWall(x, y) != null) {
					y++;
				}
				break;
			default:
				break;
			}

			if (WithinBounds (x,y)&& typeSet[x,y]!=4) {
				PutWall (x,y);	// Places a wall at the "pointer" position
			} else {
				x = startX;	// Moves the "pointer" back to the start position
				y = startY; // if the pointer is out of the map bounds
			}

			switch (Random.Range(0,3)) { // Change the direction of the pointer (turn left, go straight, turn right)
			case 0:
				direction--;
				if (direction<0) direction=3;
				break;
			case 2:
				direction++;
				if (direction>3) direction=0;
				break;
			default:
				break;
			}

		}
	}

	 //Generates several squiggly lines of walls
	void GenerateWalls () {
		for (int i=0; i<Random.Range(minWallsInLevel,maxWallsInLevel); i++) {
			GenerateWallGroup (Random.Range (0, mapWidth), Random.Range (0, mapHeight), Random.Range (1, wallsInGroup));
		}
	}

	//set up bound and obstacles
	void GenerateMaps() {

		//set bound between countryside and outside
		for (int j = 0; j < mapHeight; j++) {
			if ( typeSet [0, j] == 0 && wallSet [0, j] == null && WithinBounds (0, j)) {

				PutRocks (0, j);
				
			}
		}

		//put obstacles
		int x;
		int y;
		for (int i=0; i<Random.Range(minObstaclesInLevel,maxObstaclesInLevel); i++) {
			do {
				x = Random.Range (0, mapWidth); 
				y = Random.Range (0, mapHeight); 
			} while(!(WithinBounds (x,y)&& wallSet [x, y] == null&&notPath(x,y)));
			PutObstacles (x, y);
		}


		}

	// give map a room like seperate spaces
	void GenerateRoom() {

		int roomHeight = (mapHeight-1)/YroomNum;
		int roomWidth = (mapWidth-1) /XroomNum;

		//X direction path

		for (int i =roomHeight/2; i<=mapHeight; i+=roomHeight) {
		
			for(int j =0; j<mapWidth-1; j++)
			{
				typeSet[j,i]=4;
				typeSet[j,i+1]=4;
				typeSet[j,i-1]=4;
			}
		}

		//Y direction path

		for (int i =roomWidth/2; i<=mapWidth; i+=roomWidth) {
		
			for (int j=2; j<mapHeight-1; j++)
			{
				typeSet[i,j]=4;
				typeSet[i+1,j]=4;
				typeSet[i-1,j]=4;
			}
		}

		//create room by rocks seperate

		//X direction rocks

		for (int i =roomHeight; i<=mapHeight-roomHeight; i+=roomHeight) {
		
			for(int j =0; j<mapWidth; j++)
			{
				if(typeSet[j,i]!=4){
				PutRocks(j,i);
				}
			}

		}

		//Y direction rocks

		for (int i = roomWidth; i<=mapWidth-roomWidth; i+=roomWidth) {
		
			for (int j=0; j<mapHeight; j++)
			{
				if(typeSet[i,j]!=4){
				PutRocks(i,j);
				}
			}

		}
	}

	// Generates a random number of enemy spawn points in the level
	void GenerateSpawns () {
		int x;
		int y;
		for (int i=0; i<Random.Range(minSpawnsInLevel,maxSpawnsInLevel); i++) {
			do {
				x = Random.Range (0, mapWidth); 
				y = Random.Range (0, mapHeight); 
			} while(!(WithinBounds (x,y)&& wallSet [x, y] == null));
			putSpawn (x, y);
		}

	}

	//Generates items by get random x,y within map bounds and check is there any other object
	void GenerateSword() {
		int x;
		int y;
		do {
			x = Random.Range (0, mapWidth); 
			y = Random.Range (0, mapHeight); 
		} while(!(WithinBounds (x,y)&& wallSet [x, y] == null));

		PutItem (x,y,Sword);
	}

	void GenerateBow() {
		int x;
		int y;
		do {
			x = Random.Range (0, mapWidth); 
			y = Random.Range (0, mapHeight); 
		} while(!(WithinBounds (x,y)&& wallSet [x, y] == null));
		
		PutItem (x,y,Bow);
	}

	void GenerateArmor() {
		int x;
		int y;
		do {
			x = Random.Range (0, mapWidth); 
			y = Random.Range (0, mapHeight); 
		} while(!(WithinBounds (x,y)&& wallSet [x, y] == null));
		
		PutItem (x,y,Armor);
	}

}
