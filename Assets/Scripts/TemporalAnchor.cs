using UnityEngine;

// Polymorphism 3: Temporal Anchor ตอบสนองต่อธาตุ Present/Past
// คลาสนี้สืบทอดจาก TerrainObject และ Implement IElementInteractive ผ่านคลาสแม่
public class TemporalAnchor : TerrainObject
{

    // Encapsulation: ตัวแปร Private สำหรับสถานะการเปิดใช้งาน
    [SerializeField] private bool _isActivated = false;

    // Public Getter: ให้ GameManager อ่านสถานะได้ (สำคัญสำหรับการเช็ค Win Condition)
    public bool IsActivated => _isActivated;

    // Override จาก Abstract Method ใน TerrainObject (Polymorphism)
    public override bool HandleElementContact(ElementType type, PlayerUnit player)
    {
        if (_isActivated)
        {
            // หากเปิดใช้งานแล้ว ไม่ต้องทำอะไร
            return true;
        }

        if (type == ElementType.Present)
        {
            // Logic: ใช้ Present Form (ธาตุปัจจุบัน) เพื่อเปิดใช้งานจุดยึดเวลา
            _isActivated = true;
            Debug.Log("Temporal Anchor Activated! LEVEL COMPLETE.");
            // ส่ง true กลับไปเพื่อบอกว่าการโต้ตอบสำเร็จ
            return true;
        }
        else
        {
            // Logic: Past Form (ธาตุอดีต) ไม่สามารถเปิดใช้งาน Anchor ได้
            Debug.Log("Cannot activate Anchor in Past Form.");
            // ส่ง false กลับไปเพื่อบอกว่าการโต้ตอบไม่สำเร็จ
            return false;
        }
    }
}
