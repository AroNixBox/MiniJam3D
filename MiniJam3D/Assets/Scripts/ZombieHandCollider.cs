using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHandCollider : MonoBehaviour
{
    [SerializeField] private CatView catComponent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
            catComponent.RequestDoDamageToPlayer(other.GetComponent<IDamageable>());
    }
}
