using UnityEngine;

// Enum: รายการธาตุที่เราใช้ (Present / Past)
public enum ElementType
{
    Present, // ปัจจุบัน (Element หลัก)
    Past     // อดีต (Element สลับ)
}

[System.Serializable]
public class UnitStats
{

    // Encapsulation: ตัวแปรเป็น Private (ซ่อนข้อมูล)
    // ใช้ [SerializeField] เพื่อให้เห็นค่าใน Unity Inspector
    [SerializeField] private int _health = 10;
    [SerializeField] private ElementType _currentElement = ElementType.Present;
    [SerializeField] private int _actionPoints = 1;

    // Public Getter/Setter: ควบคุมการเข้าถึงข้อมูล
    // get; private set; หมายความว่า: อ่านค่าได้จากภายนอก แต่แก้ไขได้จากภายในคลาสนี้เท่านั้น
    public int Health
    {
        get { return _health; }
        private set { _health = value; }
    }

    public ElementType CurrentElement
    {
        get { return _currentElement; }
        private set { _currentElement = value; }
    }

    public int ActionPoints
    {
        get { return _actionPoints; }
        private set { _actionPoints = value; }
    }

    // เมธอดสำหรับแก้ไขค่า Health (ช่องทางเดียวในการลด/เพิ่มเลือด)
    public void ChangeHealth(int amount)
    {
        this.Health += amount;
        if (this.Health < 0) this.Health = 0;

        Debug.Log("Health changed by: " + amount + ". New Health: " + this.Health);
    }

    // เมธอดสำหรับเปลี่ยน Element
    public void ChangeElement(ElementType newElement)
    {
        this.CurrentElement = newElement;
        Debug.Log("Element shifted to: " + newElement);
    }

    // เมธอดสำหรับใช้ Action Point
    public bool UseActionPoint(int cost = 1)
    {
        if (this.ActionPoints >= cost)
        {
            this.ActionPoints -= cost;
            Debug.Log("Used Action Point(s). Remaining: " + this.ActionPoints);
            return true;
        }
        Debug.Log("Not enough Action Points.");
        return false;
    }
}