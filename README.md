# Unity Resources (Currencies) feature

## Important
By now I'm working on Zenject integration for this project. This will be added soon :) 

## Description
Ready-to-use solution for work with resources. 
Supports integer and BigInteger resources.

## Features
- Use your enum for resources
- Add, spend resources
- Subscribe to resource update event
- Get amount of resource

## Tutorial
1. Download latest unity package from packages

2. Create some enum for your resources
```C#
public enum ResourceType
{
    Wood,
    Stone,
    Scroll
}
```

3. Create object ResourcesController
```C#
_resourcesController = new ResourcesController<ResourceType>();
```

4. Initialize resources
```C#
//Create dummy resources; You can load resources data and use it

//Integer resources
ResourceInteger<ResourceType>[] integerResources = {
    new ResourceInteger<ResourceType>(ResourceType.Wood, 100),
    new ResourceInteger<ResourceType>(ResourceType.Stone, 100),
    new ResourceInteger<ResourceType>(ResourceType.Scroll, 100)
};

//BigInteger resources
ResourceBigInteger<ResourceType>[] bigIntegerResources = {
    new ResourceBigInteger<ResourceType>(ResourceType.Wood, 100),
    new ResourceBigInteger<ResourceType>(ResourceType.Stone, 100),
    new ResourceBigInteger<ResourceType>(ResourceType.Scroll, 100)
};

//Initialize integer resources
_resourcesController.InitializeResources(integerResources);

//Initialize big integer resources
_resourcesController.InitializeResources(bigIntegerResources);
```

5. Subscribe to resource update event
```C#
//Subscribe
private void OnEnable()
{
    _resourcesController.ResourceIntegerUpdated += OnResourceIntegerUpdated;
    _resourcesController.ResourceBigIntegerUpdated += OnResourceBigIntegerUpdated;
}

//Unsubscribe
private void OnDisable()
{
    _resourcesController.ResourceIntegerUpdated -= OnResourceIntegerUpdated;
    _resourcesController.ResourceBigIntegerUpdated -= OnResourceBigIntegerUpdated;
}

//Integer resource example
private void OnResourceIntegerUpdated(ResourceType resourceType, int oldAmount, int newAmount, object sender)
{
    Debug.Log($"Resource {resourceType} changed; Old amount = {oldAmount}, new amount = {newAmount}; Sender - {sender}");
}

//BigInteger resource example
private void OnResourceBigIntegerUpdated(ResourceType resourceType, BigInteger oldAmount, BigInteger newAmount, object sender)
{
    Debug.Log($"Resource {resourceType} changed; Old amount = {oldAmount}, new amount = {newAmount}; Sender - {sender}");
}
```

6. Add resource
```C#
//Add amount to integer resource
_resourcesController.AddAmount(ResourceType.Stone, 100, this);

//Add amount to BigInteger resource
_resourcesController.AddAmount(ResourceType.Stone, new BigInteger(100), this);
```

7. Spend resource
```C#
//Spend integer resource
_resourcesController.SpendAmount(ResourceType.Stone, 100, this);

//Spend BigInteger resource
_resourcesController.SpendAmount(ResourceType.Stone, new BigInteger(100), this);
```

8. Get resource amount
```C#
//Get integer resource amount
int integerResourceAmount = _resourcesController.GetIntegerAmount(ResourceType.Stone);

//Get BigInteger resource amount
BigInteger bigIntegerResourceAmount = _resourcesController.GetBigIntegerAmount(ResourceType.Stone);
```
