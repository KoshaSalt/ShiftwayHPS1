using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerPosition;
    public float[] playerRotation;
    public bool isSeated;

    public PlayerData(Player player)
    {
        playerPosition = new float[3];
        playerPosition[0] = player.playerPosition.x;
        playerPosition[1] = player.playerPosition.y;
        playerPosition[2] = player.playerPosition.z;

        playerRotation = new float[4];
        playerRotation[0] = player.playerRotation.x;
        playerRotation[1] = player.playerRotation.y;
        playerRotation[2] = player.playerRotation.z;
        playerRotation[3] = player.playerRotation.w;

        isSeated = player.isSeated;
    }
}
