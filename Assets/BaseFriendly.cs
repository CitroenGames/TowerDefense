using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Tilemaps;

public class BaseFriendly : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform BaseTransform;
    [SerializeField] private LayerMask EnemyMask;

    [Header("Attributes")]
    [SerializeField] private float TargetingRange = 5f;

    private Transform Target = null;

    private void Update()
    {
        if (Target != null)
        {
            RotateTowardTarget();
            return;
        }
        if (FindTarget())
        {
            RotateTowardTarget();
            return;
        }
    }

    private bool FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, TargetingRange, (Vector2)transform.position, 0f, EnemyMask);

        if (hits.Length > 0)
        {
            Target = hits[0].transform;
            return true;
        }
        return false;
    }

    private void RotateTowardTarget()
    {
        if (Target == null) return;

        float angle = Mathf.Atan2(Target.position.y - transform.position.y, Target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion TargetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

        transform.rotation = TargetRotation;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, TargetingRange);
    }
}
