using HotChocolate.Data.Filters;
using HotChocolate.Types;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Types;

public class CharOperationFilterInputType : FilterInputType<string>
{
    protected override void Configure(IFilterInputTypeDescriptor<string> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        // Equality
        ((IFilterInputTypeDescriptor)descriptor).Field("eq")
            .Description("Equals")
            .Type<StringType>();

        ((IFilterInputTypeDescriptor)descriptor).Field("neq")
            .Description("Not equals")
            .Type<StringType>();

        // In list
        ((IFilterInputTypeDescriptor)descriptor).Field("in")
            .Description("In list of chars")
            .Type<ListType<StringType>>();
    }
}

public class NullableCharOperationFilterInputType : FilterInputType<string?>
{
    protected override void Configure(IFilterInputTypeDescriptor<string?> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        // Equality
        ((IFilterInputTypeDescriptor)descriptor).Field("eq")
            .Description("Equals")
            .Type<StringType>();

        ((IFilterInputTypeDescriptor)descriptor).Field("neq")
            .Description("Not equals")
            .Type<StringType>();

        // In list
        ((IFilterInputTypeDescriptor)descriptor).Field("in")
            .Description("In list of chars")
            .Type<ListType<StringType>>();
    }
}