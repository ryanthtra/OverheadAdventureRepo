using UnityEngine;
using System.Collections;

// Camera used for following a single player
public class ECameraSPlayer : ECamera {
	
	private GameObject m_player;
	private GameObject m_current_room;
	
	public GameObject Player { get{ return m_player; } } 
	public GameObject CurrentRoom { get{ return m_current_room; } }
		
	// Use this for initialization
	protected override void Start () {
		m_player = GameObject.FindGameObjectWithTag("Player");
		m_current_room = GameObject.FindGameObjectWithTag("CurrentRoom");
		m_motor = new ECameraSPlayerMotor(this);
		
		Debug.Log ("m_motor = " + m_motor);
		Debug.Log ("m_current_room = " + m_current_room);
	}
	
	// Update is called once per frame
	protected override void Update () {
		m_motor.Update(Time.deltaTime);
	}
}
