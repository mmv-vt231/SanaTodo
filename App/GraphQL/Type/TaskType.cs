using App.Models;
using App.Repository;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace App.GraphQL.Type
{
    public class TaskType : ObjectGraphType<Models.Task>
    {
        public TaskType(IDataLoaderContextAccessor accessor, IRepository repository)
        {
            Field(t => t.Id);
            Field(t => t.Text);
            Field(t => t.Completed);
            Field(t => t.EndDate, nullable: true);
            Field(t => t.CategoryId);

            Field<CategoryType, Category>("Category")
                .ResolveAsync(context =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<int, Category>("GetCategoriesById", repository.GetCategoriesById);

                    return loader.LoadAsync(context.Source.CategoryId);
                });
        }
    }
}
