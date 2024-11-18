using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    public Transform rayController;
    public GameObject ray;
    private Player_ST player =>GetComponentInParent<Player_ST>();
    private void AnimationTriggered()
    {
        player.AnimationTrigger();
    }
    public void instantiateRay()
    {
        Instantiate(ray, rayController.position, rayController.rotation);
    }
}
