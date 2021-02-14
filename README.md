<h1 align="center">
  <br>
  <img src="https://user-images.githubusercontent.com/29013117/107876653-41bf9d00-6ed8-11eb-90b0-770dccce5964.png" width="200"></a>
</h1>

<h4 align="center">A Lightweight Mocking Assistant for Moq Library in .NET Platforms!</h4>


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
    <img src="https://img.shields.io/github/stars/omeerkorkmazz/MoqAssist?style=social">
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
  <a href="#sample">Sample</a> •
  <a href="#technologies">Techonologies</a> •
  <a href="#contribution">Contribution</a> •
  <a href="#authors">Authors</a> •
  <a href="#license">License</a> 
</p>

## About
**MoqAssist** is a lightweight and simple mocking assistant used by developers writing unit tests in **.NET platforms** with **Moq** library. Basically, the main purpose is to provide developers to write tests more easily and quickly without fighting with details.

What MoqAssist does is that once registering objects (e.g., dependencies or candidate classes for testing) into the dictionary managed by **MoqAssistDictionary**, MoqAssist automatically builds the constructors of classes by using its dictionary that includes the mocking objects so that a developer does not have to manage the dependencies.

The use of MoqAssist is straightforward as explained in the following;
* Register the objects wanted to be tested only once via *MoqAssistDictionary*.
* Call MoqAssist defining which class will be tested.
* Ready for testing!

## Sample

| :zap:  In the sample, there are *ProductService*, *CategoryService* and *UserService*, and product service depends others.|
|-----------------------------------------|

###  1- The Use of MoqAssistDictionary

 * Firstly, create a unit test project. It might be any unit test framework of .NET platforms. In the examples of MoqAssist, xUnit was used.
 * Then, create a class inherited from MoqAssistDictionary to register the mocked objects.
 * Override the *RegisterMocks()* method.

```csharp
namespace MoqAssist.UnitTests.Tests.MockDictionary
{
    public class DefaultMockDictionary : MoqAssistDictionary
    {
        public override void RegisterMocks()
        {
            Register<IUserService>(new Mock<IUserService>());
            Register<ICategoryService>(new Mock<ICategoryService>());
        }
    }
}
```

* *DefaultMockDictionary* will be the once produced dictionary with the registrations of the mocked objects.

```csharp
Register<T>(Mock<T> obj);
```
* Register method is used to store the mocked object into the dictionary.

```csharp
bool IsMockExist<T>();
KeyValuePair<string, object> GetMockPair<T>();
```
* In addition, MoqAssistDictionary offers a couple of methods for searching and validation. 
* For the *Key-Value Pairs*, the key represents the full name of the object and the value represents the mocked object.


| :zap:  Now, the system is ready for testing. Create a test file and decide which class needs to be tested!|
|-----------------------------------------|


###  2- The Use of MoqAssist

 * Here, declare the MoqAssist with a given object for testing.
 * Also, declare the original object which operates the real business with mock dependencies.
 
```csharp
private MoqAssist<ProductService> _productService { get; set; }
private IProductService _productServiceInstance { get; set; }
```
 
 * Construct the MoqAssist with a dictionary with registered mock objects.
 
```csharp
_productService = MoqAssist<ProductService>.Construct(new DefaultMockDictionary());
```

 * Get the automatically mocked constructor that needs to tested. The order of constructors are preserved by MoqAssist.
 
 ```csharp
_productServiceInstance = _productService.GetConstructors()[0];
```

 * Get mock objects for any setup operations, if needed.
 
```csharp
_userServiceMock = _productService.GetMock<IUserService>();
_categoryServiceMock = _productService.GetMock<ICategoryService>();
```

* At the end, the code seems as follows;

```csharp
public class ProductServiceTests : IDisposable
{
   private MoqAssist<ProductService> _productService { get; set; }
   private IProductService _productServiceInstance { get; set; }

   private Mock<IUserService> _userServiceMock { get; set; }
   private Mock<ICategoryService> _categoryServiceMock { get; set; } 
   
   public ProductServiceTests()
   {
        _productService = MoqAssist<ProductService>.Construct(new DefaultMockDictionary());
        _productServiceInstance = _productService.GetConstructors()[0];

        _userServiceMock = _productService.GetMock<IUserService>();
        _categoryServiceMock = _productService.GetMock<ICategoryService>();
    }

     public void Dispose()
     {
         _productService = null;
         _productServiceInstance = null;

         _userServiceMock = null;
         _categoryServiceMock = null;
     }     
 }  
```

