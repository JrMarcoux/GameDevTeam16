using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SyncPlayerMovement : NetworkBehaviour {

	[SerializeField]
	Transform playerPosition;
	[SerializeField]
	float jumpRate = 15;
	[SyncVar]
	private Vector3 syncPos;
	
	void FixedUpdate()
	{
		TransmitPositionPlayer();
		JumpPosition();

	}

	[ClientCallback]
	void TransmitPositionPlayer()
	{
		if (isLocalPlayer)
		{
			CmdPositionToServer(playerPosition.position);
		}
	}

	[Command]
	void CmdPositionToServer(Vector3 position)
	{
		syncPos = position;
	}

	void JumpPosition()
	{
		if (!isLocalPlayer)
		{
			playerPosition.position = Vector3.Lerp(playerPosition.position, syncPos, Time.deltaTime * jumpRate);
		}
	}
}
