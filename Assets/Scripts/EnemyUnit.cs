using UnityEngine;

// 2. Inheritance: EnemyUnit สืบทอดคุณสมบัติทั้งหมดจาก AbstractUnit
public class EnemyUnit : AbstractUnit
{

    // 1. Abstract Method Implementation: การคำนวณ Damage ของศัตรู
    // นี่คือ Method Overriding ของ abstract method จาก AbstractUnit
    public int CalculateDamage(AbstractUnit target)
    {
        int baseDamage = 1;

        // Polymorphism Logic: หากศัตรูอยู่ใน Past Form (element ตรงข้าม) อาจจะทำ Damage เพิ่มขึ้น
        if (this.CurrentElement == ElementType.Past)
        {
            baseDamage += 1;
        }

        return baseDamage;
    }

    // 4. Polymorphism (Method Overloading): เมธอดที่มีชื่อเดียวกัน แต่รับพารามิเตอร์ต่างกัน
    // เมธอด Attack มาตรฐาน
    public int Attack(AbstractUnit target)
    {
        // ใช้ CalculateDamage เวอร์ชันมาตรฐาน
        int damage = CalculateDamage(target);
        target.TakeDamage(damage);
        return damage;
    }

    // 4. Polymorphism (Method Overloading): Attack ที่มีตัวคูณความเสียหาย (เช่น Critical Hit)
    public int Attack(AbstractUnit target, float damageMultiplier)
    {
        // ใช้ CalculateDamage เวอร์ชัน Overloaded
        int damage = (int)(CalculateDamage(target) * damageMultiplier);
        target.TakeDamage(damage);
        return damage;
    }

    // 1. Abstract Method Implementation: Logic การโต้ตอบกับ Element (Polymorphism)
    // Override จาก AbstractUnit
    public override void HandleElementContact(ElementType element, IElementInteractive sourceObject)
    {
        // Enemy เป็น Time Anomaly: แพ้ Present Form (พลังงานบริสุทธิ์)
        if (element == ElementType.Present)
        {
            TakeDamage(3); // รับ Damage มากกว่าเมื่อโดนธาตุที่ตรงข้าม
        }
        else
        {
            TakeDamage(1);
        }
    }

    // 1. Abstract Method Implementation: Logic การตายของศัตรู
    // Override จาก AbstractUnit
    protected override void Die()
    {
        Debug.Log($"Enemy {gameObject.name} destroyed. Temporal Anomaly neutralized.");
        Destroy(this.gameObject);
    }

    // เมธอด Behavior จะถูกเรียกใช้ผ่าน EnemyAI script
    public void Behavior()
    {
        // Logic AI จะถูกสั่งการและควบคุมใน EnemyAI.cs
    }
}
