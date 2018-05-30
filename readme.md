![Icon](https://raw.githubusercontent.com/NServiceBusExtensions/NServiceBus.Bond/master/icon.png)

NServiceBus.Bond
===========================

Add support for [NServiceBus](https://docs.particular.net/nservicebus/) message serialization via [Microsoft Bond](https://microsoft.github.io/bond/manual/bond_cs.html)


## The nuget package  [![NuGet Status](http://img.shields.io/nuget/v/NServiceBus.Bond.svg?style=flat)](https://www.nuget.org/packages/NServiceBus.Bond/)

https://nuget.org/packages/NServiceBus.Bond/

    PM> Install-Package NServiceBus.Bond


## Usage

```
var config = new EndpointConfiguration("EndpoinName");
config.UseSerialization<BondSerializer>();
```

## Documentation

https://docs.particular.net/nuget/serialization/NServiceBus.Bond

## Icon

<a href="https://thenounproject.com/term/Chemistry/107944/" target="_blank">Chemistry</a> designed by <a href="https://thenounproject.com/Externografico/" target="_blank">Rafa Bosch
</a> from The Noun Project