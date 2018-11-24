using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BotSpawnerMultiplayer : NetworkBehaviour
{
	public GameObject botPrefab;
	public string botTag;
	public float MinShootPointX;
	public float MaxShootPointX;
	public float MinShootPointZ;
	public float MaxShootPointZ;

	public override void OnStartServer()
	{
			
			var bot = (GameObject)Instantiate(botPrefab, transform.position, Quaternion.Euler(0.0f,0.0f,0.0f));
			bot.GetComponent<controlBotMultiplayer>().minX = MinShootPointX;
			bot.GetComponent<controlBotMultiplayer>().maxX = MaxShootPointX;
			bot.GetComponent<controlBotMultiplayer>().minZ = MinShootPointZ;
			bot.GetComponent<controlBotMultiplayer>().maxZ = MaxShootPointZ;
			bot.tag = botTag;
			NetworkServer.Spawn(bot);
	}
	
}
