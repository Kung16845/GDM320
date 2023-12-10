using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Animation_PlayerMovement : MonoBehaviour
{
    private GunSpeedManager gunSpeedManager;
    Animator animator;
    public Vector2 movement;
    Vector2 mPmovement;
    Transform player;
    bool isAiming = false;
    bool isOnSpecialAnimation = false;
    bool isDead = false;
    public GameObject OnHandItemHolder;
    public GameObject PlayerOnItemHolder;
    public Animation_ItemWithPlayer item;

    public Pistol GunHolder;

    string currentActiveLayer = "nothing";

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        gunSpeedManager = FindObjectOfType<GunSpeedManager>();
        OnHandItemHolder = FindInActiveObjectByName("BgOnHand");
    }
    void Update()
    {
        if (!isOnSpecialAnimation)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (Input.GetKey(KeyCode.LeftShift)) { animator.SetBool("isRunning", true); }
            else { animator.SetBool("isRunning", false); }
            if (Input.GetKey(KeyCode.LeftControl)) { animator.SetBool("isSlow", true); }
            else { animator.SetBool("isSlow", false); }

            if (OnHandItemHolder.transform.childCount > 0)
            {
                switch (checkItemOnHand())
                {
                    case "Pistol": resetAnimLayerTo("HoldGun"); break;

                    default: resetAnimLayerTo("HoldItem"); 
                        
                        //PlayerOnItemHolder.SetActive(true); item.changItemOnHandTo(checkItemOnHand());
                        break;
                }
            }
            else { resetAnimLayerTo("nothing"); PlayerOnItemHolder.SetActive(false); }

            if (currentActiveLayer == "HoldGun" || currentActiveLayer == "AimGun" || currentActiveLayer == "ReloadGun")
            {
                if (GunHolder.isAiming) { resetAnimLayerTo("AimGun"); }
                else if (GunHolder.isReloading) { resetAnimLayerTo("ReloadGun"); }
                else { resetAnimLayerTo("HoldGun"); }
            }


            if (GunHolder.isAiming) { aimRotate(); }
            else { normalRotate(); }



            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }
    void normalRotate()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            transform.position = new Vector3(player.position.x, player.position.y);
        }
    }

    void aimRotate()
    {
        Vector2 refLine = new Vector2(Screen.width, Screen.height / 2) - new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mP = (Vector2)Input.mousePosition - (new Vector2(Screen.width / 2, Screen.height / 2));

        mPmovement.x = Mathf.Cos(Mathf.Deg2Rad * Vector2.Angle(refLine, mP));

        refLine = new Vector2(Screen.width / 2, Screen.height) - new Vector2(Screen.width / 2, Screen.height / 2);

        mPmovement.y = Mathf.Cos(Mathf.Deg2Rad * Vector2.Angle(refLine, mP));


        animator.SetFloat("Horizontal", mPmovement.x);
        animator.SetFloat("Vertical", mPmovement.y);
        transform.position = new Vector3(player.position.x, player.position.y);
    }

    string checkItemOnHand()
    {
        var slot = OnHandItemHolder.GetComponentInChildren<UIItemCharactor>();

        return slot.nameItem.text;
    }
    void resetAnimLayerTo(string newAnim)
    {
        animator.SetLayerWeight(animator.GetLayerIndex(currentActiveLayer), 0);
        animator.SetLayerWeight(animator.GetLayerIndex(newAnim), 1);
        currentActiveLayer = newAnim;
    }

    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
    public void GetSanityBottle()
    {
        isOnSpecialAnimation = true;
        resetAnimLayerTo("Special");
        animator.SetInteger("SpecialAction", 1);
    }

    public void HitByTrap()
    {
        isOnSpecialAnimation = true;
        resetAnimLayerTo("Special");
        animator.SetInteger("SpecialAction", 2);
    }
    public void GetHurt()
    {
        isOnSpecialAnimation = true;
        resetAnimLayerTo("Special");
        animator.SetInteger("SpecialAction", 3);
    }
    public void DEAD()
    {
        isDead = true;
        isOnSpecialAnimation = true;
        resetAnimLayerTo("Special");
        animator.SetInteger("SpecialAction", 4);
    }
    public void GetBandage()
    {
        isOnSpecialAnimation = true;
        resetAnimLayerTo("Special");
        animator.SetInteger("SpecialAction", 5);
    }


    enum SpecialAction
    {
        Drink = 1,
        trap = 2,
        hurt =3
    }

   
    public void exitSpecial()
    {
        if (!isDead)
        {
            StartCoroutine(ExitSpecialAnim());
        }
    }
    IEnumerator ExitSpecialAnim()
    {
        animator.SetInteger("SpecialAction", 0);

        yield return new WaitForSeconds(1);

        isOnSpecialAnimation = false;

        resetAnimLayerTo("Normal");
    }
}
