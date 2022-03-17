// May be integration???
// Arrange
tempSensor.WillReturnTemp = 25;

var stringWriter = new StringWriter();
Console.SetOut(stringWriter);

// Act
uut.Regulate();

// Assert
var output = stringWriter.ToString();
Assert.That(output, Is.EqualTo("Temperature measured was 25\r\n"));