* In order to write a test, sample scenarios as follows;

```csharp
namespace MoqAssist.UnitTests.Tests
{

    public class ProductServiceTests : IDisposable
    {
        private MoqAssist<ProductService> _productService { get; set; }
        private IProductService _productServiceInstance { get; set; }

        private Mock<IUserService> _userServiceMock { get; set; }
        private Mock<ICategoryService> _categoryServiceMock { get; set; }

        public ProductServiceTests()
        {
            _productService = MoqAssist<ProductService>.Construct(new DefaultMockDictionary());
            _productServiceInstance = _productService.GetConstructors()[0];

            _userServiceMock = _productService.GetMock<IUserService>();
            _categoryServiceMock = _productService.GetMock<ICategoryService>();
        }

        public void Dispose()
        {
            _productService = null;
            _productServiceInstance = null;

            _userServiceMock = null;
            _categoryServiceMock = null;
        }

        [Theory]
        [InlineData("Test Product", 100.50, 25, 1, 3)]
        [InlineData("Test Product 2", 25, 2, 4, 9)]
        public void Create_Should_Return_True_When_Process_Successfull(string productName, decimal price, int stock, int userId, int categoryId)
        {
            _userServiceMock.Setup(x => x.GetById(userId)).Returns(new User() { Id = userId });
            _categoryServiceMock.Setup(x => x.GetById(categoryId)).Returns(new Category() { Id = categoryId });

            var response = _productServiceInstance.Create(productName, price, stock, userId, categoryId);
            Assert.True(response == true);
        }

        [Theory]
        [InlineData("Test Product", 100.50, 25, 1, 3)]
        [InlineData("Test Product 2", 25, 2, 4, 9)]
        public void Create_Should_Return_False_When_Category_Null(string productName, decimal price, int stock, int userId, int categoryId)
        {
            _userServiceMock.Setup(x => x.GetById(userId)).Returns(new User() { Id = userId });
            _categoryServiceMock.Setup(x => x.GetById(categoryId)).Returns<Category>(null);

            var response = _productServiceInstance.Create(productName, price, stock, userId, categoryId);
            Assert.True(response == false);
        }

        [Theory]
        [InlineData("Test Product", 100.50, 25, 1, 3)]
        [InlineData("Test Product 2", 25, 2, 4, 9)]
        public void Create_Should_Return_False_When_User_Null(string productName, decimal price, int stock, int userId, int categoryId)
        {
            _userServiceMock.Setup(x => x.GetById(userId)).Returns<User>(null);
            _categoryServiceMock.Setup(x => x.GetById(categoryId)).Returns(new Category() { Id = categoryId });

            var response = _productServiceInstance.Create(productName, price, stock, userId, categoryId);
            Assert.True(response == false);
        }
    }
}  
```



## Technologies

* [Dotnet Core](https://dotnet.microsoft.com/) - version net5.0 - Used to implement MoqAssist and MoqAssistDictionary
* [Moq](https://www.nuget.org/packages/Moq/) - version 4.16.0 - Used for mocking business logics.
* [xUnit](https://xunit.net/) - version net5.0 - Used for sample tests.


## Contribution
Feel free to contact me for any questions, discussions, or ideas. [Send Email](mailto:omer.korkmaz.95@windowslive.com?subject=[GitHub]%20MoqAssist)

**How to contribute**
* Fork the repository
* Create a feature branch
* Commit the changes and push the branch
* Create a new pull request


## Authors

* [**Omer KORKMAZ**](https://www.linkedin.com/in/omerkorkmazz/) - *[*@omeerkorkmazz*](https://www.github.com/omeerkorkmazz)*


## License

Distributed under the MIT License. See [LICENSE](https://github.com/omeerkorkmazz/MoqAssist/blob/master/LICENSE) for more information.
