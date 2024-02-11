using System.Collections.Generic;
using UnityEngine;

public class SoldierTrigger : MonoBehaviour
{
    public List<Transform> Targets = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            Targets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            Targets.Remove(other.transform);
        }
    }
}
