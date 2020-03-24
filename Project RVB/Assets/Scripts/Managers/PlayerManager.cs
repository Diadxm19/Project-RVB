using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera ViewPointMain;
    private GameObject CharacterOne;
    private GameObject CharacterTwo;
    private GameObject CharacterThree;
    private GameObject CharacterSelected;
    public Vector3 offset = new Vector3(0, -10, 0);
    private float smoothspeed = 0.125f;

    private GameObject[] enemyUnits;

    public  void PlayerManagerStart()
    {
        ViewPointMain = GameObject.Find("Main Camera").GetComponent<Camera>();
        CharacterOne = GameObject.Find("Character1");
        CharacterTwo = GameObject.Find("Character2");
        CharacterThree = GameObject.Find("Character3");
        CharacterSelected = CharacterOne;

    }

    private void SetCameraOnCharacter(GameObject character)
    {
        Vector3 DesiredPosition = new Vector3(character.transform.position.x, ViewPointMain.transform.position.y, character.transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(ViewPointMain.transform.position, DesiredPosition, smoothspeed);
        ViewPointMain.transform.position = smoothedPosition;

        transform.LookAt(character.transform);
    }

    private void MoveCharacter(GameObject go)
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = ViewPointMain.ScreenPointToRay(Input.mousePosition);
            Vector3 directionToFace = ViewPointMain.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit[] hit;

            hit = Physics.RaycastAll(ray);
            for (int i = 0; i < hit.Length; i++)
            {
                if (go.layer == hit[i].collider.gameObject.layer && hit[i].collider.tag == "CharacterView" && go.GetComponent<Operators>().GetCharacterCanMove())
                {
                    go.GetComponent<NavMeshAgent>().SetDestination(hit[i].point);
                    go.GetComponent<Operators>().SetCharacterCanMove(false);
                }
            }
        }
    }

    private void FindAllEnemys()
    {

    }

    private void SwitchCharacter()
    {
        if (Input.GetKeyDown("1"))
        {
            CharacterSelected = CharacterOne;
        }
        else if (Input.GetKeyDown("2"))
        {
            CharacterSelected = CharacterTwo;
        }
        else if (Input.GetKeyDown("3"))
        {
            CharacterSelected = CharacterThree;
        }
    }

    private void PlayerControls()
    {
        MoveCharacter(CharacterSelected);

        SwitchCharacter();
    }

    public void PlayerManagerFixedUpdate()
    {
        SetCameraOnCharacter(CharacterSelected);
    }

    // Update is called once per frame
    public void PlayerManagerUpdate()
    {
        PlayerControls();

    }
}
