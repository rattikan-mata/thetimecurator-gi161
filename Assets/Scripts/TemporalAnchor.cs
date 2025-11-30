using UnityEngine;

// Polymorphism 3: Temporal Anchor ตอบสนองต่อธาตุ Present/Past
public class TemporalAnchor : TerrainObject
{

    private bool isActivated = false;

    // Override จาก Abstract Method ใน TerrainObject (Polymorphism)
    public override bool HandleElementContact(ElementType type, PlayerUnit player)
    {
        if (isActivated)
        {
            return true; // ไม่ต้องทำอะไรแล้ว
        }

        if (type == ElementType.Present)
        {
            // Logic: ใช้ Present Form เพื่อเปิดใช้งานจุดยึดเวลา
            isActivated = true;
            Debug.Log("Temporal Anchor Activated! LEVEL COMPLETE.");
            // Logic ชนะเกม (GameManager.WinGame())
            return true;
        }
        else
        {
            // Logic: Past Form ไม่สามารถเปิดใช้งาน Anchor ได้
            Debug.Log("Cannot activate Anchor in Past Form.");
            return false;
        }
    }
}