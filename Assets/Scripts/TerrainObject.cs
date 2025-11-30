using UnityEngine;

// คลาสหลักของวัตถุบนฉากที่สามารถโต้ตอบได้ (เช่น ลาวา, น้ำแข็ง, Anchor)
// สังเกตว่าคลาสนี้เป็น Abstract เพราะไม่มี Logic การโต้ตอบที่แน่นอน
public abstract class TerrainObject : MonoBehaviour, IElementInteractive
{

    // Abstract Method (บังคับให้คลาสลูกต้องมี Logic การโต้ตอบ)
    // การเรียกเมธอดนี้จะทำให้เกิด Polymorphism ใน CorruptLava และ IceShardBlock
    public abstract bool HandleElementContact(ElementType type, PlayerUnit player);

    // เมธอดเสริมสำหรับ Level Design (ใช้สำหรับหาตำแหน่งบน Grid)
    public Vector3 GetGridPosition()
    {
        // คืนค่าตำแหน่งที่ถูกปัดเศษ (เพื่อให้ตรงกับ Grid)
        return new Vector3(Mathf.Round(transform.position.x),
                           Mathf.Round(transform.position.y),
                           0);
    }
}