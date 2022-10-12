using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public GameObject MainCamera;
    public GameObject ButtonGroup;

    public Vector3 MinigunCamera;

    public GameObject SniperCross;
    public GameObject MinigunCross;

    public GameObject Minigun_BTN;

    public GameObject Explosion;
    public int miniUse;
    int miniTime;
    bool Exp = false;
    int explosive;
    public TextMeshProUGUI minigunSecond_TXT;
    public TextMeshProUGUI miniUse_TXT;
    public GameObject Timer_TXT;
    private void Awake()
    {
        Instance = this;
        miniUse = 7;
    }
    public void SniperActive()
    {
        MainCamera.GetComponent<SniperComponent>().enabled = true;
        MainCamera.GetComponent<Camera>().DOFieldOfView(10, 0.5f);

        ButtonGroup.SetActive(false);
        SniperCross.SetActive(true);
        CameraController.Instance.speedH = 0.2f;
        CameraController.Instance.speedV = 0.2f;
    }
    public void SniperDeactive()
    {
        MainCamera.GetComponent<SniperComponent>().enabled = false;
        MainCamera.GetComponent<Camera>().DOFieldOfView(27, 0.5f);

        ButtonGroup.SetActive(true);
        SniperCross.SetActive(false);
        CameraController.Instance.speedH = 2f;
        CameraController.Instance.speedV = 2f;
    }
    public void MinigunActive()
    {
        miniTime = 10;
        miniUse = 7;
        Quaternion target = Quaternion.Euler(0, 70, 0);
        MainCamera.GetComponent<MinigunComponent>().enabled = true;
        CanvasManager.Instance.MainCamera.transform.position = new Vector3(-6.8f, 3.5f, 0.5f);
        CameraController.Instance.pitchR = 110;
        CameraController.Instance.pitchL = 20;
        CameraController.Instance.yawL = -15;
        MainCamera.GetComponent<Camera>().DOFieldOfView(65, 0.5f);
        ButtonGroup.SetActive(false);
        MinigunCross.SetActive(true);
        CameraController.Instance.speedH = 3.5f;
        CameraController.Instance.speedV = 3.5f;
        Timer_TXT.SetActive(true);
        StartCoroutine(MiniTimer());
    }
    public IEnumerator MiniTimer()
    {
        while (miniTime > 0)
        {
            yield return new WaitForSeconds(1);
            miniTime--;
        }
        if (miniTime == 0)
        {
            MinigunDeactive();
        }
    }
    public void MinigunDeactive()
    {
        MainCamera.GetComponent<MinigunComponent>().enabled = false;
        MainCamera.GetComponent<Camera>().DOFieldOfView(27, 0.5f);
        CanvasManager.Instance.MainCamera.transform.position = CameraController.Instance.spawnPos;
        CameraController.Instance.pitchR = -25;
        CameraController.Instance.pitchL = -45;
        CameraController.Instance.yawL = 20;
        ButtonGroup.SetActive(true);
        MinigunCross.SetActive(false);
        Timer_TXT.SetActive(false);
        CameraController.Instance.speedH = 2f;
        CameraController.Instance.speedV = 2f;
    }
    public void Update()
    {
        minigunSecond_TXT.text = miniTime.ToString();
        miniUse_TXT.text = miniUse.ToString();
        if (miniUse == 0)
        {
            Minigun_BTN.GetComponent<Button>().interactable = true;
        }
        else
        {
            Minigun_BTN.GetComponent<Button>().interactable = false;
        }
        if (miniUse < 0)
        {
            miniUse = 0;
        }

        if (HealthScript.Instance.health_Value <= 0)
        {
            ButtonGroup.SetActive(false);
            HealthScript.Instance.health_Value--;
            explosive++;
            for (int i = 0; i < EnemySpawner.Instance.Active_Enemies.Count; i++)
            {
                EnemySpawner.Instance.Spawn = false;
                EnemySpawner.Instance.Active_Enemies[i].GetComponent<Animator>().SetBool("Win", true);
                Destroy(EnemySpawner.Instance.Active_Enemies[i].GetComponent<EnemyComponent>());
                Destroy(EnemySpawner.Instance.Active_Enemies[i].GetComponent<NavMeshAgent>());
                Destroy(EnemySpawner.Instance.Active_Enemies[i].GetComponent<CapsuleCollider>());
            }
            if (explosive ==1)
            {
                explosive++;
                Explos();
            }
        }


    }
    public void Explos()
    {
        Debug.Log("Patla");
        Instantiate(Explosion);
    }

}
