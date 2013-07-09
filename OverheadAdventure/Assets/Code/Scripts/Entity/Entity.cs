using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour 
{
	#region ENTITY_COMPONENTS
	protected EntityController m_controller;
	protected EntityMotor m_motor;
	#endregion
	
	protected CharacterController m_body;
	
	public float m_speed = 1f;
	
	#region PROPERTIES
	public EntityController controller	{ get{ return m_controller; } }
	public EntityMotor motor			{ get{ return m_motor; } }
	
	public CharacterController body		{ get{ return m_body; } }
	public float speed					{ get{ return m_speed; } }
	#endregion
	
	// Use this for initialization
	protected virtual void Start () 
	{
		m_controller = new EntityController(this);
		m_motor = new EntityMotor(this);
		m_body = GetComponent<CharacterController>();
	
		Debug.Log ("m_controller = " + m_controller);
		Debug.Log ("m_motor = " + m_motor);
		Debug.Log ("m_body = " + m_body);
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		m_controller.Update(Time.deltaTime);
		
		m_motor.Update(Time.deltaTime);
	}
}
