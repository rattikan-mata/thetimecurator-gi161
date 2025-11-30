using UnityEngine;
using UnityEngine.UI; // ต้องมีบรรทัดนี้สำหรับ Text/Image UI
using System;

public class UIManager : MonoBehaviour
{

    // อ้างอิง PlayerUnit (กำหนดใน Inspector)
    public PlayerUnit player;

    // อ้างอิง UI Components (ต้องสร้างใน Canvas ก่อน)
    public Text healthText;
    public Text elementText;
    public Text apText;
    public Image playerElementImage; // ใช้เพื่อเปลี่ยนสีพื้นหลังตาม Element

    void Update()
    {
        // ตรวจสอบ Player/UI เพื่อป้องกัน Error
        if (player == null || healthText == null) return;

        // ดึงค่า Health (ใช้ Getter จาก AbstractUnit -> Encapsulation)
        healthText.text = $"Health: {player.Health}";

        // ดึงค่า AP (ใช้ Getter จาก AbstractUnit.Stats -> Encapsulation)
        apText.text = $"AP: {player.Stats.ActionPoints}";

        // ดึงค่า Element Form
        ElementType currentElement = player.CurrentElement;
        elementText.text = $"Form: {currentElement.ToString()}";

        // เปลี่ยนสี UI ตาม Element Form (Past/Present)
        if (playerElementImage != null)
        {
            Color colorPresent = new Color(0.2f, 0.4f, 0.8f); // สีน้ำเงิน
            Color colorPast = new Color(0.8f, 0.5f, 0.2f); // สีส้ม

            if (currentElement == ElementType.Present)
            {
                elementText.color = Color.cyan;
                playerElementImage.color = colorPresent;
            }
            else
            {
                elementText.color = Color.yellow;
                playerElementImage.color = colorPast;
            }
        }
    }
}