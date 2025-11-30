using UnityEngine;

// 2. Inheritance: PlayerUnit สืบทอดคุณสมบัติทั้งหมดจาก AbstractUnit
public class PlayerUnit : AbstractUnit
{

    // เมธอดสำหรับสลับ Element (เรียกใช้เมื่อผู้เล่นกดปุ่ม E)
    public void ShiftElement()
    {
        // ใช้ ElementType ที่เป็น enum เพื่อกำหนด Element ใหม่
        ElementType newElement = Stats.CurrentElement == ElementType.Present ? ElementType.Past : ElementType.Present;

        // เรียกใช้เมธอด ShiftElement จาก AbstractUnit (Inheritance/Encapsulation)
        base.ShiftElement(newElement);
    }

    // Logic สำหรับการโต้ตอบกับ IElementInteractive Objects (Terrain)
    // นี่คือส่วนที่เรียกใช้ Polymorphism
    public void InteractWith(IElementInteractive targetObject)
    {

        // targetObject เป็น Interface, โค้ดที่ถูกเรียกใช้จะแตกต่างกันไปตามชนิดของวัตถุ (Lava, Ice)
        // ส่ง Element ปัจจุบันของผู้เล่นไป เพื่อให้วัตถุตอบสนอง (Polymorphism)
        bool success = targetObject.HandleElementContact(this.CurrentElement, this);

        if (!success)
        {
            // ถ้าโต้ตอบไม่สำเร็จ (Logic ใน CorruptLava หรือ IceBlock ส่ง false กลับมา)
            // ตัวอย่าง: ถ้าชน CorruptLava ใน Present Form จะรับ Damage
            TakeDamage(5);
            Debug.Log("Interaction Failed! Took 5 Damage.");
        }
        else
        {
            // ถ้าโต้ตอบสำเร็จ (เช่น IceBlock ถูกทำลาย หรือ Anchor ถูกเปิดใช้งาน)
            Debug.Log("Interaction Success!");
        }
    }

    // 1. Abstract Method Implementation: Player มี Logic การโต้ตอบของตัวเอง
    // Override จาก AbstractUnit เพื่อกำหนดว่า PlayerUnit จะทำอะไรเมื่อถูก Element อื่นสัมผัส
    public override void HandleElementContact(ElementType element, IElementInteractive sourceObject)
    {
        // ในเกมนี้ Player อาจถูก Enemy Attack หรือโดน Effect จาก Terrain Object อื่น
        Debug.Log($"Player received element signal: {element}");
    }

    // 1. Abstract Method Implementation: Logic การตายของ Player
    // Override จาก AbstractUnit เพื่อกำหนดพฤติกรรมการตายที่เฉพาะเจาะจง
    protected override void Die()
    {
        Debug.Log("Player Died. Game Over.");
        // Logic การโหลดฉากใหม่ หรือแสดง Game Over UI
    }
}