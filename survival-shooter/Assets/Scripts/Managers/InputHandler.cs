using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;

    private PlayerHealth playerHealth;
    private GameManager gameManager;

    Queue<Command> commands = new Queue<Command>();

    private void Awake() 
    {
        playerHealth = playerMovement.GetComponent<PlayerHealth>();

        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if (playerHealth.isDead || gameManager.isPaused)
            return;

        Command moveCommand = InputMovementHandling();

        if (moveCommand != null)
        {
            commands.Enqueue(moveCommand);

            moveCommand.Execute();
        }
    }

    private void Update()
    {
        if (gameManager.isPaused)
            return;

        Command shootCommand = InputShootHandling();

        if (shootCommand != null)
        {
            shootCommand.Execute();
        }
    }

    Command InputMovementHandling()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(playerMovement, 1, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(playerMovement, -1, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            return new MoveCommand(playerMovement, 0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(playerMovement, 0, -1);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            return Undo();
        }
        else
        {
            return new MoveCommand(playerMovement, 0, 0);
        }
    }

    Command Undo()
    {
        if (commands.Count > 0)
        {
            Command undoCommand = commands.Dequeue();

            undoCommand.UnExecute();
        }

        return null;
    }

    Command InputShootHandling()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            return new ShootCommand(playerShooting);
        }
        else
        {
            return null;
        }
    }
}
