using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color HoverColor = Color.yellow;

    private GameObject Building;
    private Color StartColor;
    private void Start()
    {
        StartColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = HoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = StartColor;
    }

    private void OnMouseDown()
    {
        if (Building != null) return;

        GameObject BuildingToBuild = BuildingManager.Instance?.GetSelectedBuilding();
        Building = Instantiate(BuildingToBuild, transform.position, Quaternion.identity);
    }
}
