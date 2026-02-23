using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxEffectMultiplier = 0.5f;
    private Transform mainCameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
        lastCameraPosition = mainCameraTransform.position;
    }

    private void Update()
    {
        // Hitung perpindahan kamera sejak frame sebelumnya
        float deltaX = mainCameraTransform.position.x - lastCameraPosition.x;

        // Hitung perpindahan latar belakang berdasarkan efek parallax
        float parallaxEffect = deltaX * parallaxEffectMultiplier;

        // Perbarui posisi latar belakang
        Vector3 backgroundTargetPosition = new Vector3(transform.position.x + parallaxEffect, transform.position.y, transform.position.z);
        transform.position = backgroundTargetPosition;

        // Simpan posisi kamera saat ini untuk frame selanjutnya
        lastCameraPosition = mainCameraTransform.position;
    }
}
