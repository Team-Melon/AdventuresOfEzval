using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void Kill() {
        foreach(Collider c in GetComponentsInChildren<Collider>()) {
            c.enabled = false;
        }
    }
}
