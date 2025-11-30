using UnityEngine;

// Polymorphism 1: CorruptLava ตอบสนองต่อธาตุ Present/Past
// คลาสนี้สืบทอดจาก TerrainObject และ Implement IElementInteractive ผ่านคลาสแม่
public class CorruptLava : TerrainObject
{

    // Override จาก Abstract Method ใน TerrainObject (Polymorphism)
    public override bool HandleElementContact(ElementType type, PlayerUnit player)
    {
        Debug.Log($"Lava Contact: Player's Element is {type}");

        if (type == ElementType.Past)
        {
            // Logic: ถ้าอยู่ใน Past Form ลาวาจะเย็นตัวลงชั่วคราว (เดินข้ามได้)
            Debug.Log("Lava is currently cooled. Movement successful.");
            // ส่ง true กลับไป เพื่ออนุญาตให้ PlayerUnit เดินผ่าน
            return true;
        }
        else
        {
            // Logic: ถ้าอยู่ใน Present Form ลาวาจะร้อนจัดและทำลายผู้เล่น
            Debug.Log("Lava is too hot in Present Form! Player takes damage.");

            // ทำ Damage ผู้เล่น (Damage Logic ถูกควบคุมโดย Encapsulation ใน PlayerUnit.TakeDamage)
            player.TakeDamage(5);

            // ส่ง false กลับไป เพื่อบอก PlayerUnit ว่าการโต้ตอบไม่สำเร็จ (ห้ามเดินผ่าน)
            return false;
        }
    }
}