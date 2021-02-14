using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.EventSystems;
public class ShootingScript : MonoBehaviour
{
    private GameController m_gameController;

    public float power = 2f;
    private int m_dots = 15;

    private Vector2 m_startPos;

    private bool m_isShooting, m_isAiming;

    private GameObject m_shootingDots;
    public GameObject ballPrefab;
    public GameObject ballsContainer;

    private List<GameObject> m_projectilesPath;

    private Rigidbody2D m_theRB;

    private void Awake()
    {
        m_gameController = GameObject.Find("GameController").GetComponent<GameController>();
        m_shootingDots = GameObject.Find("Dots");
        m_theRB = ballPrefab.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        m_projectilesPath = m_shootingDots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        HideDots();

        for (int i = 0; i < m_projectilesPath.Count; i++)
        {
            m_projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (m_gameController.shotCount <= 3 && !IsMouseOverUI())
        {
            Aim();

            if (m_isAiming)
            {
                RotateDots();
            }
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void Aim()
    {
        if (Input.GetMouseButton(0))
        {
            if (!m_isAiming)
            {
                m_isAiming = true;
                m_startPos = Input.mousePosition;
                m_gameController.CheckShotCount();
            }
            else
            {
                PathCalculation();
            }
        }
        else if (m_isAiming && !m_isShooting)
        {
            m_isAiming = false;

            StartCoroutine(ShootingCoroutine());

            HideDots();

            if (m_gameController.shotCount == 1)
            {
                Camera.main.GetComponent<CameraTransitions>().RotateCameraToSide();
            }

        }
    }

    private Vector2 ShootForce(Vector3 force)
    {
        return (new Vector2(m_startPos.x, m_startPos.y) - new Vector2(force.x, force.y)) * power;
    }

    private Vector2 DotPath(Vector2 startP, Vector2 startVelocity, float t)
    {
        return startP + startVelocity * t + .5f * Physics2D.gravity * t * t;
    }

    private void PathCalculation()
    {
        if (m_theRB != null)
        {
            Vector2 velocity = ShootForce(Input.mousePosition) * Time.fixedDeltaTime / m_theRB.mass;

            for (int i = 0; i < m_projectilesPath.Count; i++)
            {
                ShowDots();
                float t = i / (float)m_dots;
                Vector3 point = DotPath(transform.position, velocity, t);
                point.z = 1;
                m_projectilesPath[i].transform.position = point;
            }
        }
        else
        {
            m_theRB = ballPrefab.GetComponent<Rigidbody2D>();
        }
    }

    private void HideDots()
    {
        for (int i = 0; i < m_projectilesPath.Count; i++)
        {
            m_projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }

    private void ShowDots()
    {
        for (int i = 0; i < m_projectilesPath.Count; i++)
        {
            m_projectilesPath[i].GetComponent<Renderer>().enabled = true;
        }
    }

    private void RotateDots()
    {
        Vector2 direction = GameObject.Find("dot (1)").transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private IEnumerator ShootingCoroutine()
    {
        for (int i = 0; i < m_gameController.ballsCount; i++)
        {
            yield return new WaitForSeconds(.1f);
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.name = "Ball";
            ball.transform.SetParent(ballsContainer.transform);
            
            m_theRB = ball.GetComponent<Rigidbody2D>();
            m_theRB.AddForce(ShootForce(Input.mousePosition));

            m_gameController.ballsCountText.text = (m_gameController.ballsCount - i - 1).ToString();
        }
        yield return new WaitForSeconds(.5f);

        m_gameController.shotCount++;
        m_gameController.ballsCountText.text = m_gameController.ballsCount.ToString();
    }
}
