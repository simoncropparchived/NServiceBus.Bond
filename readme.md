<img src="https://raw.github.com/NServiceBusExtensions/NServiceBus.Bond/master/src/icon.png" height="25px"> Add support for [NServiceBus](https://docs.particular.net/nservicebus/) message serialization via [Microsoft Bond](https://microsoft.github.io/bond/manual/bond_cs.html)

<!--- StartOpenCollectiveBackers -->

[Already a Patron? skip past this section](#endofbacking)


## Community backed

**It is expected that all developers [become a Patron](https://opencollective.com/nservicebusextensions/order/6976) to use any of these libraries. [Go to licensing FAQ](https://github.com/NServiceBusExtensions/Home/blob/master/readme.md#licensingpatron-faq)**


### Sponsors

Support this project by [becoming a Sponsors](https://opencollective.com/nservicebusextensions/order/6972). The company avatar will show up here with a link to your website. The avatar will also be added to all GitHub repositories under this organization.


### Patrons

Thanks to all the backing developers! Support this project by [becoming a patron](https://opencollective.com/nservicebusextensions/order/6976).

<img src="https://opencollective.com/nservicebusextensions/tiers/patron.svg?width=890&avatarHeight=60&button=false">

<!--- EndOpenCollectiveBackers -->

<a href="#" id="endofbacking"></a>

## The NuGet package [![NuGet Status](http://img.shields.io/nuget/v/NServiceBus.Bond.svg)](https://www.nuget.org/packages/NServiceBus.Bond/)

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