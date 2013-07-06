using UnityEngine;
using System.Collections;

public class EntityController
{
	protected Entity m_owner;
	
	protected bool m_move_north = 		false;
	protected bool m_move_south = 		false;
	protected bool m_move_east =		false;
	protected bool m_move_west =		false;
	
	protected float m_horiz_movement =	0f;
	protected float m_vert_movement =	0f;
	protected Vector3 m_look_at_vector = Vector3.zero;
		
	#region PROPERTIES
	public bool moveNorth	{ get{ return m_move_north; } }
	public bool moveSouth	{ get{ return m_move_south; } }
	public bool moveEast	{ get{ return m_move_east; } }
	public bool moveWest	{ get{ return m_move_west; } }
	public float horizMovement { get{ return m_horiz_movement; } }
	public float vertMovement { get{ return m_vert_movement; } }
	public Vector3 lookAtVector { get{ return m_look_at_vector; } }
	#endregion
	
	#region CONSTRUCTORS
	private EntityController()
	{
	}
	
	public EntityController(Entity owner)
	{
		m_owner = owner;
	}
	
	#endregion
	
	
	public virtual void Update(float time_elapsed)
	{
		m_move_north = testNorth();
		m_move_south = testSouth();
		m_move_east = testEast();
		m_move_west = testWest();
	}
	
	protected virtual bool testNorth()
	{
		return false;
	}
	
	protected virtual bool testSouth()
	{
		return false;
	}
	
	protected virtual bool testEast()
	{
		return false;
	}
	
	protected virtual bool testWest()
	{
		return false;
	}
}
