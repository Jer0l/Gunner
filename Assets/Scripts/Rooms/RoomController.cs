using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private RoomDoor doorA;
    [SerializeField] private RoomDoor doorB;

    private RoomGenerator roomGenerator;
    private bool roomCompleted;

    public void Initialize(RoomGenerator generator)
    {
        roomGenerator = generator;

        doorA.Initialize(this);
        doorB.Initialize(this);

        roomCompleted = false;
    }

    private void Update()
    {
        if (roomCompleted)
            return;

        if (AreAllEnemiesDead())
        {
            CompleteRoom();
        }
    }

    private bool AreAllEnemiesDead()
    {
        if (enemyContainer == null)
            return true;

        for (int i = 0; i < enemyContainer.childCount; i++)
        {
            Transform enemy = enemyContainer.GetChild(i);

            if (enemy.gameObject.activeInHierarchy)
                return false;
        }

        return true;
    }

    private void CompleteRoom()
    {
        roomCompleted = true;

        doorA.Unlock();
        doorB.Unlock();
    }

    public void UseDoor(RoomDoor door, Transform player)
    {
        roomGenerator.GenerateNextRoom(door, player);
    }
}