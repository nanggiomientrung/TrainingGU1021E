using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer currentHealthBar;
    [SerializeField] private SpriteRenderer maxHealthBar;

    private float maximumLife; // max máu
    private float currentLife; // máu hiện tại
    private float maximumWidth; // max thanh máu


    public void InitHealthBar(float maxLife)
    {
        if (maxLife <= 0) maxLife = 100f;

        maximumLife = maxLife;
        currentLife = maxLife;

        maximumWidth = maxHealthBar.size.x;

        currentHealthBar.size = new Vector2(maximumWidth, currentHealthBar.size.y);
    }

    /// <summary>
    /// Gọi khi thay đổi máu của đối tượng
    /// Dương khi hồi máu
    /// Âm khi bị rút máu
    /// </summary>
    /// <param name="changeLifeValue"></param>
    public void ChangeLife(float changeLifeValue)
    {
        currentLife += changeLifeValue;
        currentLife = Mathf.Clamp(currentLife, 0, maximumLife);
        // tương đương: if(currentLife < 0) currentLife = 0 và if(currentLife > maximumLife) currentLife = maximumLife;
        currentHealthBar.size = new Vector2(maximumWidth * currentLife / maximumLife, currentHealthBar.size.y);
    }
}
