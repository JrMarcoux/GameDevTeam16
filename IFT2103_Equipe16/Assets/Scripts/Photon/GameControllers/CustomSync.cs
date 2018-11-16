using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CustomSync : MonoBehaviourPun, IPunObservable
{

	public float SyncRate = 15;
	private Vector3 OriginPlayerPosition = Vector3.zero;


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(transform.position);
		}
		else
		{
			OriginPlayerPosition = (Vector3)stream.ReceiveNext();
		}
	}


	public void Update()
	{
		if (!photonView.IsMine)
		{
			transform.position = Vector3.Lerp(transform.position, OriginPlayerPosition, Time.deltaTime * this.SyncRate);
		}
	}


}
