using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도

    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 좌우 실행
       // HoirMove();
        // 움직임 실행
        Move();
        // 회전 실행
        Rotate();
        // 입력값에 따라 애니메이터의 Move 파라미터 값을 변경
        playerAnimator.SetFloat("Move", playerInput.v);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        // 상대적으로 이동할 거리 계산
        float moveDistance =
            playerInput.v * moveSpeed * Time.deltaTime;
        // 리지드바디를 통해 게임 오브젝트 위치 변경
        //playerRigidbody.MovePosition(moveDistance,0,0,);
        float moveDistance2 =
            playerInput.h * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveDistance2,0, moveDistance);
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void HoirMove() {

        float moveDistance =
            playerInput.h *  moveSpeed * Time.deltaTime;
		// 리지드바디를 통해 게임 오브젝트 위치 변경
		// playerRigidbody.MovePosition(playerRi
		// gidbody.position + moveDistance);
		transform.Translate(0, 0, moveDistance);
	
    }
	private void Rotate()
    {
        Vector3 moveDistance = new Vector3(playerInput.h, 0f, playerInput.v);
        transform.transform.LookAt(transform.position + moveDistance);

    }
}