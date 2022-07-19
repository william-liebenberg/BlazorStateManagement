using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.States
{
	public abstract class BaseState<T> : INotifyPropertyChanged
	{
		private T _state;

		public BaseState(T initialState)
		{
			_state = initialState;
		}

		protected T State => _state;

		public event PropertyChangedEventHandler? PropertyChanged = null!;

		protected void OnPropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

	public class Counter
	{
		public int Count { get; set; }
	}

	public class CounterState : BaseState<Counter>
	{

		public CounterState() : base(new Counter { Count = 0 })
		{
		}

		public void Reset()
		{
			State.Count = 0;
			OnPropertyChanged();
		}

		public void Increment()
		{
			++State.Count;
			OnPropertyChanged();
		}

		public void IncrementBy(int step)
		{
			State.Count += step;
			OnPropertyChanged();
		}
	}

	public class CounterListState : BaseState<List<Counter>>
	{
		public CounterListState() : base(new List<Counter>())
		{
		}

		public void Reset()
		{
			State.Clear();
			OnPropertyChanged();
		}

		public void AddCounter()
		{
			State.Add(new Counter() { Count = 0 });

			OnPropertyChanged();
		}

		public void Increment(int index, int step)
		{
			if(index < 0 || index >= State.Count)
			{
				return;
			}

			State[index].Count += step;
			OnPropertyChanged();
		}

		public IReadOnlyList<Counter> GetCounters()
		{
			return State;
		}
	}
}
