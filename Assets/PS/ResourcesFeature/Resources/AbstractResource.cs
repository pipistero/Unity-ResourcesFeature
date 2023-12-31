using System;

namespace PS.ResourcesFeature.Resources
{
    /// <summary>
    /// Base class for resources.
    /// </summary>
    /// <typeparam name="T">Type of amount.</typeparam>
    /// <typeparam name="E">Enum with your resource types.</typeparam>
    public abstract class AbstractResource<T, E>
        where E : Enum
    {
        /// <summary>
        /// Event invokes when the amount updated.
        /// </summary>
        public event Action<T, T, object> Updated; 

        /// <summary>
        /// Resource type.
        /// </summary>
        public E Type { get; }

        protected T _amount;

        protected AbstractResource(E type, T defaultAmount = default)
        {
            Type = type;

            _amount = defaultAmount;
        }

        protected void OnChanged(T oldAmount, T newAmount, object sender)
        {
            Updated?.Invoke(oldAmount, newAmount, sender);
        }

        /// <summary>
        /// Adds amount.
        /// </summary>
        /// <param name="amount">Amount to add.</param>
        /// <param name="sender">An object that adds an amount to a resource.</param>
        public abstract void Add(T amount, object sender);
        
        /// <summary>
        /// Spends amount.
        /// </summary>
        /// <param name="amount">Amount to spend.</param>>
        /// <param name="sender">An object that spends an amount of a resource.</param>
        public abstract void Spend(T amount, object sender);
        
        /// <summary>
        /// Getter for resource amount.
        /// </summary>
        /// <returns>Returns amount of resource.</returns>
        public abstract T GetAmount();
        
        /// <summary>
        /// Checks if there is enough resource amount.
        /// </summary>
        /// <param name="amount">Amount to check.</param>
        /// <returns>Returns true when the resource amount is greater than or equal to the param amount.</returns>
        public abstract bool HasAmount(T amount);
    
    }
}