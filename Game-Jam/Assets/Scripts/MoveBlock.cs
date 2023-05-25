using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
[SelectionBase]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MoveBlock : MonoBehaviour
{
    public enum MovingPlatformType
    {
        BACK_FORTH,
        LOOP,
        ONCE
    }

    //public Platform_Catcher platformCatcher;
    public float speed = 1.0f;
    public MovingPlatformType platformType;

    public bool startMovingOnlyWhenVisible;
    public bool isMovingAtStart = true;

    [HideInInspector]
    public Vector3[] localNodes = new Vector3[1];

    public Vector3[] inspector_worldNode { get {return worldNode; } }

    protected Vector3[] worldNode;

    protected int m_Current = 0;
    protected int m_Next = 0;
    protected int m_Dir = 1;

    protected float m_WaitTime = -1.0f;

    protected Rigidbody2D m_Rigidbody2D;
    protected Vector2 m_Velocity;

    protected bool m_Started = false;
    protected bool m_VeryFirstStart = false;

    protected SpriteRenderer spriteRenderer;

    public Vector2 Velocity
    {
        get { return m_Velocity; }
    }

    private void Reset()
    {
        localNodes[0] = Vector3.zero;

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        //m_Rigidbody2D.isKinematic = true;

        /*if (platformCatcher == null)
            platformCatcher = GetComponent<Platform_Catcher> ();*/
    }

    private void Start()
    {
        localNodes[0] = transform.position;

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        //m_Rigidbody2D.isKinematic = true;
        m_Rigidbody2D.MovePosition(localNodes[0]);

        /*if (platformCatcher == null)
            platformCatcher = GetComponent<Platform_Catcher>();*/
    
        //Allow to make platform only move when they became visible
        spriteRenderer = GetComponent<SpriteRenderer>();

        //we make point in the path being defined in local space so game designer can move the platform & path together
        //but as the platform will move during gameplay, that would also move the node. So we convert the local nodes
        // (only used at edit time) to world position (only use at runtime)
        worldNode = new Vector3[localNodes.Length];
        worldNode[0] = transform.position;
        for (int i = 1; i < worldNode.Length; ++i)
            worldNode[i] = transform.TransformPoint(localNodes[i]);

        Init();
    }

    protected void Init()
    {
        m_Current = 0;
        m_Dir = 1;
        m_Next = localNodes.Length > 1 ? 1 : 0;

        m_WaitTime = 0;

        m_VeryFirstStart = false;
        if (isMovingAtStart)
        {
            m_Started = !startMovingOnlyWhenVisible;
            m_VeryFirstStart = true;
        }
        else
            m_Started = false;
    }

    private void FixedUpdate()
    {
        if(spriteRenderer.isVisible || startMovingOnlyWhenVisible)
            OnBecameVisible();
        else
            OnBecameInvisible();

        if (!m_Started)
            return;

        //no need to update we have a single node in the path
        if (m_Current == m_Next)
            return;

        if(m_WaitTime > 0)
        {
            m_WaitTime -= Time.deltaTime;
            return;
        }

        float distanceToGo = speed * Time.deltaTime;

        while(distanceToGo > 0)
        {

            Vector2 direction = worldNode[m_Next] - transform.position;

            float dist = distanceToGo;
            if(direction.sqrMagnitude < dist * dist)
            {   //we have to go farther than our current goal point, so we set the distance to the remaining distance
                //then we change the current & next indexes
                dist = direction.magnitude;

                m_Current = m_Next;

                m_WaitTime = 0;

                if (m_Dir > 0)
                {
                    m_Next += 1;
                    if (m_Next >= worldNode.Length)
                    { //we reach the end

                        switch(platformType)
                        {
                            case MovingPlatformType.BACK_FORTH:
                                m_Next = worldNode.Length - 2;
                                m_Dir = -1;
                                break;
                            case MovingPlatformType.LOOP:
                                m_Next = 0;
                                break;
                            case MovingPlatformType.ONCE:
                                m_Next -= 1;
                                StopMoving();
                                break;
                        }
                    }
                }
                else
                {
                    m_Next -= 1;
                    if(m_Next < 0)
                    { //reached the beginning again

                        switch (platformType)
                        {
                            case MovingPlatformType.BACK_FORTH:
                                m_Next = 1;
                                m_Dir = 1;
                                break;
                            case MovingPlatformType.LOOP:
                                m_Next = worldNode.Length - 1;
                                break;
                            case MovingPlatformType.ONCE:
                                m_Next += 1;
                                StopMoving();
                                break;
                        }
                    }
                }
            }

            m_Velocity = direction.normalized * dist;

            transform.position +=  new Vector3(direction.normalized.x * dist, direction.normalized.y * dist, 0f);
            m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + m_Velocity);
            //m_Rigidbody2D.AddForce(m_Velocity, ForceMode2D.Force);
            //platformCatcher.MoveCaughtObjects (m_Velocity);

            //We remove the distance we moved. That way if we didn't had enough distance to the next goal, we will do a new loop to finish
            //the remaining distance we have to cover this frame toward the new goal
            distanceToGo -= dist;

            // we have some wait time set, that mean we reach a point where we have to wait. So no need to continue to move the platform, early exit.
            if (m_WaitTime > 0.001f) 
                break;
        }
    }

    public void StartMoving()
    {
        m_Started = true;
    }

    public void StopMoving()
    {
        m_Started = false;
    }

    public void ResetPlatform()
    {
        transform.position = worldNode[0];
        Init();
    }

    private void OnBecameVisible() {
        // Debug.Log("Objeto está sendo visto!");
        m_Started = true;
        m_VeryFirstStart = false;
    }

    private void OnBecameInvisible() {
        // Debug.Log("Objeto não está sendo visto!");
        m_Started = false;
        m_VeryFirstStart = false;
    }
}