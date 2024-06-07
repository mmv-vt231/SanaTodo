using App.DTO;
using App.GraphQL.Type;
using App.Repository;
using GraphQL;
using GraphQL.Types;

namespace App.GraphQL.Mutation
{
    public class TaskMutation : ObjectGraphType
    {
        public TaskMutation(IRepository repository)
        {
            Field<IntGraphType>("CreateTask")
                .Arguments(new QueryArguments(
                    new QueryArgument<TaskInputType> { Name = "task" }
                ))
                .Resolve(context => repository.CreateTask(
                    context.GetArgument<CreateTaskDTO>("task")
                ));

            Field<IntGraphType>("CompleteTask")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "taskId" },
                    new QueryArgument<BooleanGraphType> { Name = "completed" }
                ))
                .Resolve(context => repository.CompleteTask(
                    context.GetArgument<int>("taskId"), 
                    context.GetArgument<bool>("completed")
                ));

            Field<IntGraphType>("DeleteTask")
               .Arguments(new QueryArguments(
                   new QueryArgument<IntGraphType> { Name = "taskId" }
               ))
               .Resolve(context => repository.DeleteTask(
                   context.GetArgument<int>("taskId")
               ));
        }
    }
}
