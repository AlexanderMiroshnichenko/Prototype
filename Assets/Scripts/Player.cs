using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private Transform m_tool;
	//[SerializeField]
	//private Rigidbody m_toolBody;
	public float range = 30f;
	public float speed = 1f;
	private float m_timer = 0f;

	private bool m_isDown;

	private void Update()
	{
		var angels = m_tool.localEulerAngles;
		// m_timer += Time.deltaTime;
		// var x = Mathf.Cos(Mathf.PI * m_timer * speed) * range;
		var target = range * (m_isDown ? -1f : 1f);

		//Debug.Log(m_toolBody.rotation);
		var x = Mathf.MoveTowardsAngle(angels.x, target, speed * Time.deltaTime);
		angels.x = x;
		m_tool.localEulerAngles = angels;
	}

	public void OnDown()
	{
		m_isDown = true;
	}

	public void OnUp()
	{
		m_isDown = false;
	}
}
