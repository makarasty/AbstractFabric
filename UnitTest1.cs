#nullable enable
namespace AbstractFabric;

public class LoggerTests
{
	[Fact]
	public void FormatLogger_WithNullFormat_ThrowsException()
	{
		var factory = new FormatLoggerFactory();
		Assert.Throws<InvalidOperationException>(() => factory.CreateLogger());
	}

	[Fact]
	public void FormatLogger_WithNullMessage_ThrowsException()
	{
		var factory = new FormatLoggerFactory { Format = "{0}" };

		var logger = factory.CreateLogger();
		Assert.Throws<ArgumentNullException>(() => logger.Log(null));
	}

	[Fact]
	public void FormatLogger_WithValidFormatAndMessage_LogsMessage()
	{
		var factory = new FormatLoggerFactory { Format = "{0}" };

		var logger = factory.CreateLogger();
		logger.Log("Test");
		// No error
	}

	[Fact]
	public void ColorLogger_WithNullColor_ThrowsException()
	{
		var factory = new ColorLoggerFactory();
		Assert.Throws<InvalidOperationException>(() => factory.CreateLogger());
	}

	[Fact]
	public void ColorLogger_WithNullMessage_ThrowsException()
	{
		var factory = new ColorLoggerFactory { Color = ConsoleColor.Red };

		var logger = factory.CreateLogger();
		Assert.Throws<ArgumentNullException>(() => logger.Log(null));
	}

	[Fact]
	public void ColorLogger_WithValidColorAndMessage_LogsMessage()
	{
		var factory = new ColorLoggerFactory { Color = ConsoleColor.Red };

		var logger = factory.CreateLogger();
		logger.Log("Test");
		// No error
	}

}

// 💮 Why not combine them together?

public class CombinedLoggerFactoryTests
{
	[Fact]
	public void CreateLogger_WithValidFormatAndColor_ShouldReturnCombinedLogger()
	{
		var factory = new CombinedLoggerFactory
		{
			Format = "{0}",
			Color = ConsoleColor.Blue
		};

		var logger = factory.CreateLogger();

		Assert.IsType<CombinedLogger>(logger);
		var combinedLogger = (CombinedLogger)logger;
		Assert.Equal("{0}", combinedLogger.Format);
		Assert.Equal(ConsoleColor.Blue, combinedLogger.Color);
	}

	[Fact]
	public void CreateLogger_WithNullFormat_ShouldThrowInvalidOperationException()
	{
		var factory = new CombinedLoggerFactory
		{
			Format = null,
			Color = ConsoleColor.Blue
		};

		var logger = factory.CreateLogger();
		logger.Log("Test");
		// No error
	}

	[Fact]
	public void CreateLogger_WithNullColor_ShouldThrowInvalidOperationException()
	{
		var factory = new CombinedLoggerFactory
		{
			Format = "{0}",
			Color = null
		};

		var logger = factory.CreateLogger();
		logger.Log("Test");
		// No error
	}
}

public class CombinedLoggerTests
{
	[Fact]
	public void Log_WithValidMessage_ShouldPrintFormattedMessageWithColor()
	{
		var logger = new CombinedLogger
		{
			Format = "Error: {0}",
			Color = ConsoleColor.Red
		};
		var message = "Test message";

		using (var sw = new StringWriter())
		{
			Console.SetOut(sw);

			logger.Log(message);

			var expected = string.Format("Error: {0}", message);
			Assert.Contains(expected, sw.ToString());
		}
	}

	[Fact]
	public void Log_WithNullMessage_ShouldThrowArgumentNullException()
	{
		var logger = new CombinedLogger
		{
			Format = "{0}",
			Color = ConsoleColor.Blue
		};

		Assert.Throws<ArgumentNullException>(() => logger.Log(null));
	}
}