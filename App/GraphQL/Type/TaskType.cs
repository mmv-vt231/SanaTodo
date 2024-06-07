using App.Repository;
using GraphQL.Types;

namespace App.GraphQL.Type
{
    public class TaskType : ObjectGraphType<Models.Task>
    {
        public TaskType(IRepository repository)
        {
            Field(t => t.Id);
            Field(t => t.Text);
            Field(t => t.Completed);
            Field(t => t.EndDate, nullable: true);
            Field(t => t.CategoryId);

            Field<CategoryType>("Category")
                .Resolve(context => repository.GetCategory(context.Source.CategoryId));
        }
    }
}
