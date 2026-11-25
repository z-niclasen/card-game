using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardGameCore.Impl;
using CardGameCore.Impl.Runs;
using Godot;
using Map = CardGameCore.Map;

namespace CardGame;

public delegate void EncounterButtonPressedDelegate(Map.MapPosition position);

public partial class MapUI : MarginContainer
{
	public event EncounterButtonPressedDelegate OnEncounterButtonPressed;
	
	private readonly Map _map = Run.Instance.Map;
	private readonly Dictionary<Map.MapPosition, TextureButton> _positionToButton = new();

	private List<HBoxContainer> _layerContainers = [];

	private VBoxContainer _vbox;
	private MarginContainer _vboxParent;

	public override void _Ready()
	{
		_vbox = GetNode<VBoxContainer>("%VBox");
		_vboxParent = GetNode<MarginContainer>("%MarginContainer");
		_layerContainers = InitializeContainers();
		DrawNodes();
		_ = HighlightValidMoves();
		DrawEdges();
	}

	private async Task HighlightValidMoves()
	{
		List<Map.MapPosition> validMoves = _map.GetValidMoves(Run.Instance.CurrentMapPosition).ToList();

		foreach (Map.MapPosition position in validMoves)
		{
			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
			TextureButton button = _positionToButton[position];
			button.StretchMode = TextureButton.StretchModeEnum.Scale;
			button.Scale = new Vector2(1.5f, 1.5f);
		}
	}

	private List<HBoxContainer> InitializeContainers()
	{
		List<HBoxContainer> containers = [];
		for (int layer = 0; layer < _map.LayerCount; layer++)
		{
			HBoxContainer layerContainer = new();
			layerContainer.AddThemeConstantOverride("separation", 200);
			_vbox.AddChild(layerContainer);
			_vbox.MoveChild(layerContainer, 0);
			
			containers.Add(layerContainer);
		}

		return containers;
	}

	private void DrawNodes()
	{
		IEnumerable<IEnumerable<Map.MapPosition>> layers = _map.PositionsByLayer;

		foreach (IEnumerable<Map.MapPosition> layer in layers)
		{
			int currentIndex = 0;
			
			foreach (Map.MapPosition position in layer)
			{
				HBoxContainer layerContainer = _layerContainers[position.Layer];
				
				while (currentIndex < position.Index)
				{
					layerContainer.AddChild(EmptyButton());
					currentIndex++;
				}

				TextureButton encounterButton = EncounterButton();
				layerContainer.AddChild(encounterButton);
				_positionToButton.Add(position, encounterButton);
				AttachEventToButton(encounterButton, position);
				
				currentIndex++;
			}
		}
		
		
	}

	private void AttachEventToButton(TextureButton button, Map.MapPosition buttonPosition)
	{
		button.Pressed += () =>
		{
			OnEncounterButtonPressed?.Invoke(buttonPosition);
		};
	}

	private TextureButton EncounterButton()
	{
		TextureButton button = new();
		button.TextureNormal = ResourceUtil.GetEncounterTypeTexture(EncounterType.Combat);
		button.ZIndex = 10;
		return button;
	}

	private TextureButton EmptyButton()
	{
		TextureButton empty = new();
		empty.CustomMinimumSize = new Vector2(64, 64);
		empty.Disabled = true; 
		return empty;
	}
	
	private async void DrawEdges()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		await HighlightValidMoves();
		
		foreach (Map.MapEdge edge in _map.Edges)
		{
			//GD.Print($"({edge.From.Layer}, {edge.From.Index}) - ({edge.To.Layer}, {edge.To.Index})");
			TextureButton fromButton = _positionToButton[edge.From];
			float centerOffset = (fromButton.Size.X * fromButton.Scale.X) / 2;
			
			Vector2 globalFrom = fromButton.GetGlobalPosition();
			globalFrom = new Vector2(globalFrom.X + centerOffset, globalFrom.Y + centerOffset);
			Vector2 localFrom = _vboxParent.GetGlobalTransform().AffineInverse() * globalFrom;
			
			TextureButton toButton = _positionToButton[edge.To];
			Vector2 globalTo = toButton.GetGlobalPosition();
			
			float toCenterOffset = (toButton.Size.X * toButton.Scale.X) / 2;
			
			globalTo = new Vector2(globalTo.X + toCenterOffset, globalTo.Y + toCenterOffset);
			Vector2 localTo = _vboxParent.GetGlobalTransform().AffineInverse() * globalTo;

			Line2D line = new();
			_vboxParent.AddChild(line);
			line.AddPoint(localFrom);
			line.AddPoint(localTo);
		}
	}
}