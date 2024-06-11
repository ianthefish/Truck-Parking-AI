using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using Unity.MLAgents.Actuators;
using System.Security.Cryptography;

public class mlagent : Agent
{
    [SerializeField]
    [Header("垂直輸入量")]
    private float input_V;
    [SerializeField]
    [Header("水平輸入量")]
    private float input_H;
    [SerializeField] Vector3 now;
    private Rigidbody truck;
    private Rigidbody trailer;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject target2;
    private float distance_r = 3f; 

    [SerializeField]  private Vector3 originalposition;

    private Vector3 originaltargetposition;
    private Vector3 originaltarget1position;
    private Vector3 originaltarget2position;
    private float currentsteerAngle;
    [SerializeField] private float motorForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backWheelCollider1_l; 
    [SerializeField] private WheelCollider backWheelCollider2_l;
    [SerializeField] private WheelCollider backWheelCollider1_r; 
    [SerializeField] private WheelCollider backWheelCollider2_r;

    [SerializeField] private Transform frontLeftWheeltransform;
    [SerializeField] private Transform frontRightWheeltransform;
    [SerializeField] private Transform backWheeltransform1; 
    [SerializeField] private Transform backWheeltransform2;
    [SerializeField]private MeshRenderer ground;
    [SerializeField]private Material successMaterial;
    [SerializeField]private Material failMaterial;
    [SerializeField]private Material partialMaterial;    
    [SerializeField]private Material defaultMaterial;   
    [SerializeField]private Vector3 posi;
    [SerializeField]private int step;
    

    private Vector3  originaltrailposition;
    private int taken;

    [SerializeField] private float angleDiff2;
    [SerializeField] private float angleDiff;
    public override void Initialize(){
        truck = GetComponent<Rigidbody>();
        trailer = trail.GetComponent<Rigidbody>();
        originalposition = transform.localPosition;
        originaltargetposition = target.transform.localPosition;
        originaltrailposition = trail.transform.localPosition;
    }
    
    public override void OnEpisodeBegin(){
        step =0;

        target.transform.localPosition = new Vector3(-39.6f,-17.14f,38.2f);
        target.transform.rotation = Quaternion.Euler(0, 0, 0);
        target2.transform.localPosition = new Vector3(-39.6f,-17.14f,27.9f);
        target2.transform.rotation = Quaternion.Euler(0, 0, 0);

        taken = 1;
        transform.localPosition = originalposition;
        trail.transform.localPosition = transform.localPosition;
        transform.localRotation = Quaternion.Euler(0, 270, 0);
        trail.transform.localRotation = Quaternion.Euler(0, 270, 0);
        truck.velocity = Vector3.zero;
        truck.angularVelocity = Vector3.zero;
        frontLeftWheelCollider.motorTorque = 0;
        frontRightWheelCollider.motorTorque = 0;
        frontLeftWheelCollider.steerAngle =  0;
        frontRightWheelCollider.steerAngle = 0;

        trailer.velocity = Vector3.zero;
        trailer. angularVelocity = Vector3.zero;

 
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(transform.localPosition); 
        sensor.AddObservation(transform.rotation);
        sensor.AddObservation(target.transform.localPosition);
        sensor.AddObservation(truck.velocity);
        sensor.AddObservation(trail.transform.rotation);
        sensor.AddObservation(trailer.velocity);
        angleDiff = calculateAngle(trail.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y);
        sensor.AddObservation(angleDiff);
        angleDiff2 = calculateAngle(transform.rotation.eulerAngles.y, target.transform.rotation.eulerAngles.y);
        sensor.AddObservation(angleDiff2);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        step = step + 1;
        posi=trail.GetComponent<Trailer>().pp;
        var vectorForce = new Vector3();
        var continuousVectorAction = actions.ContinuousActions;
        vectorForce.x = continuousVectorAction[0];
        vectorForce.z = continuousVectorAction[1];
        input_H =  continuousVectorAction[0];
        input_V =  continuousVectorAction[1];

        frontLeftWheelCollider.motorTorque = (vectorForce.z) * motorForce;
        frontRightWheelCollider.motorTorque = (vectorForce.z) * motorForce;

        currentsteerAngle = maxSteerAngle * vectorForce.x;
        frontLeftWheelCollider.steerAngle =  currentsteerAngle;
        frontRightWheelCollider.steerAngle = currentsteerAngle;

        UpdateSingleWheel(frontLeftWheelCollider,frontLeftWheeltransform);
        UpdateSingleWheel(frontRightWheelCollider,frontRightWheeltransform);
        UpdateSingleWhee2(backWheelCollider1_l,backWheelCollider1_r,backWheeltransform1);
        UpdateSingleWhee2(backWheelCollider2_l,backWheelCollider2_r,backWheeltransform2);

        var distance = Vector3.Distance(transform.localPosition, target.transform.localPosition);
        var distance2 = Vector3.Distance(transform.localPosition, target2.transform.localPosition);

        angleDiff = calculateAngle(trail.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y);
        angleDiff2 = calculateAngle(transform.rotation.eulerAngles.y, target.transform.rotation.eulerAngles.y);

        
        if(distance2 <= 1.5f && taken ==1){ 
            AddReward(500.0f);
            taken=0;
            StartCoroutine(SwapGroundMaterial(partialMaterial, 0.5f));
        }


        if(distance <= distance_r){ 
            AddReward(1000.0f);
            StartCoroutine(SwapGroundMaterial(successMaterial, 0.5f));
            EndEpisode();
        }
       
        if( trail.GetComponent<Trailer>().checkfor1 == 1){
            AddReward(-30f);
        }
        AddReward(-distance*0.01f);
        AddReward(-1f/this.MaxStep);
 
        if(transform.localPosition.y < -30 ){
            AddReward(-1000f);
            StartCoroutine(SwapGroundMaterial(failMaterial, 0.5f)); 
            EndEpisode();

        }
        
        if(angleDiff > 110)
        {
            AddReward(-30.0f);
            if(angleDiff > 120){ 
                AddReward(-999.0f);
                StartCoroutine(SwapGroundMaterial(failMaterial, 0.5f)); 
                EndEpisode();
            }
        }

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");

    }
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform){
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
    private void UpdateSingleWhee2(WheelCollider wheelCollider1,WheelCollider wheelCollider2, Transform wheelTransform){
        Vector3 pos1,pos2;
        Quaternion rot1,rot2;
        wheelCollider1.GetWorldPose(out pos1, out rot1);
        wheelCollider2.GetWorldPose(out pos2, out rot2);
        wheelTransform.rotation = rot1;
        wheelTransform.position = (pos1+pos2)/2;
    }
    private IEnumerator SwapGroundMaterial(Material mat, float time){
        ground.material = mat;
        yield return new WaitForSeconds(time);
        ground.material = defaultMaterial;
    }
    
    void OnCollisionEnter(Collision collision){
        AddReward(-30f);

    }

    float calculateAngle(float a, float b)
    {
        a = a % 360;
        b = b % 360;
        var temp = Mathf.Abs(a - b);
        if(temp > 180) temp = 360 - temp;
        return temp;
    }

}
