using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject bomb;

    private Rigidbody rb;

    public int heldAmmo = 4;
    public int maxAmmo = 4;
    public int currentAmmo = 4;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && currentAmmo > 0)
        {
            switch(transform.gameObject.name)
            {
                case "explosive_4":
                    Inventory.instance.UseItem("Molotov Cocktail", 1);
                    break;

                case "explosive_1":
                    Inventory.instance.UseItem("Pipe Bomb", 1);
                    break;

                case "explosive_2":
                    Inventory.instance.UseItem("Bile Bomb", 1);
                    break;

                case "explosive_3":
                    Inventory.instance.UseItem("Stun Grenade", 1);
                    break;

                default:
                    break;
                    
            }
            currentAmmo -= 1;
            yeet();
        }
    }

    void yeet()
    {
        GameObject proj = Instantiate(bomb, transform.position, transform.rotation);
        proj.AddComponent<Rigidbody>();
        proj.AddComponent<CapsuleCollider>();
        //bomb.AddComponent<Explode>();
        rb = proj.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddForce(proj.transform.forward * 500f);
        rb.AddForce(proj.transform.up * 200f);
        rb.useGravity = true;
    }

    public void addAmmo()
    {
        if(currentAmmo < heldAmmo)
        {
            currentAmmo++;
        }
    }
}
