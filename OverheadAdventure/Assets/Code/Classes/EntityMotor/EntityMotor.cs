using UnityEngine;
using System.Collections;

public class EntityMotor 
{
	protected Entity m_owner;
	
	protected Vector3 m_move_vector;
	
	#region CONSTRUCTORS
	private EntityMotor()
	{
	}
	
	public EntityMotor(Entity owner)
	{
		m_owner = owner;
		init();
	}
	#endregion
	
	protected void init()
	{
		m_move_vector = Vector3.zero;
		
		// Establish gameObject's local space vectors.
		m_owner.transform.forward = new Vector3(0f, 0f, 1f);
		m_owner.transform.up = new Vector3(0f, 1f, 0f);
		m_owner.transform.right = new Vector3(1f, 0f, 0f);
	}
	
	public void Update(float time_elapsed)
	{
		m_move_vector = Vector3.zero;
		
		goNorth();
		goSouth();
		goEast ();
		goWest ();
		
		m_move_vector.Normalize();			// Compensate in case of diagonal movement.
		m_move_vector *= m_owner.speed;
		m_owner.body.Move(m_move_vector * time_elapsed);
		
		m_owner.transform.forward = m_owner.controller.lookAtVector;
	}
	
	protected void goNorth()
	{
		if (m_owner.controller.moveNorth)
		{
			m_move_vector += new Vector3(0f, 0f, m_owner.controller.vertMovement);
		}
	}
	
	protected void goSouth()
	{
		if (m_owner.controller.moveSouth)
		{
			m_move_vector += new Vector3(0f, 0f, m_owner.controller.vertMovement);
		}
	}
	
	protected void goEast()
	{
		if (m_owner.controller.moveEast)
		{
			m_move_vector += new Vector3(m_owner.controller.horizMovement, 0f, 0f);
		}
	}
	
	protected void goWest()
	{
		if (m_owner.controller.moveWest)
		{
			m_move_vector += new Vector3(m_owner.controller.horizMovement, 0f, 0f);
		}
	}
}
