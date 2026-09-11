using CardGameCore.Framework;
using CardGameCore.Framework.Relics;
using Godot;

namespace CardGame;

public partial class RelicUI : Control
{
	public IRelic Relic
	{
		get => _relic;
		set
		{
			_relic = value;
			UpdateTexture();
		}
	}
	private IRelic _relic;

	private TextureRect _textureRect;

	public override void _Ready()
	{
		_textureRect = GetNode<TextureRect>("%TextureRect");
	}

	public static RelicUI InstantiateScene()
	{
		return ResourceLoader.Load<PackedScene>("uid://h2mdovtfd7af").Instantiate<RelicUI>();
	}

	private void UpdateTexture()
	{
		if (Relic == null)
			return;
		
		_textureRect.Texture = ResourceUtil.GetRelicTexture(Relic.Name);
	}
}