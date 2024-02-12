public interface ILogger
{
	void Log(string message);
}

public class FormatLogger : ILogger
{
	public string? Format { get; set; }

	public void Log(string message)
	{
		if (message == null)
			throw new ArgumentNullException(nameof(message));

		if (Format == null)
			throw new InvalidOperationException("Format not specified");

		Console.WriteLine(string.Format(Format, message));
	}
}

public class ColorLogger : ILogger
{
	public ConsoleColor? Color { get; set; }

	public void Log(string message)
	{
		if (message == null)
			throw new ArgumentNullException(nameof(message));

		if (!Color.HasValue)
			throw new InvalidOperationException("Color not specified");

		Console.ForegroundColor = Color.Value;
		Console.WriteLine(message);
		Console.ResetColor();
	}
}

public interface ILoggerFactory
{
	ILogger CreateLogger();
}

public class FormatLoggerFactory : ILoggerFactory
{
	public string? Format { get; set; }

	public ILogger CreateLogger()
	{
		if (Format == null)
			throw new InvalidOperationException("Format not specified");

		return new FormatLogger { Format = Format };
	}
}

public class ColorLoggerFactory : ILoggerFactory
{
	public ConsoleColor? Color { get; set; }

	public ILogger CreateLogger()
	{
		if (!Color.HasValue)
			throw new InvalidOperationException("Color not specified");

		return new ColorLogger { Color = Color.Value };
	}
}

// 💮 Why not combine them together?

public class CombinedLogger : ILogger
{
	public string? Format { get; set; }
	public ConsoleColor? Color { get; set; }

	public void Log(string message)
	{
		if (message == null)
			throw new ArgumentNullException(nameof(message));

		if (Format == null)
			Format = "{0}";

		if (!Color.HasValue)
			Color = ConsoleColor.White;

		Console.ForegroundColor = Color.Value;
		Console.WriteLine(string.Format(Format, message));
		Console.ResetColor();
	}
}

public class CombinedLoggerFactory : ILoggerFactory
{
	public string? Format { get; set; }
	public ConsoleColor? Color { get; set; }

	public ILogger CreateLogger()
	{
		if (Format == null)
			Format = "{0}";

		if (!Color.HasValue)
			Color = ConsoleColor.White;

		return new CombinedLogger { Format = Format, Color = Color };
	}
}