using System;
using System.Numerics;

namespace PS.ResourcesFeature.Resources
{
    /// <summary>
    /// Class for big integer resource.
    /// </summary>
    /// <typeparam name="T">Enum with your resource types.</typeparam>
    public class ResourceBigInteger<T> : AbstractResource<BigInteger, T> where T : Enum
    {
        public ResourceBigInteger(T type, BigInteger defaultAmount = default) : base(type, defaultAmount)
        {
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is negative.</exception>
        public override void Add(BigInteger amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount),"The add amount must not be negative");

            var oldAmount = _amount;
        
            _amount += amount;
        
            OnChanged(oldAmount, _amount);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is negative.</exception>
        public override void Spend(BigInteger amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount),"The spend amount must not be negative");

            var oldAmount = _amount;

            _amount -= amount;
        
            if (_amount < 0)
                _amount = new BigInteger(0);
        
            OnChanged(oldAmount, _amount);
        }

        /// <inheritdoc/>
        public override BigInteger GetAmount()
        {
            return _amount;
        }

        /// <inheritdoc/>
        public override bool HasAmount(BigInteger amount)
        {
            return _amount >= amount;
        }
    }
}