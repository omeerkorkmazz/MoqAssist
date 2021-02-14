<h1 align="center">
  <br>
  <img src="https://user-images.githubusercontent.com/29013117/107876653-41bf9d00-6ed8-11eb-90b0-770dccce5964.png" width="200"></a>
</h1>

<h4 align="center">A lightweight Mocking Assistant for Moq library in .NET platforms!</h4>


<p align="center">
  <a href="#">
    <img src="https://img.shields.io/badge/dotnet-core5.0-brightgreen">
  </a>
  
  <a href="#">
    <img src="https://img.shields.io/badge/library-Moq-blue">
  </a>
  
  <a href="https://github.com/omeerkorkmazz/MoqAssist/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/Naereen/StrapDown.js.svg">
  </a> 
  
  <a href="https://github.com/omeerkorkmazz/MoqAssist/stargazers/">
    <img src="https://img.shields.io/github/stars/omeerkorkmazz/MoqAssist.svg?style=social&label=Star&maxAge=2592000">
  </a> 
</p>

<p align="center"> 
  <a href="#">
    <img src="https://forthebadge.com/images/badges/made-with-c-sharp.svg">
  </a>
  
  <a href="https://github.com/omeerkorkmazz">
    <img src="https://forthebadge.com/images/badges/powered-by-black-magic.svg">
  </a>  
</p>


<p align="center">
  <a href="#about">About</a> •
  <a href="#builtWith">Built With</a> •
  <a href="#authors">Authors</a> •
  <a href="#license">License</a> 
</p>

## About
**MoqAssist** is a lightweight and simple mocking assistant used by developers writing unit tests in **.NET platforms** with **Moq** library. Basically, the main purpose is to provide developers to write tests more easily and quickly.

What MoqAssist does is that once registering objects (e.g., dependencies or candidate classes for testing), MoqAssist automatically builds the constructors of classes by using its dictionary that includes the mocking objects so that a developer does not have to manage the dependencies.

The use of MoqAssist is straightforward as explained in the following;
* Register the objects wanted to be tested only once via *MoqAssistDictionary*.
* Call MoqAssist defining which class will be tested.
* Ready for testing!
