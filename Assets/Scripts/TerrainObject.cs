using UnityEngine;

// คลาสหลักของวัตถุบนฉากที่สามารถโต้ตอบได้
// Implement IElementInteractive (Interface) เพื่อใช้ Polymorphism
public abstract class TerrainObject : MonoBehaviour, IElementInteractive
{

    // Abstract Method (บังคับให้คลาสลูกต้องมี Logic การโต้ตอบ)
    // การเรียกเมธอดนี้จะทำให้เกิด Polymorphism
    public abstract bool HandleElementContact(ElementType type, PlayerUnit player);

    // เมธอดเสริมสำหรับ Level Design
    public Vector3 GetGridPosition()
    {
        // ในโปรเจกต์จริง ควรมีการคำนวณตำแหน่ง Grid ที่แม่นยำ
        return new Vector3(Mathf.Round(transform.position.x),
                           Mathf.Round(transform.position.y),
                           0);
    }
}