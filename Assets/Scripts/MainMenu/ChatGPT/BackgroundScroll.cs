using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    private void Update()
    {
        // Mendapatkan posisi lama latar belakang
        Vector3 pos = transform.position;

        // Menghitung perpindahan horizontal
        float deltaX = Time.deltaTime * scrollSpeed;

        // Menambahkan perpindahan horizontal pada posisi latar belakang
        pos.x += deltaX;

        // Menerapkan posisi baru pada latar belakang
        transform.position = pos;
    }
}

