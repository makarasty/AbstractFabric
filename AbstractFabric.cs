public abstract class Logger
{
	public abstract void Log(string message);
}

public class FormatLogger : Logger
{
	public string? Format { get; set; }

	public override void Log(string message)
	{
		if (message == null)
			throw new ArgumentNullException(nameof(message));

		if (Format == null)
			throw new InvalidOperationException("Format not specified");

		Console.WriteLine(string.Format(Format, message));
	}
}

public class ColorLogger : Logger
{
	public ConsoleColor? Color { get; set; }

	public override void Log(string message)
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

public abstract class LoggerFactory
{
	public abstract Logger CreateLogger();
}

public class FormatLoggerFactory : LoggerFactory
{
	public string? Format { get; set; }

	public override Logger CreateLogger()
	{
		if (Format == null)
			throw new InvalidOperationException("Format not specified");

		return new FormatLogger { Format = Format };
	}
}

public class ColorLoggerFactory : LoggerFactory
{
	public ConsoleColor? Color { get; set; }

	public override Logger CreateLogger()
	{
		if (!Color.HasValue)
			throw new InvalidOperationException("Color not specified");

		return new ColorLogger { Color = Color.Value };
	}
}

// 💮 Why not combine them together?

public class CombinedLogger : Logger
{
	public string? Format { get; set; }
	public ConsoleColor? Color { get; set; }

	public override void Log(string message)
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

public class CombinedLoggerFactory : LoggerFactory
{
	public string? Format { get; set; }
	public ConsoleColor? Color { get; set; }

	public override Logger CreateLogger()
	{
		if (Format == null)
			Format = "{0}";

		if (!Color.HasValue)
			Color = ConsoleColor.White;

		return new CombinedLogger { Format = Format, Color = Color };
	}
}