using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도
    public bool isMove;
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    
    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        isMove = false;
        moveSpeed = GameManager.instance.Speed;
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        if (isMove)
        {
            Move();
            Rotate();
            moveSpeed = GameManager.instance.Speed;
        }
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        // 상대적으로 이동할 거리 계산
        //float moveDistance =
        //    playerInput.v * moveSpeed * Time.deltaTime;
        //// 리지드바디를 통해 게임 오브젝트 위치 변경
        ////playerRigidbody.MovePosition(moveDistance,0,0,);
        //float moveDistance2 =
        //    playerInput.h * moveSpeed * Time.deltaTime;
        //transform.position += new Vector3(moveDistance2,0, moveDistance);

        Vector3 moveDistance = new Vector3(playerInput.h, 0, playerInput.v).normalized;

        transform.position += moveDistance * GameManager.instance.Speed * Time.deltaTime;

    }

    // 입력값에 따라 캐릭터를 좌우로 회전

	private void Rotate()
    {
        Vector3 moveDistance = new Vector3(playerInput.h, 0f, playerInput.v);
       // transform.transform.LookAt(transform.position + moveDistance);
        Quaternion targetRotation = Quaternion.LookRotation(moveDistance, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

    }
}