namespace ktsu.io.ImGuiStyler;

using ImGuiNET;
using ktsu.io.ScopedAction;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class Theme
{
	private static float NormalLuminanceMult { get; set; } = 0.4f;
	private static float NormalSaturationMult { get; set; } = 0.5f;
	private static float ActiveLuminanceMult { get; set; } = .6f;
	private static float ActiveSaturationMult { get; set; } = .7f;
	private static float HoverLuminanceMult { get; set; } = .7f;
	private static float HoverSaturationMult { get; set; } = .8f;
	private static float DragLuminanceMult { get; set; } = 1.1f;
	private static float BackgroundLuminanceMult { get; set; } = .13f;
	private static float BackgroundSaturationMult { get; set; } = .05f;
	private static float DisabledSaturationMult { get; set; } = .1f;

	public static ImColor GetStateColor(ImColor baseColor, bool enabled) => enabled ? baseColor : baseColor.MultiplySaturation(DisabledSaturationMult);
	public static ImColor GetNormalColor(ImColor stateColor) => stateColor.MultiplyLuminance(NormalLuminanceMult).MultiplySaturation(NormalSaturationMult);
	public static ImColor GetActiveColor(ImColor stateColor) => stateColor.MultiplyLuminance(ActiveLuminanceMult).MultiplySaturation(ActiveSaturationMult);
	public static ImColor GetHoveredColor(ImColor stateColor) => stateColor.MultiplyLuminance(HoverLuminanceMult).MultiplySaturation(HoverSaturationMult);
	public static ImColor GetDragColor(ImColor stateColor) => stateColor.MultiplyLuminance(DragLuminanceMult);
	public static ImColor GetBackgroundColor(ImColor stateColor) => stateColor.MultiplyLuminance(BackgroundLuminanceMult).MultiplySaturation(BackgroundSaturationMult);
	public static ImColor GetTextColor(ImColor backgroundColor) => backgroundColor.CalculateOptimalContrastingColor();

	public static class Palette
	{
		public static ImColor Red { get; set; } = ImGuiStyler.Color.FromHex("#ff4a49");
		public static ImColor Green { get; set; } = ImGuiStyler.Color.FromHex("#49ff4a");
		public static ImColor Blue { get; set; } = ImGuiStyler.Color.FromHex("#49a3ff");

		public static ImColor Cyan { get; set; } = ImGuiStyler.Color.FromHex("#49feff");
		public static ImColor Magenta { get; set; } = ImGuiStyler.Color.FromHex("#ff49fe");
		public static ImColor Yellow { get; set; } = ImGuiStyler.Color.FromHex("#ecff49");

		public static ImColor Orange { get; set; } = ImGuiStyler.Color.FromHex("#ffa549");
		public static ImColor Pink { get; set; } = ImGuiStyler.Color.FromHex("#ff49a3");
		public static ImColor Lime { get; set; } = ImGuiStyler.Color.FromHex("#a3ff49");
		public static ImColor Purple { get; set; } = ImGuiStyler.Color.FromHex("#c949ff");

		public static ImColor White { get; set; } = ImGuiStyler.Color.FromHex("#ffffff");
		public static ImColor Black { get; set; } = ImGuiStyler.Color.FromHex("#000000");
		public static ImColor Gray { get; set; } = ImGuiStyler.Color.FromHex("#808080");
		public static ImColor LightGray { get; set; } = ImGuiStyler.Color.FromHex("#c0c0c0");
		public static ImColor DarkGray { get; set; } = ImGuiStyler.Color.FromHex("#404040");
		public static ImColor Transparent { get; set; } = ImGuiStyler.Color.FromHex("#00000000");

		public static ImColor Normal { get; set; } = Blue;
		public static ImColor Emphasis { get; set; } = Orange;
		public static ImColor Error { get; set; } = Red;
		public static ImColor Warning { get; set; } = Yellow;
		public static ImColor Info { get; set; } = Cyan;
		public static ImColor Success { get; set; } = Green;
	}

	public static void Apply(ImColor baseColor)
	{
		var normalColor = GetNormalColor(baseColor);
		var hoveredColor = GetHoveredColor(baseColor);
		var activeColor = GetActiveColor(baseColor);
		var backgroundColor = GetBackgroundColor(baseColor);
		var dragColor = GetDragColor(baseColor);
		var textColor = normalColor.CalculateOptimalContrastingColor();
		var borderColor = backgroundColor.CalculateOptimalContrastingColor();

		var colors = ImGui.GetStyle().Colors;
		colors[(int)ImGuiCol.Text] = textColor.Value;
		colors[(int)ImGuiCol.TextSelectedBg] = baseColor.Value;
		colors[(int)ImGuiCol.TextDisabled] = textColor.Value;
		colors[(int)ImGuiCol.Button] = normalColor.Value;
		colors[(int)ImGuiCol.ButtonActive] = activeColor.Value;
		colors[(int)ImGuiCol.ButtonHovered] = hoveredColor.Value;
		colors[(int)ImGuiCol.CheckMark] = textColor.Value;
		colors[(int)ImGuiCol.Header] = normalColor.Value;
		colors[(int)ImGuiCol.HeaderActive] = activeColor.Value;
		colors[(int)ImGuiCol.HeaderHovered] = hoveredColor.Value;
		colors[(int)ImGuiCol.SliderGrab] = dragColor.Value;
		colors[(int)ImGuiCol.SliderGrabActive] = baseColor.Value;
		colors[(int)ImGuiCol.Tab] = normalColor.Value;
		colors[(int)ImGuiCol.TabActive] = activeColor.Value;
		colors[(int)ImGuiCol.TabHovered] = hoveredColor.Value;
		colors[(int)ImGuiCol.TitleBg] = normalColor.Value;
		colors[(int)ImGuiCol.TitleBgActive] = activeColor.Value;
		colors[(int)ImGuiCol.TitleBgCollapsed] = normalColor.Value;
		colors[(int)ImGuiCol.Border] = borderColor.Value;
		colors[(int)ImGuiCol.FrameBg] = normalColor.Value;
		colors[(int)ImGuiCol.FrameBgActive] = activeColor.Value;
		colors[(int)ImGuiCol.FrameBgHovered] = hoveredColor.Value;
		colors[(int)ImGuiCol.NavHighlight] = normalColor.Value;
		colors[(int)ImGuiCol.ResizeGrip] = normalColor.Value;
		colors[(int)ImGuiCol.ResizeGripActive] = activeColor.Value;
		colors[(int)ImGuiCol.ResizeGripHovered] = hoveredColor.Value;
		colors[(int)ImGuiCol.PlotLines] = normalColor.Value;
		colors[(int)ImGuiCol.PlotLinesHovered] = hoveredColor.Value;
		colors[(int)ImGuiCol.PlotHistogram] = normalColor.Value;
		colors[(int)ImGuiCol.PlotHistogramHovered] = hoveredColor.Value;
		colors[(int)ImGuiCol.ScrollbarGrab] = normalColor.Value;
		colors[(int)ImGuiCol.ScrollbarGrabActive] = activeColor.Value;
		colors[(int)ImGuiCol.ScrollbarGrabHovered] = hoveredColor.Value;
		colors[(int)ImGuiCol.WindowBg] = backgroundColor.Value;
		colors[(int)ImGuiCol.ChildBg] = backgroundColor.Value;
		colors[(int)ImGuiCol.PopupBg] = backgroundColor.Value;
	}

	public class ScopedThemeColor : ScopedAction
	{
		public ScopedThemeColor(ImColor baseColor, bool enabled)
		{
			var stateColor = GetStateColor(baseColor, enabled);
			var normalColor = GetNormalColor(stateColor);
			var hoveredColor = GetHoveredColor(stateColor);
			var activeColor = GetActiveColor(stateColor);
			var backgroundColor = GetBackgroundColor(stateColor);
			var textColor = normalColor.CalculateOptimalContrastingColor();

			int numStyles = 0;
			PushStyleAndCount(ImGuiCol.Text, textColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.TextSelectedBg, stateColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.TextDisabled, textColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.Button, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.ButtonActive, activeColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.ButtonHovered, hoveredColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.CheckMark, textColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.Header, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.HeaderActive, activeColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.HeaderHovered, hoveredColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.SliderGrab, stateColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.SliderGrabActive, activeColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.Tab, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.TabActive, activeColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.TabHovered, hoveredColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.TitleBg, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.TitleBgActive, activeColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.TitleBgCollapsed, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.Border, textColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.FrameBg, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.FrameBgActive, activeColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.FrameBgHovered, hoveredColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.NavHighlight, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.ResizeGrip, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.ResizeGripActive, activeColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.ResizeGripHovered, hoveredColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.PlotLines, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.PlotLinesHovered, hoveredColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.PlotHistogram, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.PlotHistogramHovered, hoveredColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.ScrollbarGrab, normalColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.ScrollbarGrabActive, activeColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.ScrollbarGrabHovered, hoveredColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.WindowBg, backgroundColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.ChildBg, backgroundColor, ref numStyles);
			PushStyleAndCount(ImGuiCol.PopupBg, backgroundColor, ref numStyles);

			OnClose = () => ImGui.PopStyleColor(numStyles);
		}

		private static void PushStyleAndCount(ImGuiCol style, ImColor color, ref int numStyles)
		{
			ImGui.PushStyleColor(style, color.Value);
			++numStyles;
		}
	}

	public static ScopedThemeColor Color(ImColor color) => new(color, enabled: true);
	public static ScopedThemeColor ColorDisabled(ImColor color) => new(color, enabled: false);
}
