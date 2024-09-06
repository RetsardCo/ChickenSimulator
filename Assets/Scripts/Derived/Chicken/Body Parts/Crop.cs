using UnityEngine;


namespace com.nullproject.project1
{
    /// <summary>
    /// This will determine the eating behaviour of the chicken
    /// How full determined by 'float capacity'
    /// How much is energy consumed determined by 'float consumption'
    /// </summary>
    public class Crop : BodyPart
    {
        [SerializeField] private float _maxCapacity;
        
        [SerializeField] private float _consumption;
        
        #region Properties
        
        public bool IsFull;

        // Random from 0 to 100, chance depends on modulo value
        public bool isRound => Calculator.GetChanceBy(0, _maxCapacity, 3);

        // Random from 0 to 100, chance depends on modulo value
        public bool isMushy;

        // Random from 0 to 100, chance depends on modulo value
        public bool hasSmell;

        #endregion
        
        public override void Upgrade(LifeStage lifeStage)
        {
            _maxCapacity = lifeStage switch
            {
                LifeStage.Hatchling => 5,
                LifeStage.Chick => 20,
                LifeStage.Pullet => 75,
                LifeStage.Hen => 100,
                _ => 0
            };

            _consumption = 0;
            IsFull = true;
        }
        
        /// <summary>
        /// Fill or Consume the capacity of this body part
        /// </summary>
        /// <param name="amount">
        ///     Positive value if Consumption;
        ///     Negative value if Fill;
        /// </param>
        public override void AddConsumption(float amount)
        {
            _consumption = Mathf.Clamp(_consumption += amount, 0, _maxCapacity);
            IsFull = _consumption < _maxCapacity;
        }
        
        public override void Trigger(Season season)
        {
            AddConsumption(season.GetAdditionalConsumption());
        }
    }
}