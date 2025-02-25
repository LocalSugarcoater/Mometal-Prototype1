using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    List<GameObject> Sensedgameobjects = new List<GameObject>();
    [SerializeField] GameObject Playerobject;
    PlayerMovement playerscript;
    List<float> EnemyDistance = new List<float>();
    GameObject PrimaryLockOnTarget;
    public GameObject CurrentlyLockedTarget;
    [SerializeField] CinemachineFreeLook playercamera;
    [SerializeField] CinemachineTargetGroup targetGroup;
    [SerializeField] CinemachineVirtualCamera lockoncamera;
    [SerializeField] Camera maincamera;
    public bool cameralockedon = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerscript = Playerobject.GetComponent<PlayerMovement>();
        targetGroup.AddMember(Playerobject.transform, 2f, 0f);
        lockoncamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(targetGroup.transform);
        float directionforhitbox = playerscript.targetangle;
        transform.rotation = Quaternion.Euler(0f, directionforhitbox, 0f);
        
        if (cameralockedon && Vector3.Distance(CurrentlyLockedTarget.transform.position, Playerobject.transform.position) > 10)
        {
           
            ResetCameraAngle();
            
        }

        if (cameralockedon)
        {
            playercamera.m_YAxis.m_InputAxisName = "";
            playercamera.m_XAxis.m_InputAxisName = "";
        }
        
        if (Sensedgameobjects.Count > 0)
        {
            for (int i = 0; i < Sensedgameobjects.Count; i++)
            {
                
                EnemyDistance.Add(Vector3.Distance(Sensedgameobjects[i].transform.position, Playerobject.transform.position));

            }

            for (int i = 0; i < Sensedgameobjects.Count; i++)
            {
                if(Vector3.Distance(Sensedgameobjects[i].transform.position, Playerobject.transform.position) == EnemyDistance.Min())
                {
                    PrimaryLockOnTarget = Sensedgameobjects[i];
                }
            }
            EnemyDistance.Clear();
        }
        else if (Sensedgameobjects.Count == 0 && PrimaryLockOnTarget != null)
        {
            PrimaryLockOnTarget = null;
        }

        if ( (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("LockOn")) && PrimaryLockOnTarget != null && !cameralockedon)
        {
            SetCameraAngle(PrimaryLockOnTarget);
        }
        else if((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("LockOn")) && cameralockedon)
        {
            ResetCameraAngle();
        }

       
        if (cameralockedon && Input.GetAxisRaw("Horizontal Camera") <= -0.1f && Sensedgameobjects.Count > 1)
        {
                for(int i = 0; i < Sensedgameobjects.Count; i++)
                {
                    if (Sensedgameobjects[i] != CurrentlyLockedTarget)
                    {
                        if (Sensedgameobjects[i].transform.position.x < CurrentlyLockedTarget.transform.position.x)
                        {
                        CurrentlyLockedTarget = Sensedgameobjects[i];
                        SetCameraAngle(CurrentlyLockedTarget);
                        break;
                        }
                    }
                }
        }
        else if (cameralockedon && Input.GetAxisRaw("Horizontal Camera") >= 0.1f && Sensedgameobjects.Count > 1)
        {
            
            for (int i = 0; i < Sensedgameobjects.Count; i++)
            {
                if (Sensedgameobjects[i] != CurrentlyLockedTarget)
                {
                    if (Sensedgameobjects[i].transform.position.x > CurrentlyLockedTarget.transform.position.x)
                    {
                        Debug.Log("bruh");
                        CurrentlyLockedTarget = Sensedgameobjects[i];
                        SetCameraAngle(CurrentlyLockedTarget);
                        break;
                    }
                }
            }
        }
        


    }

    
    private void OnTriggerEnter(Collider Sensorcollider)
    {
        if (!Sensedgameobjects.Contains(Sensorcollider.gameObject) && Sensorcollider.gameObject.tag == "Enemy")
        {
            Sensedgameobjects.Add(Sensorcollider.gameObject);
        }
    }

    private void OnTriggerStay(Collider Sensorcolliderstay)
    {

    }

    private void OnTriggerExit(Collider Sensorcolliderexit)
    {

        Sensedgameobjects.Remove(Sensorcolliderexit.gameObject);

    }

    public void SetCameraAngle(GameObject LockOnTarget)
    {
        playercamera.enabled = false;
        lockoncamera.enabled = true;
        targetGroup.AddMember(LockOnTarget.transform, 1f, 0f);
        lockoncamera.LookAt = LockOnTarget.transform;
        lockoncamera.Follow = Playerobject.transform;
        //playercamera.Follow = targetGroup.transform;
        CurrentlyLockedTarget = LockOnTarget;
        cameralockedon = true;
    }

    public void ResetCameraAngle()
    {
        lockoncamera.enabled = false;
        playercamera.enabled = true;
        targetGroup.RemoveMember(CurrentlyLockedTarget.transform);
        playercamera.LookAt = Playerobject.transform;
        playercamera.Follow = Playerobject.transform;
        playercamera.m_YAxis.m_InputAxisName = "Vertical Camera";
        playercamera.m_XAxis.m_InputAxisName = "Horizontal Camera";
        CurrentlyLockedTarget = null;
        cameralockedon = false;
    }

    /*
    public void SetCameraAngle(GameObject LockOnTarget)
    {

        targetGroup.AddMember(LockOnTarget.transform, 1f, 0f);
        playercamera.LookAt = targetGroup.transform;
        //playercamera.Follow = targetGroup.transform;
        CurrentlyLockedTarget = LockOnTarget;
        cameralockedon = true;
    }

    public void ResetCameraAngle()
    {
        targetGroup.RemoveMember(CurrentlyLockedTarget.transform);
        playercamera.LookAt = Playerobject.transform;
        playercamera.Follow = Playerobject.transform;
        playercamera.m_YAxis.m_InputAxisName = "Vertical Camera";
        playercamera.m_XAxis.m_InputAxisName = "Horizontal Camera";
        CurrentlyLockedTarget = null;
        cameralockedon = false;
    }
    */
    /*
    public void SetCameraAngle(GameObject LockOnTarget)
    {
        targetGroup.AddMember(LockOnTarget.transform, 1f, 0f);
        playercamera.LookAt = LockOnTarget.transform;
        CurrentlyLockedTarget = LockOnTarget;
        cameralockedon = true;
    }

    public void ResetCameraAngle()
    {
        targetGroup.RemoveMember(CurrentlyLockedTarget.transform);
        playercamera.LookAt = Playerobject.transform;
        CurrentlyLockedTarget = null;
        cameralockedon = false;
    }*/
}
