using UnityEngine;
using System.Collections;

static public class EnemyAim{

	static public bool smallAim(Vector3 player, Vector3 enemy)
    {
        if(Mathf.Abs(player.y - enemy.y) < 1.0f)
            return true;
        return false;
    }
}
