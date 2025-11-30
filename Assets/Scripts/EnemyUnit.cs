using UnityEngine;

// 2. Inheritance: EnemyUnit สืบทอดคุณสมบัติทั้งหมดจาก AbstractUnit
public class EnemyUnit : AbstractUnit
{

    // Logic สำหรับ AI (จะพัฒนาต่อในขั้นตอนถัดไป)
    public void Behavior()
    {
        // AI Logic จะถูกเขียนที่นี่
        Debug.Log($"Enemy {gameObject.name} is thinking...");
        // ตัวอย่าง: โจมตีผู้เล่น
        // FindObjectOfType<PlayerUnit>().TakeDamage(1); 
    }

    // 1. Abstract Method Implementation: การคำนวณ Damage ของศัตรู
    public override int CalculateDamage(AbstractUnit target)
    {
        // Logic คำนวณ Damage พื้นฐานของศัตรู
        int baseDamage = 1;

        // Polymorphism Logic: หากศัตรูอยู่ใน Past Form อาจจะทำ Damage เพิ่มขึ้น
        if (this.CurrentElement == ElementType.Past)
        {
            baseDamage += 1;
        }

        return baseDamage;
    }

    // 4. Polymorphism (Method Overloading): เมธอดที่มีชื่อเดียวกัน แต่รับพารามิเตอร์ต่างกัน
    public int Attack(AbstractUnit target)
    {
        int damage = CalculateDamage(target);
        target.TakeDamage(damage);
        return damage;
    }

    public int Attack(AbstractUnit target, float damageMultiplier)
    {
        int damage = (int)(CalculateDamage(target) * damageMultiplier);
        target.TakeDamage(damage);
        return damage;
    }


    // 1. Abstract Method Implementation: Logic การโต้ตอบกับ Element
    public override void HandleElementContact(ElementType element, IElementInteractive sourceObject)
    {
        // Logic การจัดการเมื่อ Enemy โดน Element โจมตี/กระตุ้น (เช่น โดนศัตรูโจมตี)
        // อาจจะถูก Stun หรือติดสถานะอื่น ๆ
        if (element == ElementType.Past)
        {
            TakeDamage(2); // Past Form อาจทำ Damage ได้ดีกว่า
        }
        else
        {
            TakeDamage(1);
        }
    }

    // 1. Abstract Method Implementation: Logic การตายของศัตรู
    protected override void Die()
    {
        Debug.Log($"Enemy {gameObject.name} destroyed.");
        // Logic การให้คะแนนหรือ Item Drop
        Destroy(this.gameObject);
    }
}