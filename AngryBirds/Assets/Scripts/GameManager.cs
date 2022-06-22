using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera cam;
    public Ball ball;
    public Trajectory trajectory;
    [SerializeField] private float pushForce = 10f;
    private bool isDragging = false;
    private bool isBall;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 direction;
    private Vector2 force;
    private float distance;

    private void Start()
    {
        cam = Camera.main;
        ball.DeactivateRb();
        isBall = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isBall)
        {
            isDragging = true;
            OnDragStart();
        }
        else if (Input.GetMouseButtonUp(0) && isBall)
        {
            isDragging = false;
            isBall = false;
            OnDragEnd();
        }
        if (isDragging)
        {
            OnDrag();
        }
    } 
    private void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        trajectory.Show();
    } 
    private void OnDragEnd()
    {
        ball.ActivateRb();
        ball.Push(force);
        trajectory.Hide();
    }
    private void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;


        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - ball.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        ball.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        trajectory.UpdateDots(ball.pos, force);
    }
}
