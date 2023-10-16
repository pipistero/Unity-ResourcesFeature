using System;

namespace PS.ResourcesFeature.Resources
{
    /// <summary>
    /// Class for integer resource.
    /// </summary>
    /// <typeparam name="T">Enum with your resource types.</typeparam>
    public class ResourceInteger<T> : AbstractResource<int, T> where T : Enum
    {
        public ResourceInteger(T type, int defaultAmount = default) : base(type, defaultAmount)
        {
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when amount is negative.</exception>
        public override void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("The added amount must not be negative");

            var oldAmount = _amount;

            _amount += amount;

            OnChanged(oldAmount, _amount);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when amount is negative.</exception>
        public override void Spend(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("The added amount must not be negative");

            var oldAmount = _amount;

            _amount -= amount;

            if (_amount < 0)
                _amount = 0;

            OnChanged(oldAmount, _amount);
        }

        /// <inheritdoc/>
        public override int GetAmount()
        {
            return _amount;
        }

        /// <inheritdoc/>
        public override bool HasAmount(int amount)
        {
            return _amount >= amount;
        }
    }
}