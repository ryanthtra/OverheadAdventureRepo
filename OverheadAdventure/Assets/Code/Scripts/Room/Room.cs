using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour 
{
	public const int WALL_THICKNESS = 1;
	private const int WALL_HEIGHT = 6;
	
	private const int FLOOR_THICKNESS = 1;
	private const int DOOR_WIDTH = 4;
	private const int DOOR_HEIGHT = 6;
	
	private const int ROOM_MIN_WIDTHDEPTH = 15;
	private const int ROOM_MAX_WIDTHDEPTH = 80;
		
	Transform m_floor;
	Transform m_door;
	
	private bool m_room_generated = false;
	
	private enum WhichWall
	{
		NORTH = 0,
		SOUTH,
		EAST,
		WEST,
		SIZE
	};
	
	WhichWall m_doorwall;
	
	Transform[] m_walls = new Transform[8];
	
	private enum WallIndex
	{
		NORTH_LEFT = 0,
		NORTH_RIGHT,
		SOUTH_LEFT,
		SOUTH_RIGHT,
		EAST_BACK,
		EAST_FRONT,
		WEST_BACK,
		WEST_FRONT,
		SIZE
	};
	
	#region PROPERTIES
	public Vector3 RoomDimensions { get{ return m_floor.localScale; } }
	public bool RoomGenerated { get{ return m_room_generated; } }
	#endregion
	
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
		Debug.Log("Generating random room dimensions...");
		GenerateFloor();
		GenerateDoor();
		GenerateWalls();
		Debug.Log("Room generation DONE!");
		m_room_generated = true;
	}
	
	void GenerateFloor()
	{
		int width = Random.Range(ROOM_MIN_WIDTHDEPTH, ROOM_MAX_WIDTHDEPTH + 1);
		int depth = Random.Range(ROOM_MIN_WIDTHDEPTH, ROOM_MAX_WIDTHDEPTH + 1);
		
		m_floor = transform.Find("Floor");
		m_floor.localScale = new Vector3((float)width, (float)FLOOR_THICKNESS, (float)depth);
	}
	
	void GenerateDoor()
	{
		m_door = transform.Find("Door");
		
		// Randomly determine which wall the door will be on.
//		m_doorwall = WhichWall.WEST;
		m_doorwall = (WhichWall) Random.Range((int)WhichWall.NORTH, (int)WhichWall.SIZE);
		
		// Variables to determine the door's local position.
		float x_location = 0f;
		float y_location = (DOOR_HEIGHT - FLOOR_THICKNESS) / 2f;
		float z_location = 0f;
		
		// Door's x and z local location dependent on which wall it'll be on.
		switch (m_doorwall)
		{
			case WhichWall.NORTH:
			case WhichWall.SOUTH:
			{
				int half_width = (int)m_floor.localScale.x / 2;
				x_location = Random.Range(-half_width + 3, half_width - 2);
			
				z_location = (m_floor.localScale.z + WALL_THICKNESS) / 2;
				z_location *= (m_doorwall == WhichWall.SOUTH ? -1 : 1);
			
				m_door.localScale = new Vector3((float)DOOR_WIDTH, (float)DOOR_HEIGHT, 1f);
				break;
			}
			
			case WhichWall.EAST:
			case WhichWall.WEST:
			{
				int half_depth = (int)m_floor.localScale.z / 2;
				z_location = Random.Range(-half_depth + 3, half_depth - 2);
			
				x_location = (m_floor.localScale.x + WALL_THICKNESS) / 2;
				x_location *= (m_doorwall == WhichWall.WEST ? -1 : 1);
			
				m_door.localScale = new Vector3(1f, (float)DOOR_HEIGHT, (float)DOOR_WIDTH);
				break;
			}
		}
		
		// Get door's position (relative to the room's world position).
		m_door.localPosition = new Vector3(x_location, y_location, z_location);
	}
	
	
	#region WALL_GENERATION
	void GenerateWalls()
	{
		if (m_doorwall == WhichWall.NORTH) GenerateWallWithDoor(WhichWall.NORTH);
		else GenerateWallWithoutDoor(WhichWall.NORTH);
		
		if (m_doorwall == WhichWall.SOUTH) GenerateWallWithDoor(WhichWall.SOUTH);
		else GenerateWallWithoutDoor(WhichWall.SOUTH);
		
		if (m_doorwall == WhichWall.EAST) GenerateWallWithDoor(WhichWall.EAST);
		else GenerateWallWithoutDoor(WhichWall.EAST);
		
		if (m_doorwall == WhichWall.WEST) GenerateWallWithDoor(WhichWall.WEST);
		else GenerateWallWithoutDoor(WhichWall.WEST);
	}
	
	void GenerateWallWithoutDoor(WhichWall wall_dir)
	{
		float x_pos = 0f;
		float y_pos = (WALL_HEIGHT - FLOOR_THICKNESS) / 2f;
		float z_pos = 0f;
		
		float scale_x = 0f;
		float scale_z = 0f;
		
		switch (wall_dir)
		{
			case WhichWall.NORTH:
			{
				m_walls[(int)WallIndex.NORTH_LEFT] = transform.Find("Wall_NorthL");
				m_walls[(int)WallIndex.NORTH_RIGHT] = transform.Find("Wall_NorthR");
			
				scale_x = m_floor.localScale.x / 2;
				m_walls[(int)WallIndex.NORTH_LEFT].localScale = 
					m_walls[(int)WallIndex.NORTH_RIGHT].localScale = 
						new Vector3(scale_x, WALL_HEIGHT, WALL_THICKNESS);
				
				x_pos = scale_x / 2;
				z_pos = (m_floor.localScale.z + WALL_THICKNESS) / 2;
			
				m_walls[(int)WallIndex.NORTH_LEFT].localPosition = 
					new Vector3(-x_pos, y_pos, z_pos);
				m_walls[(int)WallIndex.NORTH_RIGHT].localPosition =
					new Vector3(x_pos, y_pos, z_pos);
				break;
			}
			
			case WhichWall.SOUTH:
			{
				m_walls[(int)WallIndex.SOUTH_LEFT] = transform.Find("Wall_SouthL");
				m_walls[(int)WallIndex.SOUTH_RIGHT] = transform.Find("Wall_SouthR");
			
				scale_x = m_floor.localScale.x / 2;
				m_walls[(int)WallIndex.SOUTH_LEFT].localScale = 
					m_walls[(int)WallIndex.SOUTH_RIGHT].localScale = 
						new Vector3(scale_x, WALL_HEIGHT, WALL_THICKNESS);
				
				x_pos = scale_x / 2;
				z_pos = -(m_floor.localScale.z + WALL_THICKNESS) / 2;
			
				m_walls[(int)WallIndex.SOUTH_LEFT].localPosition = 
					new Vector3(-x_pos, y_pos, z_pos);
				m_walls[(int)WallIndex.SOUTH_RIGHT].localPosition =
					new Vector3(x_pos, y_pos, z_pos);
				break;
			}
			
			case WhichWall.EAST:
			{
				m_walls[(int)WallIndex.EAST_BACK] = transform.Find("Wall_EastB");
				m_walls[(int)WallIndex.EAST_FRONT] = transform.Find("Wall_EastF");
			
				scale_z = m_floor.localScale.z / 2 + WALL_THICKNESS;
				m_walls[(int)WallIndex.EAST_BACK].localScale = 
					m_walls[(int)WallIndex.EAST_FRONT].localScale = 
						new Vector3(WALL_THICKNESS, WALL_HEIGHT, scale_z);
				
				z_pos = scale_z / 2;
				x_pos = (m_floor.localScale.x + WALL_THICKNESS) / 2;
			
				m_walls[(int)WallIndex.EAST_BACK].localPosition = 
					new Vector3(x_pos, y_pos, z_pos);
				m_walls[(int)WallIndex.EAST_FRONT].localPosition =
					new Vector3(x_pos, y_pos, -z_pos);
				break;
			}
			
			case WhichWall.WEST:
			{
				m_walls[(int)WallIndex.WEST_BACK] = transform.Find("Wall_WestB");
				m_walls[(int)WallIndex.WEST_FRONT] = transform.Find("Wall_WestF");
			
				scale_z = m_floor.localScale.z / 2 + WALL_THICKNESS;
				m_walls[(int)WallIndex.WEST_BACK].localScale = 
					m_walls[(int)WallIndex.WEST_FRONT].localScale = 
						new Vector3(WALL_THICKNESS, WALL_HEIGHT, scale_z);
				
				z_pos = scale_z / 2;
				x_pos = -(m_floor.localScale.x + WALL_THICKNESS) / 2;
			
				m_walls[(int)WallIndex.WEST_BACK].localPosition = 
					new Vector3(x_pos, y_pos, z_pos);
				m_walls[(int)WallIndex.WEST_FRONT].localPosition =
					new Vector3(x_pos, y_pos, -z_pos);
				break;
			}
		}
	}
	
	void GenerateWallWithDoor(WhichWall wall_dir)
	{
		float x_pos_lf 		= 0f;
		float x_pos_rb 		= 0f;
		float y_pos 		= (WALL_HEIGHT - FLOOR_THICKNESS) / 2f;
		float z_pos_lf		= 0f;
		float z_pos_rb		= 0f;
		
		float scale_x_lf 	= 0f;
		float scale_x_rb	= 0f;
		float scale_z_lf	= 0f;
		float scale_z_rb	= 0f;
		
		switch (wall_dir)
		{
			case WhichWall.NORTH:
			{
				// North left wall calculation
				m_walls[(int)WallIndex.NORTH_LEFT] = transform.Find("Wall_NorthL");
			
				scale_x_lf = m_door.localPosition.x - DOOR_WIDTH/2 + m_floor.localScale.x/2;
				m_walls[(int)WallIndex.NORTH_LEFT].localScale = 
						new Vector3(scale_x_lf, WALL_HEIGHT, WALL_THICKNESS);
				
				x_pos_lf = (m_door.localPosition.x - DOOR_WIDTH/2) - (scale_x_lf/2);
				z_pos_lf = (m_floor.localScale.z + WALL_THICKNESS) / 2;
			
				m_walls[(int)WallIndex.NORTH_LEFT].localPosition = 
					new Vector3(x_pos_lf, y_pos, z_pos_lf);

				// North right wall calculation
				m_walls[(int)WallIndex.NORTH_RIGHT] = transform.Find("Wall_NorthR");
			
				scale_x_rb = m_floor.localScale.x - (m_door.localPosition.x + DOOR_WIDTH/2 + m_floor.localScale.x/2);
				m_walls[(int)WallIndex.NORTH_RIGHT].localScale = 
						new Vector3(scale_x_rb, WALL_HEIGHT, WALL_THICKNESS);
			
				x_pos_rb = (m_door.localPosition.x + DOOR_WIDTH/2) + (scale_x_rb/2);
				z_pos_rb = z_pos_lf;
			
				m_walls[(int)WallIndex.NORTH_RIGHT].localPosition = 
					new Vector3(x_pos_rb, y_pos, z_pos_rb);
				break;
			}
			
			case WhichWall.SOUTH:
			{
				// South left wall calculation
				m_walls[(int)WallIndex.SOUTH_LEFT] = transform.Find("Wall_SouthL");
			
				scale_x_lf = m_door.localPosition.x - DOOR_WIDTH/2 + m_floor.localScale.x/2;
				m_walls[(int)WallIndex.SOUTH_LEFT].localScale = 
						new Vector3(scale_x_lf, WALL_HEIGHT, WALL_THICKNESS);
				
				x_pos_lf = (m_door.localPosition.x - DOOR_WIDTH/2) - (scale_x_lf/2);
				z_pos_lf = -(m_floor.localScale.z + WALL_THICKNESS) / 2;
			
				m_walls[(int)WallIndex.SOUTH_LEFT].localPosition = 
					new Vector3(x_pos_lf, y_pos, z_pos_lf);

				// South right wall calculation
				m_walls[(int)WallIndex.SOUTH_RIGHT] = transform.Find("Wall_SouthR");
			
				scale_x_rb = m_floor.localScale.x - (m_door.localPosition.x + DOOR_WIDTH/2 + m_floor.localScale.x/2);
				m_walls[(int)WallIndex.SOUTH_RIGHT].localScale = 
						new Vector3(scale_x_rb, WALL_HEIGHT, WALL_THICKNESS);
			
				x_pos_rb = (m_door.localPosition.x + DOOR_WIDTH/2) + (scale_x_rb/2);
				z_pos_rb = z_pos_lf;
			
				m_walls[(int)WallIndex.SOUTH_RIGHT].localPosition = 
					new Vector3(x_pos_rb, y_pos, z_pos_rb);
				break;
			}
			
			case WhichWall.EAST:
			{
				// East back wall calculation
				m_walls[(int)WallIndex.EAST_FRONT] = transform.Find("Wall_EastF");
			
				scale_z_lf = WALL_THICKNESS + m_door.localPosition.z - DOOR_WIDTH/2 + m_floor.localScale.z/2;
				m_walls[(int)WallIndex.EAST_FRONT].localScale = 
						new Vector3(WALL_THICKNESS, WALL_HEIGHT, scale_z_lf);
				
				z_pos_lf = (m_door.localPosition.z - DOOR_WIDTH/2) - (scale_z_lf/2);
				x_pos_lf = (m_floor.localScale.x + WALL_THICKNESS) / 2;
			
				m_walls[(int)WallIndex.EAST_FRONT].localPosition = 
					new Vector3(x_pos_lf, y_pos, z_pos_lf);

				// East front wall calculation
				m_walls[(int)WallIndex.EAST_BACK] = transform.Find("Wall_EastB");
			
				scale_z_rb = WALL_THICKNESS + m_floor.localScale.z - 
					(m_door.localPosition.z + DOOR_WIDTH/2 + m_floor.localScale.z/2);
				m_walls[(int)WallIndex.EAST_BACK].localScale = 
						new Vector3(WALL_THICKNESS, WALL_HEIGHT, scale_z_rb);
			
				z_pos_rb = (m_door.localPosition.z + DOOR_WIDTH/2) + (scale_z_rb/2);
				x_pos_rb = x_pos_lf;
			
				m_walls[(int)WallIndex.EAST_BACK].localPosition = 
					new Vector3(x_pos_rb, y_pos, z_pos_rb);
				break;
			}
			
			case WhichWall.WEST:
			{
				// West back wall calculation
				m_walls[(int)WallIndex.WEST_FRONT] = transform.Find("Wall_WestF");
			
				scale_z_lf = WALL_THICKNESS + m_door.localPosition.z - DOOR_WIDTH/2 + m_floor.localScale.z/2;
				m_walls[(int)WallIndex.WEST_FRONT].localScale = 
						new Vector3(WALL_THICKNESS, WALL_HEIGHT, scale_z_lf);
				
				z_pos_lf = (m_door.localPosition.z - DOOR_WIDTH/2) - (scale_z_lf/2);
				x_pos_lf = -(m_floor.localScale.x + WALL_THICKNESS) / 2;
			
				m_walls[(int)WallIndex.WEST_FRONT].localPosition = 
					new Vector3(x_pos_lf, y_pos, z_pos_lf);

				// West front wall calculation
				m_walls[(int)WallIndex.WEST_BACK] = transform.Find("Wall_WestB");
			
				scale_z_rb = WALL_THICKNESS + m_floor.localScale.z - 
					(m_door.localPosition.z + DOOR_WIDTH/2 + m_floor.localScale.z/2);
				m_walls[(int)WallIndex.WEST_BACK].localScale = 
						new Vector3(WALL_THICKNESS, WALL_HEIGHT, scale_z_rb);
			
				z_pos_rb = (m_door.localPosition.z + DOOR_WIDTH/2) + (scale_z_rb/2);
				x_pos_rb = x_pos_lf;
			
				m_walls[(int)WallIndex.WEST_BACK].localPosition = 
					new Vector3(x_pos_rb, y_pos, z_pos_rb);
				break;
			}
		}
	}
	#endregion
}
