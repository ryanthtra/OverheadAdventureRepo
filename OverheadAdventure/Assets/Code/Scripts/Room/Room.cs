using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour 
{
	public const int WALL_THICKNESS = 1;
	private const int FLOOR_THICKNESS = 1;
	private const int DOOR_WIDTH = 4;
	
	private const int ROOM_MIN_WIDTHDEPTH = 15;
	private const int ROOM_MAX_WIDTHDEPTH = 80;
		
	Transform m_floor;
	
	private enum WhichWall
	{
		NORTH = 0,
		SOUTH,
		EAST,
		WEST,
		SIZE
	}
	
	WhichWall m_doorwall;
	
	public Vector3 RoomDimensions { get{ return m_floor.localScale; } }
	
	// Use this for initialization
	void Start () 
	{
		GenerateRoom();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void GenerateRoom()
	{
		GenerateFloor();
		GenerateDoor();
		GenerateWalls();
	}
	
	void GenerateFloor()
	{
		Debug.Log ("Generating Floor...");
		int width = Random.Range(ROOM_MIN_WIDTHDEPTH, ROOM_MAX_WIDTHDEPTH + 1);
		int depth = Random.Range(ROOM_MIN_WIDTHDEPTH, ROOM_MAX_WIDTHDEPTH + 1);
		
		m_floor = transform.Find("Floor");
		m_floor.localScale = new Vector3((float)width, 1f, (float)depth);
	}
	
	void GenerateDoor()
	{
		m_doorwall = (WhichWall) Random.Range((int)WhichWall.NORTH, (int)WhichWall.SIZE);
		
		switch (m_doorwall)
		{
			case WhichWall.NORTH:
			case WhichWall.SOUTH:
				
				break;
			
			case WhichWall.EAST:
			case WhichWall.WEST:
				break;
		}
	}
	
	void GenerateWalls()
	{
	}
}
