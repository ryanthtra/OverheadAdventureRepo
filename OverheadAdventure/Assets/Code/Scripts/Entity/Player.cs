using UnityEngine;
using System.Collections;

public class Player : Entity 
{
	// Use this for initialization
	protected override void Start () 
	{
		m_controller = new PlayerController(this);
		m_motor = new EntityMotor(this);
		m_body = GetComponent<CharacterController>();
		
		m_speed = 5f;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
