using UnityEngine;
using UnityEngine.UI; // ต้องมีบรรทัดนี้สำหรับ Text/Image UI

public class UIManager : MonoBehaviour
{

    // อ้างอิง PlayerUnit (กำหนดใน Inspector)
    public PlayerUnit player;

    // อ้างอิง UI Components (ต้องสร้างใน Canvas ก่อน)
    public Text healthText;
    public Text elementText;
    public Image playerElementImage; // ใช้เพื่อเปลี่ยนสีพื้นหลังตาม Element

    void Update()
    {
        if (player == null || healthText == null) return;

        // ดึงค่า Health (ใช้ Getter จาก UnitStats ผ่าน Encapsulation)
        healthText.text = "Health: " + player.Health;

        // ดึงค่า Element Form
        ElementType currentElement = player.CurrentElement;
        elementText.text = "Form: " + currentElement.ToString();

        // เปลี่ยนสี UI ตาม Element Form (Past/Present)
        if (currentElement == ElementType.Present)
        {
            elementText.color = Color.cyan;
            playerElementImage.color = new Color(0.2f, 0.4f, 0.8f); // สีน้ำเงิน
        }
        else
        {
            elementText.color = Color.yellow;
            playerElementImage.color = new Color(0.8f, 0.5f, 0.2f); // สีส้ม
        }
    }
}