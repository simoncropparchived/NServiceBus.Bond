<img src="/src/icon.png" height="25px"> Add support for [NServiceBus](https://docs.particular.net/nservicebus/) message serialization via [Microsoft Bond](https://microsoft.github.io/bond/manual/bond_cs.html)

> Bond is a cross-platform framework for working with schematized data. It supports cross-language serialization/deserialization and powerful generic mechanisms for efficiently manipulating data. Bond is broadly used at Microsoft in high-scale services.


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


## NuGet package

https://nuget.org/packages/NServiceBus.Bond/ [![NuGet Status](https://img.shields.io/nuget/v/NServiceBus.Bond.svg)](https://www.nuget.org/packages/NServiceBus.Bond/)


## Usage

snippet: BondSerialization

include: interface-not-supported


### SerializationDelegates

Customizes the cached delegates that serialize and deserialize message types. This is an optional setting.

The default serialization delegates are as follows.

snippet: BondSerializationDelegates

The serializers are cached as per the [Bond performance guidance](https://microsoft.github.io/bond/manual/bond_cs.html#performance).

snippet: SerializerCache


include: custom-contenttype-key

snippet: BondContentTypeKey



## Icon

[Chemistry](https://thenounproject.com/term/Chemistry/107944/) designed by [Rafa Bosch](https://thenounproject.com/Externografico/) from [The Noun Project](https://thenounproject.com).