using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Tilemaps;
using Unity.VisualScripting;

public class BaseFriendly : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform BaseTransform;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform BulletSpawnPoint;

    [Header("Attributes")]
    [SerializeField] private float TargetingRange = 5f;
    [SerializeField] private float RotationSpeed = 200f;
    [SerializeField] private float FireRate = 1f;

    private Transform Target = null;
    private float FireTimer = 0f;

    private void Update()
    {
        if (Target == null)
        {
            if(!FindTarget())
            {
                return;
            }
        }

        if (!IsTargetInRange())
        {
            Target = null;
            return;
        }

        RotateTowardTarget();
        
        FireTimer += Time.deltaTime;
        if (FireTimer >= 1f / FireRate)
        {
            Fire();
            FireTimer = 0f;
        }
    }

    private void Fire()
    {
        GameObject bulletobj = Instantiate(BulletPrefab, BulletSpawnPoint.position, Quaternion.identity);
        BaseBullet BulletScript = bulletobj.GetComponent<BaseBullet>();
        if (BulletScript != null)
        {
            BulletScript.SetTarget(Target);
        }
        else
        {
            Debug.LogError("BulletScript is null");
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

        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, RotationSpeed * Time.deltaTime);
    }

    private bool IsTargetInRange()
    {
        return Vector2.Distance(Target.position, transform.position) <= TargetingRange;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, TargetingRange);
    }
}
