using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PseudoHaptics : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RealHand;
    public GameObject FakeHand;
    [SerializeField] OVRHand MYHand;
    [SerializeField] OVRSkeleton MYSkelton;

    public PseudoHaptics PseudoHaptics_other;

    public GameObject FixedBox;
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

    public int grab_count;


    bool flag_start;
    public bool flag_reset;
    void Start()
    {
        flag_start = false;
        flag_reset = false;
        CD = 1.0f;
        grab_count = 0;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CD = 1.0f;
            flag_reset = true;



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
        if(grab_count == 0)
        {
            if (other.gameObject.tag == "1.4")
            {
                CD = 1.4f;

            }
            if (other.gameObject.tag == "1.2")
            {
                CD = 1.2f;


            }
            if (other.gameObject.tag == "1.0")
            {
                CD = 1.0f;


            }
            if (other.gameObject.tag == "0.8")
            {
                CD = 0.8f;


            }
            if (other.gameObject.tag == "0.6")
            {
                CD = 0.6f;


            }
            if (other.gameObject.tag == "reset")
            {
                CD = 1.0f;
                flag_reset = true;
            }

            if (other.gameObject.tag == "UP")
            {
                if (CD < 1.4f)
                {
                    CD = CD + 0.1f;
                }

                CD = CD * 10;
                CD = Mathf.Floor(CD) / 10;
            }

            if (other.gameObject.tag == "DOWN")
            {
                if (CD > 0.6f)
                {
                    CD = CD - 0.1f;
                }

                CD = CD * 10;
                CD = Mathf.Ceil(CD) / 10;

            }

        }
        else
        {
            return;
        }



    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "not")
        {
            return;
        }
        else if (other.gameObject.tag == "UP")
        {
            return;
        }
        else if (other.gameObject.tag == "DOWN")
        {
            return;
        }
        else if (other.gameObject.tag == "reset")
        {
            flag_reset = true;
        }
        else
        {
            if (ThumbPinchStrength > 0.7)///つかんだ
            {
                other.gameObject.transform.parent = IndexSphere.transform;
                other.GetComponent<Rigidbody>().isKinematic = true;
                Rigidbody other_Rig = other.GetComponent<Rigidbody>();
                var rb = FixedBox.GetComponent<Rigidbody>();
                other_Rig.constraints = RigidbodyConstraints.FreezePositionX;
                //other_Rig.constraints = RigidbodyConstraints.FreezePositionY;
                other_Rig.constraints = RigidbodyConstraints.FreezePositionZ;

                other_Rig.constraints = RigidbodyConstraints.FreezeRotation;

                other.gameObject.transform.localPosition = Vector3.zero;
                flag_start = true;
                grab_count = 1;




            }
            else///はなした
            {
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.transform.parent = null;

                flag_start = false;
                grab_count = 0;

            }
        }
       
    }

}
   
