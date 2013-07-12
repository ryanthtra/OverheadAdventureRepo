using UnityEngine;
using System.Collections;

public class ECameraSPlayerMotor : EntityMotor 
{
	ECameraSPlayer m_owner;
	
	private float m_ydiff;
	private float m_viewport_halfwidth;
	private float m_viewport_halfheight;
	private bool m_disable_horiz_scroll = true;
	private bool m_disable_vert_scroll = true;
	private bool m_has_room_dimensions = false;
	Vector3 room_dimensions;
	
	public ECameraSPlayerMotor(ECameraSPlayer owner)
	{
		m_owner = owner;
		init();
	}
	
	protected override void init()
	{
		m_ydiff = m_owner.gameObject.transform.position.y;
		m_viewport_halfheight = m_owner.camera.orthographicSize;
		m_viewport_halfwidth = m_viewport_halfheight * m_owner.camera.aspect;
	}
	
	private void GetCurrentRoomDimensions()
	{
		room_dimensions = m_owner.CurrentRoom.GetComponent<Room>().RoomDimensions;
		if (m_viewport_halfheight <  ((room_dimensions.z / 2) + Room.WALL_THICKNESS))
			m_disable_vert_scroll = false;
		if (m_viewport_halfwidth < ((room_dimensions.x / 2) + Room.WALL_THICKNESS))
			m_disable_horiz_scroll = false;
		
		m_has_room_dimensions = true;
	}
	
	// Update is called once per frame
	public override void Update(float time_elapsed) 
	{
		// Camera is basically the 
		if (m_has_room_dimensions)
		{
			m_owner.gameObject.transform.position = 
				new Vector3(
					m_disable_horiz_scroll ? 0f : Mathf.Clamp(
													m_owner.Player.transform.position.x, 
													-(room_dimensions.x/2 + Room.WALL_THICKNESS) + m_viewport_halfwidth, 
													(room_dimensions.x/2 + Room.WALL_THICKNESS) - m_viewport_halfwidth),
					m_owner.Player.transform.position.y + m_ydiff,
					m_disable_vert_scroll ? 0f : Mathf.Clamp(
													m_owner.Player.transform.position.z, 
													-(room_dimensions.z/2 + Room.WALL_THICKNESS) + m_viewport_halfheight, 
													(room_dimensions.z/2 + Room.WALL_THICKNESS) - m_viewport_halfheight)
					);
		}
		else
		{
			GetCurrentRoomDimensions();
		}
	}
}
