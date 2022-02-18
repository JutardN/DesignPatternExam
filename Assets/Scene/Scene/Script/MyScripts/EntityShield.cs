using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityShield : MonoBehaviour
{
    [SerializeField] Health _health;

    public void Shield()
    {
        _health.Shielding = !_health.Shielding;
    }
}
