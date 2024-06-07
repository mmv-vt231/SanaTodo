using App.GraphQL.Mutation;
using App.GraphQL.Query;
using GraphQL.Types;

namespace App.GraphQL.Scheme
{
    public class APIScheme : Schema
    {
        public APIScheme(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
            Query = serviceProvider.GetRequiredService<RootQuery>();
            Mutation = serviceProvider.GetRequiredService<RootMutation>();
        }
    }
}
