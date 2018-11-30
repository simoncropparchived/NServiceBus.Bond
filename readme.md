![Icon](https://raw.githubusercontent.com/NServiceBusExtensions/NServiceBus.Bond/master/icon.png)

NServiceBus.Bond
===========================

Add support for [NServiceBus](https://docs.particular.net/nservicebus/) message serialization via [Microsoft Bond](https://microsoft.github.io/bond/manual/bond_cs.html)


<!--- StartOpenCollectiveBackers -->

## Community backed

**This is a community backed project. Backing is done via [opencollective.com/nservicebusextensions](https://opencollective.com/nservicebusextensions/).**

**It is expected that any developer that uses any of these libraries [become at a backer](https://opencollective.com/nservicebusextensions#contribute).** This is an honesty system, there is no code that enforces this requirement. However when raising an issue or a pull request, the GitHub users name may be checked against [the list of backers](https://github.com/NServiceBusExtensions/Home/blob/master/backers.md), and that issue/PR may be closed without further examination.


### Backers

Thanks to all the backers! [[Become a backer](https://opencollective.com/nservicebusextensions#contribute)]

<a href="https://opencollective.com/nservicebusextensions#contribute" target="_blank"><img src="https://opencollective.com/nservicebusextensions/tiers/backer.svg"></a>

[<img src="https://opencollective.com/nservicebusextensions/donate/button@2x.png?color=blue" width="200px">](https://opencollective.com/nservicebusextensions#contribute)

<!--- EndOpenCollectiveBackers -->


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