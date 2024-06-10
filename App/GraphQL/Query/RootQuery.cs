using App.GraphQL.Type;
using App.Repository;
using GraphQL;
using GraphQL.Types;

namespace App.GraphQL.Query
{
    public class RootQuery : ObjectGraphType    
    {
        public RootQuery(IRepositoryController repositoryController) 
        {
            var repository = repositoryController.Load();

            Field<ListGraphType<CategoryType>>("Categories")
                .Resolve(context => repository.GetCategories());

            Field<CategoryType>("Category")
               .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }))
               .Resolve(context => repository.GetCategory(context.GetArgument<int>("id")));

            Field<ListGraphType<TaskType>>("Tasks")
                .Resolve(context => repository.GetTasks());
        }
    }
}
