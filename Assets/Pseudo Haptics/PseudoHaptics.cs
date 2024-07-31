using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoHaptics : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RealHand;
    public GameObject FakeHand;
    [SerializeField] OVRHand MYHand;
    [SerializeField] OVRSkeleton MYSkelton;

    Vector3 pos_FakeHand;
    Vector3 pos_RealHand;
    Vector3 pos_start;


    public GameObject IndexSphere;
    //親指と人差し指が接触したかどうかの判定
    private bool isIndexPinching;
    private float ThumbPinchStrength;

    Vector3 mid_pos;
    Vector3 thumbTipPos;
    Vector3 indexTipPos;
    Quaternion indexTipRotate;


    public float CD;
    Vector3 dis_pos;


    bool flag_start;

    void Start()
    {
        flag_start = false;
    }

    // Update is called once per frame
    void Update()
    {
        pos_RealHand = RealHand.transform.position;
        if (MYHand.IsTracked) // 手が検出されている場合
        {
            indexTipPos = MYSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
            thumbTipPos = MYSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
            indexTipRotate = MYSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.rotation;
            //Index whether Pinching or not
            isIndexPinching = MYHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

            //Degree of contact between thumb and index finger
            ThumbPinchStrength = MYHand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);

            mid_pos = (indexTipPos + thumbTipPos) / 2;

            //Pass Index finger's information to IndexSphere
            IndexSphere.transform.position = mid_pos;
            IndexSphere.transform.rotation = indexTipRotate;

        }
        else
        {
            IndexSphere.transform.position = Vector3.zero;
        }


        if (Input.GetKeyDown("space"))
        {
            flag_start = true;
            pos_start = RealHand.transform.position;
        }
        else if (Input.GetKeyDown("a"))
        {
            flag_start = false;
        }
        if (flag_start)
        {
            dis_pos = pos_RealHand - pos_start;
            pos_FakeHand = pos_start + dis_pos * CD;
            FakeHand.transform.position = pos_FakeHand;
        }
        else
        {
            FakeHand.transform.position = RealHand.transform.position;
        }



       
    }
    void OnTriggerEnter(Collider other)
    {
        pos_start = RealHand.transform.position;
        if(other.gameObject.tag == "1.4")
        {
            CD = 1.4f;
            Debug.Log("1.4 CD="+ CD);
        }
        if (other.gameObject.tag == "1.2")
        {
            CD = 1.2f;
            Debug.Log("1.2 CD="+ CD);

        }
        if (other.gameObject.tag == "1.0")
        {
            CD = 1.0f;
            Debug.Log("1.0 CD="+ CD);

        }
        if (other.gameObject.tag == "0.8")
        {
            CD = 0.8f;
            Debug.Log("0.8 CD="+ CD);

        }
        if (other.gameObject.tag == "0.6")
        {
            CD = 0.6f;
            Debug.Log("0.6 CD="+ CD);

        }
    }
    void OnTriggerStay(Collider other)
    {

        if (ThumbPinchStrength > 0.9)///つかんだ
        {
            other.gameObject.transform.parent = IndexSphere.transform;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.localPosition = Vector3.zero;
            flag_start = true;
            
            


        }
        else///はなした
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.transform.parent = null;
            flag_start = false;

        }
    }

}
   
