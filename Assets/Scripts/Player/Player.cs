
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        // ========================= Поля.
        private Rigidbody2D rigBody;
        // Движение.
        [Header("Movement")]
        [SerializeField] private float crouchingSpeed = 3f;
        [SerializeField] private float walkingSpeed = 5f;
        [SerializeField] private float runningSpeed = 7f;
        private Vector2 inputVector;
        public Vector2 lastMoveDirection = Vector2.right;
        private bool isRunning;
        private bool isCrouching;

        public PlayerStats stats = new PlayerStats();

        public PlayerItemSystem itemSystem = new PlayerItemSystem();
        // Рывок.
        [Header("Dash")]
        [SerializeField] private float dashSpeed = 10f;
        [SerializeField] private float dashDuration = 0.3f;
        [SerializeField] private float dashCooldown = 1.5f;
        private Vector2 dashDirection;
        private float dashTimer;
        private float dashCooldownTimer;
        private bool isDashing;
        // Подбор.
        public PlayerPickup pickup;
        public HandItemView handView;
        [SerializeField] LayerMask npcMask;
        NPC npc;
        void Start()
        {
            stats.Init();
        }
        void Awake()
        {
            Instance = this;
            rigBody = GetComponent<Rigidbody2D>();

            pickup = GetComponent<PlayerPickup>();
        }
        // Вызывается каждый кадр. Решаю, че происходит.
        void Update()
        {
            inputVector = GameInput.instance.GetMovementVector().normalized;
            if (inputVector != Vector2.zero) lastMoveDirection = inputVector;
            isRunning = GameInput.instance.IsRunning();
            isCrouching = GameInput.instance.IsCrouching();
            if (GameInput.instance.IsDashing() && dashCooldownTimer <= 0f) StartDash();

            itemSystem.Tick(Time.deltaTime);
            Vector2 scroll = GameInput.instance.GetScroll();

            if (scroll.y != 0)
            {
                InventoryManager.Instance.ChangeHotbarSlot(scroll.y);
            }
            stats.RegenerateMana(Time.deltaTime);
            HandleInteract();

            if (GameInput.instance.IsInteract())
            {
                TryInteract();
            }

            if (DialogueSystem.Instance.entered && GameInput.instance.Attack())
                DialogueSystem.Instance.Next();
        }
        void TryInteract()
        {
            Collider2D hit =
    Physics2D.OverlapCircle(transform.position, 2.5f, npcMask);

            if (hit == null)
            {
                Debug.Log("NO HIT");
                return;
            }

            Debug.Log("HIT: " + hit.name);

            NPC npc = hit.GetComponent<NPC>();

            if (npc == null)
            {
                Debug.Log("NO NPC ON HIT OBJECT");
                return;
            }

            npc.Interact();
        }
        // Вызывается по фиксированному таймеру. Физика происходящего.
        void FixedUpdate()
        {
            HandleMovement();
        }

        // ========================= Движение.
        void HandleMovement()
        {
            dashCooldownTimer -= Time.fixedDeltaTime;
            float speed = walkingSpeed;

            if (isDashing)
            {
                dashTimer -= Time.fixedDeltaTime;
                dashDirection = inputVector != Vector2.zero ? inputVector : lastMoveDirection;
                rigBody.MovePosition(rigBody.position + dashDirection * dashSpeed * Time.fixedDeltaTime);

                if (dashTimer <= 0f)
                    isDashing = false;

                return;
            }


            if (isCrouching) speed = crouchingSpeed;
            else if (isRunning) speed = runningSpeed;

            rigBody.MovePosition(rigBody.position + inputVector * (speed * Time.fixedDeltaTime));
        }
        void StartDash()
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;

            dashDirection = inputVector;

            if (dashDirection == Vector2.zero)
                dashDirection = Vector2.right;
        }
        //======
        void HandleInteract()
        {
            if (!GameInput.instance.IsInteract())
                return;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);

            Pickup bestPickup = null;
            Chest bestChest = null;

            float bestPickupDist = float.MaxValue;
            float bestChestDist = float.MaxValue;

            foreach (var h in hits)
            {
                Vector2 pos = h.transform.position;

                Pickup p = h.GetComponentInParent<Pickup>();
                if (p != null)
                {
                    float d = Vector2.Distance(transform.position, pos);

                    if (d < bestPickupDist)
                    {
                        bestPickupDist = d;
                        bestPickup = p;
                    }
                }
                
                Chest c = h.GetComponentInParent<Chest>();
                if (c != null)
                {
                    float d = Vector2.Distance(transform.position, pos);

                    if (d < bestChestDist)
                    {
                        bestChestDist = d;
                        bestChest = c;
                    }
                }
            }
            if (bestPickup != null)
            {
                if (bestPickup.Interact())
                    return;
            }

            if (bestChest != null)
            {
                if (bestChest.Interact())
                    return;
            }
            InventoryManager
                .Instance
                .UseSlot(
                    InventoryManager
                    .Instance
                    .selectedHotbarSlot
                );
        }

    }
}
