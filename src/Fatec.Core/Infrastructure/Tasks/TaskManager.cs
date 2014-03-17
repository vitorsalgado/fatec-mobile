using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fatec.Core.Infrastructure.Tasks
{
	public class TaskManager
	{
		private static readonly TaskManager _taskManager = new TaskManager();
		private object _locker = new object();
		private TaskManager() { }

		private bool _startupTasksExecuted = false;

		public void RunTasks()
		{
			if (_startupTasksExecuted)
				return;

			lock (_locker)
			{
				var startupTasks = new List<ITask>();

				var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Fatec")).ToList();
				List<Type> startupTasksDefinition = new List<Type>();

				assemblies.ForEach((Assembly assembly) =>
				{
					startupTasksDefinition.AddRange(assembly.GetTypes()
						.Where(type => typeof(ITask).IsAssignableFrom(type) && type.IsClass));
				});

				foreach (var startupTaskDefinition in startupTasksDefinition)
					startupTasks.Add((ITask)Activator.CreateInstance(startupTaskDefinition));

				Parallel.ForEach(startupTasks, x => x.Run());

				_startupTasksExecuted = true;
			}
		}

		public static TaskManager Instance 
		{ 
			get { return _taskManager; } 
		}

	}
}
