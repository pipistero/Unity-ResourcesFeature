using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using PS.ResourcesFeature.Resources;

namespace PS.ResourcesFeature.Controller
{
    /// <summary>
    /// Controller for resources.
    /// Contains integer and big integer resources.
    /// </summary>
    /// <typeparam name="T">Enum with your resource types.</typeparam>
    public class ResourcesController<T> where T : Enum
    {
        /// <summary>
        /// Event invokes when the amount of any integer resource updated.
        /// </summary>
        public event Action<T, int, int, object> ResourceIntegerUpdated;
        
        /// <summary>
        /// Event invokes when the amount of any big integer resource updated.
        /// </summary>
        public event Action<T, BigInteger, BigInteger, object> ResourceBigIntegerUpdated;

        private Dictionary<T, ResourceInteger<T>> _resourcesInteger;
        private Dictionary<T, ResourceBigInteger<T>> _resourcesBigInteger;

        /// <summary>
        /// To load resources see methods
        /// <see cref="InitializeResources(System.Collections.Generic.IEnumerable{ResourceInteger{T}})"/>
        /// <see cref="InitializeResources(System.Collections.Generic.IEnumerable{ResourceBigInteger{T}})"/>
        /// </summary>
        public ResourcesController()
        {
            _resourcesInteger = new Dictionary<T, ResourceInteger<T>>();
            _resourcesBigInteger = new Dictionary<T, ResourceBigInteger<T>>();

            InitializeEvents();
        }

        #region Internal methods

        private void InitializeEvents()
        {
            foreach (var resource in _resourcesInteger)
            {
                resource.Value.Updated += delegate(int oldAmount, int newAmount, object sender)
                {
                    ResourceIntegerUpdated?.Invoke(resource.Key, oldAmount, newAmount, sender);
                };
            }

            foreach (var resource in _resourcesBigInteger)
            {
                resource.Value.Updated += delegate(BigInteger oldAmount, BigInteger newAmount, object sender)
                {
                    ResourceBigIntegerUpdated?.Invoke(resource.Key, oldAmount, newAmount, sender);
                };
            }
        }

        private void ValidateIntegerResource(T resourceType)
        {
            if (_resourcesInteger.ContainsKey(resourceType) == false)
                throw new ArgumentException($"There is no resource with type ({resourceType}) in resources");
        }
        
        private void ValidateBigIntegerResource(T resourceType)
        {
            if (_resourcesBigInteger.ContainsKey(resourceType) == false)
                throw new ArgumentException($"There is no resource with type ({resourceType}) in resources");
        }

        #endregion

        #region Initialize resources methods

        /// <summary>
        /// Loads integer resources.
        /// </summary>
        /// <param name="resourcesInteger">Your integer resources.</param>
        public void InitializeResources(IEnumerable<ResourceInteger<T>> resourcesInteger)
        {
            _resourcesInteger = resourcesInteger.ToDictionary(resource => resource.Type);

            InitializeEvents();
        }

        /// <summary>
        /// Loads big integer resources.
        /// </summary>
        /// <param name="resourcesBigInteger">Your big integer resources.</param>
        public void InitializeResources(IEnumerable<ResourceBigInteger<T>> resourcesBigInteger)
        {
            _resourcesBigInteger = resourcesBigInteger.ToDictionary(resource => resource.Type);

            InitializeEvents();
        }

        #endregion

        #region AddAmount methods

        /// <summary>
        /// Adds amount to integer resource.
        /// </summary>
        /// <param name="resourceType">Type of resource from your enum.</param>
        /// <param name="amount">Amount to add. Amount must not be negative.</param>
        /// <param name="sender">An object that adds an amount to a resource.</param>
        /// <exception cref="ArgumentException">Thrown when there is no resource type in resources.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is negative.</exception>
        /// <seealso cref="AddAmount(T,BigInteger,object)"/>
        public void AddAmount(T resourceType, int amount, object sender)
        {
            ValidateIntegerResource(resourceType);

            _resourcesInteger[resourceType].Add(amount, sender);
        }

