using CoreFramework;

namespace inGame.AbstractShooter.Models
{
    public class PlayerModel : BaseModel
    {
        public event System.Action<int> OnReduceHP;
        public event System.Action<int> OnIncreaseHP;

        public int CurrentHP = 10;
        public int MaxHP = 10;

        public void ReduceHP()
        {
            CurrentHP--;

            OnReduceHP?.Invoke(CurrentHP);
        }

        public void IncreaseHP()
        {
            CurrentHP++;

            if (CurrentHP > MaxHP)
                CurrentHP = MaxHP;

            OnIncreaseHP?.Invoke(CurrentHP);
        }
    }
}
