using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    private Player_ST player =>GetComponentInParent<Player_ST>();
    private void AnimationTriggered()
    {
        player.AnimationTrigger();
    }
}
