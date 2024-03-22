using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject sprite;
    private Animator anim;
    private float faceDir = 1;
    private bool isFacingRight = true;
    private Vector3 moveInput = new Vector3(0f, 0f, 0f);

    public float moveSpeed;


    public float pickupRange = 1.5f;

    //public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons, assignedWeapons;

    public int maxWeapons = 3;

    [HideInInspector]
    public List<Weapon> fullyLevelledWeapons = new List<Weapon>();

    private void Awake()
    {
        instance = this;
        anim= GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
        moveSpeed = PlayerStatController.instance.moveSpeed[0].value;
        pickupRange = PlayerStatController.instance.pickupRange[0].value;
        maxWeapons = Mathf.RoundToInt( PlayerStatController.instance.maxWeapons[0].value);
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //Debug.Log(moveInput);

        moveInput.Normalize();

        //Debug.Log(moveInput);

        transform.position += moveInput * moveSpeed * Time.deltaTime;
        if (moveInput != Vector3.zero)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }
            FlipController();
    }

    public void AddWeapon(int weaponNumber)
    {
        if(weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
            UIController.instance.UpdateWeapon();
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
        UIController.instance.UpdateWeapon();
    }
    private void Flip()
    {
        sprite.transform.localScale = new Vector3(-faceDir, 1, 1);
        faceDir = -faceDir;
        isFacingRight = !isFacingRight;
    }
    private void FlipController()
    {
        if(isFacingRight && moveInput.x <0)
        {
            Flip();
        }
        else if(!isFacingRight && moveInput.x >0)
        {
            Flip();
        }
    }
}
