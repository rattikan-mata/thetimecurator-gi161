using UnityEngine;

// 2. Inheritance: PlayerUnit สืบทอดคุณสมบัติทั้งหมดจาก AbstractUnit
// Implement Logic เฉพาะสำหรับตัวละครหลัก
public class PlayerUnit : AbstractUnit
{

    // เมธอดสำหรับสลับ Element (เรียกใช้เมื่อผู้เล่นกดปุ่ม)
    public void ShiftElement()
    {
        // ใช้ ElementType ที่เป็น enum เพื่อกำหนด Element ใหม่
        ElementType newElement = _stats.CurrentElement == ElementType.Present ? ElementType.Past : ElementType.Present;

        // เรียกใช้เมธอด ShiftElement จาก AbstractUnit (Inheritance/Encapsulation)
        base.ShiftElement(newElement);
    }

    // Logic สำหรับการโต้ตอบกับ IElementInteractive Objects (Terrain)
    // นี่คือส่วนที่เรียกใช้ Polymorphism
    public void InteractWith(IElementInteractive targetObject)
    {
        // targetObject เป็น Interface, โค้ดที่ถูกเรียกใช้จะแตกต่างกันไปตามชนิดของวัตถุ (Lava, Ice)
        bool success = targetObject.HandleElementContact(this.CurrentElement, this);

        if (!success)
        {
            // ถ้าโต้ตอบไม่สำเร็จ (อุปสรรคทำลายผู้เล่น หรือผู้เล่นต้องรับ Damage)
            // ในเกมนี้ Lava, Ice จะจัดการผลลัพธ์ แต่ถ้า Logic บอกว่าไม่สำเร็จ อาจจะรับ Damage เล็กน้อย
            TakeDamage(1);
            Debug.Log("Interaction Failed! Took 1 Damage.");
        }
        else
        {
            Debug.Log("Interaction Success!");
        }
    }

    // 1. Abstract Method Implementation: Player มี Logic การโต้ตอบของตัวเอง
    // Override จาก AbstractUnit เพื่อกำหนดว่า PlayerUnit จะทำอะไรเมื่อถูก Element อื่นสัมผัส
    public override void HandleElementContact(ElementType element, IElementInteractive sourceObject)
    {
        // Logic การจัดการเมื่อ Player โดน Element โจมตี/กระตุ้น (เช่น โดนศัตรูโจมตี)
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