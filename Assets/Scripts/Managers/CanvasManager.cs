using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public GameObject MainCamera;
    public GameObject ButtonGroup;
    public GameObject Slider;
    public GameObject FailPanel;

    public Vector3 MinigunCamera;

    public GameObject SniperCross;
    public GameObject MinigunCross;
    public GameObject RocketCross;

    public GameObject Minigun_BTN;
    public GameObject Rocket_BTN;

    public GameObject Explosion;
    public int miniUse;
    int miniTime;
    public int rocket_Count;
    bool Exp = false;
    int explosive;
    public TextMeshProUGUI minigunSecond_TXT;
    public TextMeshProUGUI miniUse_TXT;  
    public GameObject Timer_TXT;

    public TextMeshProUGUI rocketUse_TXT;
    public GameObject RocketBoost_Value;
    public TextMeshProUGUI RocketBoost_TXT;
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
        miniTime = 13;
        miniUse = 7;
        Quaternion target = Quaternion.Euler(0, 70, 0);
        MainCamera.GetComponent<MinigunComponent>().enabled = true;
        CanvasManager.Instance.MainCamera.transform.DOLocalMove(new Vector3(-6.8f, 3.5f, 0.5f), 3);
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
    public void RocketActive()
    {
        RocketBoost_Value.SetActive(true);
        rocket_Count = 3;
        miniTime = 10;
        miniUse = 7;
        Quaternion target = Quaternion.Euler(0, 70, 0);
        MainCamera.GetComponent<HelicopterComponent>().enabled = true;
 
        CameraController.Instance.pitchR = 60;
        CameraController.Instance.pitchL = -80;
        CameraController.Instance.yawL = 30;
        CameraController.Instance.yawR = 80;
        MainCamera.GetComponent<Camera>().DOFieldOfView(65, 0.5f);
        ButtonGroup.SetActive(false);
        RocketCross.SetActive(true);
        CameraController.Instance.speedH = 1f;
        CameraController.Instance.speedV = 1f;         
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
        CanvasManager.Instance.MainCamera.transform.DOLocalMove(new Vector3(32,23,-32),3);
        CameraController.Instance.pitchR = -25;
        CameraController.Instance.pitchL = -45;
        CameraController.Instance.yawL = 20;
        ButtonGroup.SetActive(true);
        MinigunCross.SetActive(false);
        Timer_TXT.SetActive(false);
        CameraController.Instance.speedH = 2f;
        CameraController.Instance.speedV = 2f;
    }
    public void RocketDeactive()
    {
        CanvasManager.Instance.MainCamera.transform.SetParent(null);
        RocketBoost_Value.SetActive(false);
        MainCamera.GetComponent<HelicopterComponent>().enabled = false;
        MainCamera.GetComponent<Camera>().DOFieldOfView(27, 0.5f);
        CanvasManager.Instance.MainCamera.transform.DOLocalMove(new Vector3(32, 23, -32), 2f);
        CameraController.Instance.pitchR = -25;
        CameraController.Instance.pitchL = -45;
        CameraController.Instance.yawL = 20;
        ButtonGroup.SetActive(true);
        RocketCross.SetActive(false);
        CameraController.Instance.speedH = 2f;
        CameraController.Instance.speedV = 2f;
    }
    public void Update()
    {
        minigunSecond_TXT.text = miniTime.ToString();
        miniUse_TXT.text = miniUse.ToString();
        rocketUse_TXT.text = miniUse.ToString();
        RocketBoost_TXT.text = rocket_Count.ToString();
        if (miniUse == 0)
        {
            Minigun_BTN.GetComponent<Button>().interactable = true;
            Rocket_BTN.GetComponent<Button>().interactable = true;
        }
        else
        {
            Minigun_BTN.GetComponent<Button>().interactable = false;
            Rocket_BTN.GetComponent<Button>().interactable = false;
        }
        if (miniUse < 0)
        {
            miniUse = 0;
        }       
        if (HealthScript.Instance.health_Value <= 0)
        {
            ButtonGroup.SetActive(false);
            HealthScript.Instance.health_Value--;
            FailPanel.SetActive(true);
            Slider.SetActive(false);

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
        Instantiate(Explosion);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
