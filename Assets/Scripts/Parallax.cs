using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float movingBackgroundSpeed = 0.05f;
    

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(movingBackgroundSpeed * Time.deltaTime, 0);
    }
}
