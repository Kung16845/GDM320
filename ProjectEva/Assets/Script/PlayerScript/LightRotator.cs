using UnityEngine;

public class LightRotator : MonoBehaviour
{
    [SerializeField] Transform lightTransform;

    bool isWasLimited;

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        //สำหรับดูทิศทางเมาส์
        //Debug.DrawLine(transform.position, mousePosition, Color.red);

        //ทิศทาง 1 หน่วยของเมาส์
        var mouseDir = (mousePosition - transform.position).normalized;
        var playerDir = transform.up;
        var mouseAngle = GetAngleFromDirection(mouseDir);
        var playerAngle = GetAngleFromDirection(playerDir);

        var limitOffset = 45f;
        var minAngle = playerAngle - limitOffset;
        var maxAngle = playerAngle + limitOffset;   
        
        //player อยู่ควอแดน 4 เพราะ maxAngle เกินหรือเท่ากับ 360
        if (maxAngle >= 360)
        {
            maxAngle -= 360f;
            minAngle -= 360f;
            
            //ถ้า mouse อยู่ควอแดน 4 ทำให้องศาติดลบ
            if (mouseAngle > 180f)
                mouseAngle -= 360f;
        }
        else
        {
            //player อยู่ควอแดน 1 เพราะ minAngle ติดลบ
            if (minAngle < 0)
            {
                //ถ้า mouse อยู่ควอแดน 4 ทำให้องศาติดลบ
                if (mouseAngle > 180f)
                    mouseAngle -= 360f;
            }
        }
        
        var clampedAngle = Mathf.Clamp(mouseAngle, minAngle, maxAngle);
        
        //สำหรับตรวจสอบค่ามุม
        //Debug.Log( $"Mouse : {mouseAngle}, Min : {minAngle}, Max : {maxAngle}, Clamped {clampedAngle}");
        
        //เมาส์ถูก limit รีเปล่า? เพราะค่า clampedAngle ไม่เท่า mouseAngle เดิม
        if (!Mathf.Approximately(clampedAngle, mouseAngle))
        {
            //ไม่เคยถูก Limit มาก่อน
            if (!isWasLimited)
            {
                lightTransform.rotation = Quaternion.Euler(new Vector3(0,0, clampedAngle));
                
                //กำหนดสถานะถูก Limit
                isWasLimited = true;
            }
        }
        else
        {
            lightTransform.rotation = Quaternion.Euler(new Vector3(0,0, clampedAngle));
            
            //ปลดสถานะถูก Limit
            isWasLimited = false;
        }
       
    }

    float GetAngleFromDirection(Vector3 direction)
    {
        float angleRadians = Mathf.Atan2(direction.y, direction.x);
        var angleDegrees = angleRadians * Mathf.Rad2Deg;
        
        //Atan2 จะ return ค่าระหว่าง -180 ถึง 180 ดังนั้น ถ้า angleDegrees น้อยกว่า 0 ให้บวกเพิ่มไป 360
        if (angleDegrees < 0)
            angleDegrees += 360;
        
        //ค่าองศา ที่มีค่าระหว่าง 0-360
        return angleDegrees;
    }
}
