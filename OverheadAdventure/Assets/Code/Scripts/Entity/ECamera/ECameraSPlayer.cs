using UnityEngine;
using System.Collections;

// Camera used for following a single player
public class ECameraSPlayer : ECamera {
	
	private GameObject m_player;
	
	public GameObject Player { get{ return m_player; } } 
		
	// Use this for initialization
	protected override void Start () {
		m_player = GameObject.FindGameObjectWithTag("Player");
		m_motor = new ECameraSPlayerMotor(this);
		
		Debug.Log ("m_motor = " + m_motor);
	}
	
	// Update is called once per frame
	protected override void Update () {
		m_motor.Update(Time.deltaTime);
	}
}
