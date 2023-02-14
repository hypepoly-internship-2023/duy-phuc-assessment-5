using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject cube1, cube2, cube3;

    [HideInInspector]
    private Vector3 originPosCube1, originPosCube2;
    public int selectedMove;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    Vector3 cubeHitPos;
    Rigidbody cube1RB;
    bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        cube1RB = cube1.GetComponent<Rigidbody>();
        originPosCube1 = cube1.transform.position;
        originPosCube2 = cube2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            cubeHitPos = hit.collider.gameObject.transform.position;
            if(Input.GetMouseButtonDown(0))
            {
               switch(selectedMove)
                {
                    // rigidbody.velocity
                    case 0:
                        canMove = true;
                        break;
                    // rigidbody.MovePosition 
                    case 1:
                        canMove = true;
                        break;
                    //  transform.Translate
                    case 2:
                        cube1.transform.Translate(cubeHitPos);
                        break;
                    // transform.position
                    case 3:
                        cube1.transform.position = cubeHitPos;
                        break;
                    default:
                        break;
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                canMove = false;
            }
        }
    }
    void FixedUpdate()
    {
        if(!canMove) { cube1RB.velocity = Vector2.zero; }
        if(canMove == true)
        {
            if(selectedMove == 1)
            {
                cube1RB.MovePosition(cubeHitPos);
            }
            if(selectedMove == 0)
            {
                Vector3 direction = cubeHitPos - cube1.transform.position;
                cube1RB.velocity = new Vector2(direction.x, direction.y);
            }
        }
    }

    public void OnDropdownChanged(TMP_Dropdown dropdown)
    {
        selectedMove = dropdown.value;
    }

    public void Reset()
    {
        cube1.transform.position = originPosCube1;
        cube2.transform.position = originPosCube2;
    }
}
