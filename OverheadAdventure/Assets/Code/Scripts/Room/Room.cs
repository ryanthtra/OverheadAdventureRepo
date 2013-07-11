using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour 
{
	public const int WALL_THICKNESS = 1;
	private const int FLOOR_THICKNESS = 1;
	private const int DOOR_WIDTH = 4;
	private const int DOOR_HEIGHT = 6;
	
	private const int ROOM_MIN_WIDTHDEPTH = 15;
	private const int ROOM_MAX_WIDTHDEPTH = 80;
		
	Transform m_floor;
	Transform m_door;
	
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
		m_door = transform.Find("Door");
		
		// Randomly determine which wall the door will be on.
		m_doorwall = (WhichWall) Random.Range((int)WhichWall.NORTH, (int)WhichWall.SIZE);
		
		// Variables to determine the door's local position.
		float x_location = 0f;
		float y_location = (DOOR_HEIGHT - FLOOR_THICKNESS) / 2;
		float z_location = 0f;
		
		switch (m_doorwall)
		{
			case WhichWall.NORTH:
			case WhichWall.SOUTH:
				int half_width = (int)m_floor.localScale.x / 2;
				x_location = Random.Range(-half_width + 3, half_width - 2);
				z_location = (m_floor.localScale.z + WALL_THICKNESS) / 2;
				z_location *= (m_doorwall == WhichWall.SOUTH ? -1 : 1);
				m_door.localScale = new Vector3((float)DOOR_WIDTH, (float)DOOR_HEIGHT, 1f);
				break;
			
			case WhichWall.EAST:
			case WhichWall.WEST:
				int half_depth = (int)m_floor.localScale.z / 2;
				z_location = Random.Range(-half_depth + 3, half_depth - 2);
				x_location = (m_floor.localScale.x + WALL_THICKNESS) / 2;
				x_location *= (m_doorwall == WhichWall.WEST ? -1 : 1);
				m_door.localScale = new Vector3(1f, (float)DOOR_HEIGHT, (float)DOOR_WIDTH);
				break;
		}
		
		// Get door's position (relative to the room's world position).
		m_door.localPosition = new Vector3(x_location, y_location, z_location);
	}
	
	void GenerateWalls()
	{
	}
}
