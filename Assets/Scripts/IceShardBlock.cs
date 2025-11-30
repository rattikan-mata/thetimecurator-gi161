using UnityEngine;

// Polymorphism 2: IceShardBlock ตอบสนองต่อธาตุ Present/Past
// คลาสนี้สืบทอดจาก TerrainObject และ Implement IElementInteractive ผ่านคลาสแม่
public class IceShardBlock : TerrainObject
{

    // Override จาก Abstract Method ใน TerrainObject (Polymorphism)
    public override bool HandleElementContact(ElementType type, PlayerUnit player)
    {
        Debug.Log($"Ice Block Contact: Player's Element is {type}");

        if (type == ElementType.Present)
        {
            // Logic: ถ้าอยู่ใน Present Form (พลังงานบริสุทธิ์) น้ำแข็งจะถูกสลาย
            Debug.Log("Ice Block shattered by Present Form energy! Path opened.");
            // ทำลายตัวเอง (แสดงการเปิดทาง)
            Destroy(this.gameObject);

            // ส่ง true กลับไป เพื่ออนุญาตให้ PlayerUnit เดินผ่านพื้นที่นี้ได้
            return true;
        }
        else
        {
            // Logic: ถ้าอยู่ใน Past Form (พลังงานเสื่อมสลาย/อดีต) น้ำแข็งจะแข็งตัวและบล็อกทางเดิน
            Debug.Log("Ice Block is solid in Past Form. Movement is blocked.");

            // ส่ง false กลับไป (ห้ามเคลื่อนที่)
            return false;
        }
    }
}