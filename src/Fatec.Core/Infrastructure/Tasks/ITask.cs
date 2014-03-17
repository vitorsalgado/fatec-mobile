namespace Fatec.Core.Infrastructure.Tasks
{
	public interface ITask
	{
		string Description { get; }
		void Run();
	}
}
