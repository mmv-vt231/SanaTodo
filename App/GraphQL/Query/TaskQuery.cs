using App.GraphQL.Type;
using App.Repository;
using GraphQL.Types;

namespace App.GraphQL.Query
{
    public class TaskQuery : ObjectGraphType
    {
        public TaskQuery(IRepository repository) {
            Field<ListGraphType<TaskType>>("Tasks")
                .Resolve(context => repository.GetTasks());
        }
    }
}
