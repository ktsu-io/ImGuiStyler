namespace ktsu.io.ImGuiStyler;

using System.Globalization;
using System.Numerics;
using ImGuiNET;
using ktsu.io.Extensions;
using ktsu.io.ScopedAction;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class Color
{
	public const float OptimalTextContrastRatio = 4.5f;

	public static ImColor FromHex(string hex)
	{
		ArgumentNullException.ThrowIfNull(hex, nameof(hex));

		if (hex.StartsWithOrdinal("#"))
		{
			hex = hex[1..];
		}

		if (hex.Length == 6)
		{
			hex += "FF";
		}

		if (hex.Length != 8)
		{
			throw new ArgumentException("Hex color must be in the format #RRGGBB or #RRGGBBAA", nameof(hex));
		}

		byte r = byte.Parse(hex.AsSpan(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
		byte g = byte.Parse(hex.AsSpan(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
		byte b = byte.Parse(hex.AsSpan(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
		byte a = byte.Parse(hex.AsSpan(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

		return FromRGBA(r, g, b, a);
	}

	public static ImColor FromRGB(byte r, byte g, byte b) => new()
	{
		Value = new Vector4(r / 255f, g / 255f, b / 255f, 1f)
	};

	public static ImColor FromRGBA(byte r, byte g, byte b, byte a) => new()
	{
		Value = new Vector4(r / 255f, g / 255f, b / 255f, a / 255f)
	};

	public static ImColor FromRGB(float r, float g, float b) => new()
	{
		Value = new Vector4(r, g, b, 1f)
	};

	public static ImColor FromRGBA(float r, float g, float b, float a) => new()
	{
		Value = new Vector4(r, g, b, a)
	};

	public static ImColor FromVector(Vector3 vector) => new()
	{
		Value = new Vector4(vector.X, vector.Y, vector.Z, 1f)
	};

	public static ImColor FromVector(Vector4 vector) => new()
	{
		Value = vector
	};

	public static ImColor FromHLS(Vector3 vector) => FromHLSA(vector.X, vector.Y, vector.Z, 1);
	public static ImColor FromHLSA(Vector4 vector) => FromHLSA(vector.X, vector.Y, vector.Z, vector.W);
	public static ImColor FromHLSA(float h, float l, float s) => FromHLSA(h, l, s, 1);
	public static ImColor FromHLSA(float h, float l, float s, float a)
	{
		float r, g, b;
		if (s == 0)
		{
			r = g = b = l;
		}
		else
		{
			float q = l < 0.5f ? l * (1f + s) : l + s - (l * s);
			float p = (2f * l) - q;
			r = HueToRGB(p, q, h + (1f / 3f));
			g = HueToRGB(p, q, h);
			b = HueToRGB(p, q, h - (1f / 3f));
		}
		return FromRGBA(r, g, b, a);
	}

	private static float HueToRGB(float p, float q, float t)
	{
		if (t < 0f)
		{
			t += 1f;
		}

		if (t > 1f)
		{
			t -= 1f;
		}

		if (t < 1f / 6f)
		{
			return p + ((q - p) * 6f * t);
		}

		if (t < 1f / 2f)
		{
			return q;
		}

		if (t < 2f / 3f)
		{
			return p + ((q - p) * ((2f / 3f) - t) * 6f);
		}

		return p;
	}

	public static ImColor Red => FromRGB(255, 0, 0);
	public static ImColor Green => FromRGB(0, 255, 0);
	public static ImColor Blue => FromRGB(0, 0, 255);
	public static ImColor Yellow => FromRGB(255, 255, 0);
	public static ImColor Cyan => FromRGB(0, 255, 255);
	public static ImColor Magenta => FromRGB(255, 0, 255);
	public static ImColor White => FromRGB(255, 255, 255);
	public static ImColor Black => FromRGB(0, 0, 0);
	public static ImColor Gray => FromRGB(128, 128, 128);
	public static ImColor LightGray => FromRGB(192, 192, 192);
	public static ImColor DarkGray => FromRGB(64, 64, 64);
	public static ImColor Transparent => FromRGBA(0, 0, 0, 0);
	public static ImColor Orange => FromRGB(255, 165, 0);
	public static ImColor Purple => FromRGB(128, 0, 128);
	public static ImColor Brown => FromRGB(165, 42, 42);
	public static ImColor Pink => FromRGB(255, 192, 203);
	public static ImColor Gold => FromRGB(255, 215, 0);
	public static ImColor Silver => FromRGB(192, 192, 192);
	public static ImColor Bronze => FromRGB(205, 127, 50);
	public static ImColor Teal => FromRGB(0, 128, 128);
	public static ImColor Olive => FromRGB(128, 128, 0);
	public static ImColor Maroon => FromRGB(128, 0, 0);
	public static ImColor Navy => FromRGB(0, 0, 128);
	public static ImColor Lime => FromRGB(0, 255, 0);
	public static ImColor Indigo => FromRGB(75, 0, 130);
	public static ImColor Turquoise => FromRGB(64, 224, 208);
	public static ImColor Violet => FromRGB(238, 130, 238);
	public static ImColor Beige => FromRGB(245, 245, 220);
	public static ImColor Peach => FromRGB(255, 218, 185);
	public static ImColor Mint => FromRGB(189, 252, 201);
	public static ImColor Lavender => FromRGB(230, 230, 250);
	public static ImColor Coral => FromRGB(255, 127, 80);
	public static ImColor Salmon => FromRGB(250, 128, 114);
	public static ImColor Khaki => FromRGB(240, 230, 140);
	public static ImColor Plum => FromRGB(221, 160, 221);
	public static ImColor GoldMetallic => FromRGB(212, 175, 55);
	public static ImColor SilverMetallic => FromRGB(168, 169, 173);
	public static ImColor BronzeMetallic => FromRGB(205, 127, 50);
	public static ImColor CopperMetallic => FromRGB(184, 115, 51);
	public static ImColor GunmetalMetallic => FromRGB(42, 52, 57);
	public static ImColor Amethyst => FromRGB(153, 102, 204);
	public static ImColor Emerald => FromRGB(80, 200, 120);
	public static ImColor Sapphire => FromRGB(15, 82, 186);
	public static ImColor Ruby => FromRGB(224, 17, 95);
	public static ImColor Diamond => FromRGB(185, 242, 255);
	public static ImColor Pearl => FromRGB(234, 224, 200);
	public static ImColor Onyx => FromRGB(53, 56, 57);
	public static ImColor RubyRed => FromRGB(132, 63, 91);
	public static ImColor SapphireBlue => FromRGB(0, 103, 165);
	public static ImColor EmeraldGreen => FromRGB(0, 153, 68);
	public static ImColor AmethystPurple => FromRGB(153, 102, 204);
	public static ImColor CitrineYellow => FromRGB(228, 208, 10);
	public static ImColor TopazOrange => FromRGB(255, 191, 0);
	public static ImColor AquamarineBlue => FromRGB(0, 191, 255);
	public static ImColor PeridotGreen => FromRGB(153, 204, 0);
	public static ImColor RoseQuartzPink => FromRGB(170, 152, 169);
	public static ImColor SerenityBlue => FromRGB(131, 146, 159);
	public static ImColor MarsalaRed => FromRGB(150, 75, 75);
	public static ImColor RadiantOrchidPurple => FromRGB(191, 85, 156);
	public static ImColor TangerineOrange => FromRGB(242, 133, 0);
	public static ImColor ClassicBlue => FromRGB(0, 133, 202);
	public static ImColor GreeneryGreen => FromRGB(136, 176, 75);
	public static ImColor UltraVioletPurple => FromRGB(95, 75, 139);
	public static ImColor LivingCoral => FromRGB(255, 111, 97);

	public static class Palette
	{
		public static ImColor Red => FromHex("#ff4a49");
		public static ImColor Green => FromHex("#49ff4a");
		public static ImColor Blue => FromHex("#49a3ff");

		public static ImColor Cyan => FromHex("#49feff");
		public static ImColor Magenta => FromHex("#ff49fe");
		public static ImColor Yellow => FromHex("#ecff49");

		public static ImColor Orange => FromHex("#ffa549");
		public static ImColor Pink => FromHex("#ff49a3");
		public static ImColor Lime => FromHex("#a3ff49");
		public static ImColor Purple => FromHex("#c949ff");
	}

	public class ScopedColor : ScopedAction
	{
		public ScopedColor(ImGuiCol target, ImColor color) : base(
			onOpen: () => ImGui.PushStyleColor(target, color.Value),
			onClose: ImGui.PopStyleColor)
		{
		}

		public ScopedColor(ImColor color)
		{
			ImGui.PushStyleColor(ImGuiCol.Button, color.Value);
			OnClose = ImGui.PopStyleColor;
		}
	}

	public static ImColor DesaturateBy(this ImColor color, float amount)
	{
		var hlsa = color.ToHLSA();
		hlsa.Z = Math.Clamp(hlsa.Z - amount, 0, 1);
		return FromHLSA(hlsa);
	}

	public static ImColor SaturateBy(this ImColor color, float amount)
	{
		var hlsa = color.ToHLSA();
		hlsa.Z = Math.Clamp(hlsa.Z + amount, 0, 1);
		return FromHLSA(hlsa);
	}

	public static ImColor WithSaturation(this ImColor color, float amount)
	{
		var hlsa = color.ToHLSA();
		hlsa.Z = Math.Clamp(amount, 0, 1);
		return FromHLSA(hlsa);
	}

	public static ImColor LightenBy(this ImColor color, float amount)
	{
		var hlsa = color.ToHLSA();
		hlsa.Y = Math.Clamp(hlsa.Y + amount, 0, 1);
		return FromHLSA(hlsa);
	}

	public static ImColor DarkenBy(this ImColor color, float amount)
	{
		var hlsa = color.ToHLSA();
		hlsa.Y = Math.Clamp(hlsa.Y - amount, 0, 1);
		return FromHLSA(hlsa);
	}

	public static ImColor WithLuminance(this ImColor color, float amount)
	{
		var hlsa = color.ToHLSA();
		hlsa.Y = Math.Clamp(amount, 0, 1);
		return FromHLSA(hlsa);
	}

	public static ImColor WithAlpha(this ImColor color, float amount)
	{
		var hlsa = color.ToHLSA();
		hlsa.W = Math.Clamp(amount, 0, 1);
		return FromHLSA(hlsa);
	}

	public static ImColor ToGrayscale(this ImColor color) => color.WithSaturation(0);

	public static Vector4 ToHLSA(this ImColor color)
	{
		float r = color.Value.X;
		float g = color.Value.Y;
		float b = color.Value.Z;
		float a = color.Value.W;
		float max = Math.Max(r, Math.Max(g, b));
		float min = Math.Min(r, Math.Min(g, b));
		float h = 0;
		float s;
		float l = (max + min) / 2f;
		float d = max - min;

		if (max == min)
		{
			h = s = 0f;
		}
		else
		{
			s = l > 0.5f ? d / (2f - max - min) : d / (max + min);
			if (max == r)
			{
				h = ((g - b) / d) + (g < b ? 6f : 0f);
			}
			else if (max == g)
			{
				h = ((b - r) / d) + 2f;
			}
			else if (max == b)
			{
				h = ((r - g) / d) + 4f;
			}
			h /= 6f;
		}

		return new Vector4(h, l, s, a);
	}

	public static float GetContrastRatioWith(this ImColor color, ImColor other)
	{
		float l1 = color.ToHLSA().Y;
		float l2 = other.ToHLSA().Y;
		return (Math.Max(l1, l2) + 0.05f) / (Math.Min(l1, l2) + 0.05f);
	}

	public static ImColor CalculateOptimalTextColorForContrast(this ImColor color)
	{
		float l = color.ToHLSA().Y;
		float l1 = ((l + 0.05f) / OptimalTextContrastRatio) - 0.05f;
		float l2 = ((l - 0.05f) * OptimalTextContrastRatio) + 0.05f;
		var hlsa = color.ToHLSA();
		return Math.Abs(l1 - l) < Math.Abs(l2 - l) ? FromHLSA(hlsa.X, l1, hlsa.Z, hlsa.W) : FromHLSA(hlsa.X, l2, hlsa.Z, hlsa.W);
	}
}
