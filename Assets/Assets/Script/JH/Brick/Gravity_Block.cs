using UnityEngine;

public class Gravity_Block : Brick
{
    public float gravitationalForce = 10f; // 중력의 세기
    public float power = 25;
    protected override void Start()
    {
        block_name = "Gravity";
        hp = curHp = 1;
        Bricks.Add(this);
    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.2f);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("ball") || collider.CompareTag("Ninja"))
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // 태양과 물체 간의 벡터 계산
                    Vector3 direction = transform.position - rb.transform.position;
                    float distance = direction.magnitude;

                    // 중력 방향 정규화
                    direction.Normalize();

                    // 중력 적용
                    //float forceMagnitude = gravitationalForce / (distance * distance); // 거리 제곱에 반비례
                    rb.AddForce(direction * power, ForceMode.Acceleration);
                    if (rb.velocity.magnitude > 10)
                        rb.velocity = rb.velocity.normalized * 10;
                }
            }
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ball") || other.collider.CompareTag("Ninja") || other.collider.CompareTag("Fire"))
        {
            other.transform.position = new Vector2(0, -5f);
            Destroy(gameObject);
        }
    }
}
