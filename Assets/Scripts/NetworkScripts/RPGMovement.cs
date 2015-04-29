using UnityEngine;
using System.Collections;

[RequireComponent( typeof( CharacterController ) )]
public class RPGMovement : MonoBehaviour 
{
	public float ForwardSpeed;
	public float BackwardSpeed;
	public float StrafeSpeed;
	public float RotateSpeed;
	
	CharacterController m_CharacterController;
	Vector3 m_LastPosition;
	PhotonView m_PhotonView;
	PhotonTransformView m_TransformView;
	
	float m_AnimatorSpeed;
	Vector3 m_CurrentMovement;
	float m_CurrentTurnSpeed;
	
	void Start() 
	{
		m_CharacterController = GetComponent<CharacterController>();
		m_PhotonView = GetComponent<PhotonView>();
		m_TransformView = GetComponent<PhotonTransformView>();
	}
	
	void Update() 
	{
		if( m_PhotonView.isMine == true )
		{
			ResetSpeedValues();
			
			UpdateRotateMovement();
			
			UpdateForwardMovement();
			UpdateBackwardMovement();
			UpdateStrafeMovement();
			
			MoveCharacterController();
			ApplyGravityToCharacterController();
			
			ApplySynchronizedValues();
		}
	}

	
	void ResetSpeedValues()
	{
		m_CurrentMovement = Vector3.zero;
		m_CurrentTurnSpeed = 0;
	}
	
	void ApplySynchronizedValues()
	{
		m_TransformView.SetSynchronizedValues( m_CurrentMovement, m_CurrentTurnSpeed );
	}
	
	void ApplyGravityToCharacterController()
	{
		m_CharacterController.Move( transform.up * Time.deltaTime * -9.81f );
	}
	
	void MoveCharacterController()
	{
		m_CharacterController.Move( m_CurrentMovement * Time.deltaTime );
	}
	
	void UpdateForwardMovement()
	{
		if( Input.GetKey( KeyCode.W ) == true )
		{
			m_CurrentMovement = transform.forward * ForwardSpeed;
		}
	}
	
	void UpdateBackwardMovement()
	{
		if( Input.GetKey( KeyCode.S ) == true )
		{
			m_CurrentMovement = -transform.forward * BackwardSpeed;
		}
	}
	
	void UpdateStrafeMovement()
	{
		if( Input.GetKey( KeyCode.Q ) == true )
		{
			m_CurrentMovement = -transform.right * StrafeSpeed;            
		}
		
		if( Input.GetKey( KeyCode.E ) == true )
		{
			m_CurrentMovement = transform.right * StrafeSpeed;
		}
	}
	
	void UpdateRotateMovement()
	{
		if( Input.GetKey( KeyCode.A ) == true )
		{
			m_CurrentTurnSpeed = -RotateSpeed;
			transform.Rotate( 0, -RotateSpeed * Time.deltaTime, 0 );
		}
		
		if( Input.GetKey( KeyCode.D ) == true )
		{
			m_CurrentTurnSpeed = RotateSpeed;
			transform.Rotate( 0, RotateSpeed * Time.deltaTime, 0 );
		}
	}
}

