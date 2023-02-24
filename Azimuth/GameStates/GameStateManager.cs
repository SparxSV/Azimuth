namespace Azimuth.GameStates
{
	public static class GameStateManager
	{
		private static Dictionary<string, IGameState> states = new Dictionary<string, IGameState>();
		private static List<IGameState> activateStates = new List<IGameState>();

		private static List<Action> activateActions = new List<Action>();
		private static List<Action> deactivateActions = new List<Action>();

		public static void AddState(IGameState _state)
		{
			if(!states.ContainsKey(_state.ID))
				states.Add(_state.ID, _state);
		}

		public static void RemoveState(IGameState _state)
		{
			if(states.ContainsKey((_state.ID)))
				states.Remove(_state.ID);
		}

		public static void Update(float _deltaTime)
		{
			foreach(Action action in deactivateActions)
				action?.Invoke();

			foreach(Action action in activateActions)
				action?.Invoke();

			deactivateActions.Clear();
			activateActions.Clear();

			foreach(IGameState state in activateStates)
				state.Update(_deltaTime);
		}

		public static void Draw()
		{
			foreach(IGameState state in activateStates)
				state.Draw();
		}

		public static void ActivateStates(string _id)
		{
			if(states.ContainsKey(_id))
			{
				activateActions.Add(() =>
				{
					activateStates.Add(states[_id]);
					states[_id].Load();
				});
			}
		}

		public static void DeactivateStates(string _id)
		{
			if(states.ContainsKey(_id) && activateStates.Contains(states[_id]))
			{
				deactivateActions.Add(() =>
				{
					activateStates.Remove(states[_id]);
					states[_id].Unload();
				});
			}
		}
	}
}