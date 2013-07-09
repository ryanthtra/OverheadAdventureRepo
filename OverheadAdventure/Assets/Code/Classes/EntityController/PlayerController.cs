using UnityEngine;
using System.Collections;

public class PlayerController : EntityController 
{
	Player m_owner;
	
	#region CONSTRUCTORS
	public PlayerController(Player owner) :
		base(owner)
	{
		this.m_owner = owner;
	}
	#endregion
	
	// Update is called once per frame
	public override void Update(float time_elapsed) 
	{
		base.Update(time_elapsed);
		
		//Debug.Log("Calling PlayerController.Update()");
		m_horiz_movement = Input.GetAxis("Horizontal");
		m_vert_movement = Input.GetAxis("Vertical");
		
		m_look_at_vector = getLookVector();
	}
	
	protected override bool testNorth()
	{
		return (Input.GetAxis("Vertical") > 0.001);
		//return false;
	}
	
	protected override bool testSouth()
	{
		return (Input.GetAxis("Vertical") < -0.001);
		//return false;
	}
	
	protected override bool testEast()
	{
		return (Input.GetAxis("Horizontal") > 0.001);
		//return false;
	}
	
	protected override bool testWest()
	{
		return (Input.GetAxis("Horizontal") < -0.001);
		//return false;
	}
	
	// Returns the vector the player should be facing relative to the mouse position.
	private Vector3 getLookVector()
	{
		// Gets mouse screen position (in terms of (screen_x, screen_y, 0)).
		Vector3 mouse_pos = Input.mousePosition;
		
		// Get the player's screen position (in terms of (screen_x, screen_y, screen_z)).
		Vector3 screen_pos = m_owner.viewport_camera.WorldToScreenPoint(m_owner.transform.position);
		
		// Calculate the vector resulting from the position differences.
		Vector3 temp_vector = mouse_pos - screen_pos;
		
		// Must move the resulting y to the z component due to world space orientation.
		// Also, flush out the y value to zero.
		Vector3 ret_vector = new Vector3(temp_vector.x, 0f, temp_vector.y);
		ret_vector.Normalize();
		
		return ret_vector;
	}
}
