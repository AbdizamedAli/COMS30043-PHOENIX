using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCon
{
	public class PlayerControl : MonoBehaviourPunCallbacks
	{
		[SerializeField] GameObject cameraHolder;
		[SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

		private bool showCursor;


		private bool m_wasGrounded;
		private bool m_isGrounded;

		float verticalLookRotation;
		bool grounded;
		Vector3 smoothMoveVelocity;
		Vector3 moveAmount;



		Rigidbody rb;
		PhotonView PV;
		Animator m_animator;
		private List<Collider> m_collisions = new List<Collider>();




		void Awake()
		{

			m_animator = GetComponent<Animator>();
			rb = GetComponent<Rigidbody>();
			PV = GetComponent<PhotonView>();
			
		}


		private void OnCollisionEnter(Collision collision)
		{
			ContactPoint[] contactPoints = collision.contacts;
			for (int i = 0; i < contactPoints.Length; i++)
			{
				if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
				{
					if (!m_collisions.Contains(collision.collider))
					{
						m_collisions.Add(collision.collider);
					}
					m_isGrounded = true;
				}
			}
		}
		private void OnCollisionStay(Collision collision)
		{
			ContactPoint[] contactPoints = collision.contacts;
			bool validSurfaceNormal = false;
			for (int i = 0; i < contactPoints.Length; i++)
			{
				if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
				{
					validSurfaceNormal = true; break;
				}
			}

			if (validSurfaceNormal)
			{
				m_isGrounded = true;
				if (!m_collisions.Contains(collision.collider))
				{
					m_collisions.Add(collision.collider);
				}
			}
			else
			{
				if (m_collisions.Contains(collision.collider))
				{
					m_collisions.Remove(collision.collider);
				}
				if (m_collisions.Count == 0) { m_isGrounded = false; }
			}
		}


		private void OnCollisionExit(Collision collision)
		{
			if (m_collisions.Contains(collision.collider))
			{
				m_collisions.Remove(collision.collider);
			}
			if (m_collisions.Count == 0) { m_isGrounded = false; }
		}

		private void Start()
		{

			if (!PV.IsMine)
			{
				Destroy(GetComponentInChildren<Camera>().gameObject);
				Destroy(rb);
			}
			m_animator.SetBool("Grounded", true);
			showCursor = false;
		}


		void Update()
		{
			if (!PV.IsMine)
				return;
			if (!showCursor)
            {
				Cursor.lockState = CursorLockMode.Locked;
			}
			else
            {
				Cursor.lockState = CursorLockMode.Confined;
            }
				
			m_animator.SetBool("Grounded", m_isGrounded);
			Look();
			Move();
			Jump();
			m_wasGrounded = m_isGrounded;

		}

		void Look()
		{
			transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

			verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
			verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

			cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
		}

		void Move()
		{
			Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

			moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);

			m_animator.SetFloat("MoveSpeed", moveAmount.magnitude);



		}

		void Jump()
		{
			if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
			{
				rb.AddForce(transform.up * jumpForce);

			}

			if (!m_wasGrounded && m_isGrounded)
			{
				m_animator.SetTrigger("Land");
			}

			if (!m_isGrounded && m_wasGrounded)
			{
				m_animator.SetTrigger("Jump");
			}
		}


		public void SetGroundedState(bool _grounded)
		{

			grounded = _grounded;
			Debug.Log(_grounded);
		}

		void FixedUpdate()
		{
			if (!PV.IsMine)
				return;
			rb.angularVelocity = Vector3.zero;
			rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

		}

	}
}