        /// <summary>
        /// Adds amount to big integer resource.
        /// </summary>
        /// <param name="resourceType">Type of resource from your enum.</param>
        /// <param name="amount">Amount to add. Amount must not be negative.</param>
        /// <param name="sender">An object that adds an amount to a resource.</param>
        /// <exception cref="ArgumentException">Thrown when there is no resource type in resources.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is negative.</exception>
        /// <seealso cref="AddAmount(T,int,object)"/>
        public void AddAmount(T resourceType, BigInteger amount, object sender)
        {
            ValidateBigIntegerResource(resourceType);

            _resourcesBigInteger[resourceType].Add(amount, sender);
        }

        #endregion

        #region SpendAmount methods

        /// <summary>
        /// Spends amount from integer resource.
        /// </summary>
        /// <param name="resourceType">Type of resource from your enum.</param>
        /// <param name="amount">Amount to spend. Must not be negative.</param>
        /// <param name="sender">An object that spends an amount of a resource.</param>
        /// <exception cref="ArgumentException">Thrown when there is no resource type in resources.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is negative.</exception>
        /// <seealso cref="SpendAmount(T,BigInteger,object)"/>
        public void SpendAmount(T resourceType, int amount, object sender)
        {
            ValidateIntegerResource(resourceType);

            _resourcesInteger[resourceType].Spend(amount, sender);
        }

        /// <summary>
        /// Spends amount from big integer resource.
        /// </summary>
        /// <param name="resourceType">Type of resource from your enum.</param>
        /// <param name="amount">Amount to spend. Must not be negative.</param>
        /// <param name="sender">An object that spends an amount of a resource.</param>
        /// <exception cref="ArgumentException">Thrown when there is no resource type in resources.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when amount is negative.</exception>
        /// <seealso cref="SpendAmount(T,int,object)"/>
        public void SpendAmount(T resourceType, BigInteger amount, object sender)
        {
            ValidateBigIntegerResource(resourceType);

            _resourcesBigInteger[resourceType].Spend(amount, sender);
        }

        #endregion

        #region HasAmount methods

        /// <summary>
        /// Checks if there is enough integer resource. 
        /// </summary>
        /// <param name="resourceType">Type of resource from your enum.</param>
        /// <param name="amount">Amount to check.</param>
        /// <returns>Returns true when resource amount greater than amount in params.</returns>
        /// <exception cref="ArgumentException">Thrown when there is no resource with type in resources.</exception>
        public bool HasAmount(T resourceType, int amount)
        {
            ValidateIntegerResource(resourceType);

            return _resourcesInteger[resourceType].HasAmount(amount);
        }

        /// <summary>
        /// Checks if there is enough big integer resource. 
        /// </summary>
        /// <param name="resourceType">Type of resource from your enum.</param>
        /// <param name="amount">Amount to check.</param>
        /// <returns>Returns true when resource amount greater than amount in params.</returns>
        /// <exception cref="ArgumentException">Thrown when there is no resource with type in resources.</exception>
        public bool HasAmount(T resourceType, BigInteger amount)
        {
            ValidateBigIntegerResource(resourceType);

            return _resourcesBigInteger[resourceType].HasAmount(amount);
        }

        #endregion

        #region GetAmount methods

        /// <summary>
        /// Gets amount of integer resource.
        /// </summary>
        /// <param name="resourceType">Type of resource from your enum.</param>
        /// <returns>Integer amount of resource.</returns>
        public int GetIntegerAmount(T resourceType)
        {
            ValidateIntegerResource(resourceType);

            return _resourcesInteger[resourceType].GetAmount();
        }
        
        /// <summary>
        /// Gets amount of big integer resource.
        /// </summary>
        /// <param name="resourceType">Type of resource from your enum.</param>
        /// <returns>BigInteger amount of resource.</returns>
        public BigInteger GetBigIntegerAmount(T resourceType)
        {
            ValidateBigIntegerResource(resourceType);

            return _resourcesBigInteger[resourceType].GetAmount();
        }

        #endregion
    }
}