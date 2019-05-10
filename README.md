AssertAll
======
Run all of your MSTest assert statements and have each failure message reported summarily.

Dependencies
------
* .NETStandard 2.0
* MSTest.TestFramework >= 1.4.0

Usage
------
Use the AssertAllTestMethod attribute in order to use AssertAll methods.  In your test, use AssertAll instead of Assert.

![example](https://raw.githubusercontent.com/GroshfengTheBarbarian/AssertAll/master/images/test_example.png)

After the test runs, each assert statement that fails will be displayed in the Visual Studio test explorer result pane, and each failed AssertAll message is displayed summarily. 

![result](https://raw.githubusercontent.com/GroshfengTheBarbarian/AssertAll/master/images/test_results.png)

AssertAll statements can't be used unless the AssertAllTestMethod attribute is used; using the TestMethod attribute will result in an InvalidOperationException being thrown.

![invalid](https://raw.githubusercontent.com/GroshfengTheBarbarian/AssertAll/master/images/invalid.png)

If you mix Assert and AssertAll statements, then your AssertAll statements will be executed up until one of your Assert statements fails.

![mixing](https://raw.githubusercontent.com/GroshfengTheBarbarian/AssertAll/master/images/mixed_example.png)

If both Assert and AssertAll statements fail, Visual Studio will display two results.

![mixed_results](https://raw.githubusercontent.com/GroshfengTheBarbarian/AssertAll/master/images/mixed_results.png)

Unresolved Issues
------
I can't get ThrowsExceptionAsync to work correctly; the exception is thrown, but it's not being captured correctly. I'm relatively new to delegates, so I'm probably just missing something simple. Help here would be welcome
