using UnityEngine;
using System.Collections;

public class ECameraSPlayerMotor : EntityMotor 
{
	ECameraSPlayer m_owner;
	
	private float m_ydiff;
	private float m_viewport_halfwidth;
	private float m_viewport_halfheight;
	
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
	
	// Update is called once per frame
	public override void Update(float time_elapsed) 
	{
		// Camera is basically the 
		m_owner.gameObject.transform.position = 
			new Vector3(
				Mathf.Clamp(m_owner.Player.transform.position.x, -25f + m_viewport_halfwidth, 25f - m_viewport_halfwidth),
				m_owner.Player.transform.position.y + m_ydiff,
				Mathf.Clamp(m_owner.Player.transform.position.z, -25f + m_viewport_halfheight, 25f - m_viewport_halfheight));
	}
}
