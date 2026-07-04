using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private HeroDefinition hero;

    public HeroDefinition Hero => hero;

    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        ApplyHero();
    }

    private void ApplyHero()
    {
        playerManager.Move.SetMovementSettings(hero.Movement);
        playerManager.WeaponHolder.CurrentWeapon.SetWeaponData(hero.Weapon);
    }
}