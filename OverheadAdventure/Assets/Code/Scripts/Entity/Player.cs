using UnityEngine;
using System.Collections;

public class Player : Entity 
{
	// Camera is needed for the ability to use mouse positioning as input.
	public Camera viewport_camera;
	
	// Use this for initialization
	protected override void Start () 
	{
		m_controller = new PlayerController(this);
		m_motor = new EntityMotor(this);
		m_body = GetComponent<CharacterController>();
		
		m_speed = 10f;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
