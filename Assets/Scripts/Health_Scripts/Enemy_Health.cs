public class Enemy_Health : Health
{
    public bool ignoreEnemy;
    private void OnEnable()
    {
        if(ignoreEnemy == true) return;
        if (RoomManager.Instance)
        {
            RoomManager.Instance.enemiesObj.Add(gameObject);
        }
    }
    private void OnDisable()
    {
        if(ignoreEnemy == true) return;
        if (RoomManager.Instance)
        {
            RoomManager.Instance.enemiesObj.Remove(gameObject);
            RoomManager.Instance.CheckForEnemies();
        }
    }
}
