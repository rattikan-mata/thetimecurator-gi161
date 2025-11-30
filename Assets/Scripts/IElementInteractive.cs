// คะแนนพิเศษ: Interface (IElementInteractive)
// Interface นี้กำหนดสัญญาว่า วัตถุใด ๆ ที่ Implement จะต้องมีเมธอด HandleContact
public interface IElementInteractive
{

    // สัญญา: เมื่อถูกสัมผัสโดย Element หนึ่ง จะต้องมีการตอบสนองกลับมาเป็น bool
    // type: คือ ElementType (Present/Past) ของผู้เล่นที่กำลังสัมผัส
    // player: คือ PlayerUnit ที่กำลังโต้ตอบ
    bool HandleElementContact(ElementType type, PlayerUnit player);
}