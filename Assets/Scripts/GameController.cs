using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

	public class GameController : MonoBehaviour
	{
		[SerializeField]
		private StoneSpawner m_stoneSpawner;
		private float m_timer = 0f;
		[SerializeField]
		private float m_delay = 1f;
		[SerializeField] private float m_power = 100f;

		private ContactPoint contact;
		private Rigidbody body;
		private void Awake()
		{

		}

		private void Start()
		{
			StartGame();
		}

		private void StartGame()
		{
			GameEvents.onGameOver += OnGameOver;
		}

		private void OnGameOver()
		{
			GameEvents.onGameOver -= OnGameOver;
			Debug.Log("Game Over");
		}

		private void Update()
		{
			m_timer += Time.deltaTime;
			if (m_timer >= m_delay)
			{
				m_stoneSpawner.Spawn();
				m_timer -= m_delay;
			}

			Debug.DrawRay(contact.normal,contact.point,Color.red);
		}

		public void OnCollisionStone(Collision collision)
		{
			if (collision.gameObject.TryGetComponent<Stone>(out var stone))
			{
				stone.SetAffect(false);
				 contact = collision.contacts[0];
				 body = contact.otherCollider.GetComponent<Rigidbody>();
				Debug.Log(contact.normal);
				body.AddForce(contact.normal.normalized * m_power, ForceMode.Impulse);
				//body.AddForceAtPosition(m_power*contact.normal, contact.point);
				
				Physics.IgnoreCollision(contact.thisCollider, contact.otherCollider, true);


			}
		}

		private void OnDestroy()
		{
			GameEvents.onGameOver -= OnGameOver;
		}
	}
}