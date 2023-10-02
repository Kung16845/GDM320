using System.Collections;
using Enemy_State;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTeleport : MonoBehaviour
{
    public NavMeshAgent agent;  // NavMeshAgent ของตัวละคร
    public Transform teleportPointA;  // จุด teleport หนึ่ง
    public Transform teleportPointB;  // จุด teleport สอง
    public bool isReadyTeleport = true;  // ตัวแปรเพื่อตรวจสอบว่าสามารถ teleport ได้หรือไม่
    public Transform EnemyForTeleport;
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        Debug.Log(" Ready for  Teleported");
        if (isReadyTeleport && enemy.GetComponent<EnemyNormal>() != null)  // ตรวจสอบว่า GameObject ที่ติด tag ว่า "Enemy" ได้ปรากฏใน collider
        {
            Debug.Log(" Use Teleported");
            if (enemy.GetComponent<EnemyNormal>().isUsingTunnel)  // ตรวจสอบว่า enemy มี component EnemyNormal และ isUsingTunnel เป็น true
            {   
                Debug.Log(" Use Teleported 2.0");
                // ตรวจสอบว่าตัวละครอยู่ที่จุด teleport หนึ่งหรือสอง แล้วทำการ teleport ไปยังจุดปลายทาง
                if (Vector2.Distance(EnemyForTeleport.position, teleportPointA.position) < 3f)
                {
                    agent.Warp(teleportPointB.position);
                    Debug.Log("Teleported to Point B");
                }
                else if (Vector2.Distance(EnemyForTeleport.position, teleportPointB.position) < 3f)
                {
                    agent.Warp(teleportPointA.position);
                    Debug.Log("Teleported to Point A");
                }

                isReadyTeleport = false;  // ตั้งค่า isReadyTeleport เป็น false เพื่อป้องกันการ teleport ซ้ำ
                enemy.GetComponent<EnemyNormal>().agent.SetDestination(enemy.GetComponent<EnemyNormal>().targetPosition);  // ตั้งค่าปลายทางใหม่ของ enemy
                StartCoroutine(DelayTime(5.0f));  // เรียกใช้ coroutine เพื่อรีเซ็ต isReadyTeleport หลังจากหน่วงเวลา 5 วินาที
            }
        }
    }

    private IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
        isReadyTeleport = true;  // ตั้งค่า isReadyTeleport เป็น true อีกครั้งหลังจากหน่วงเวลา
    }
}
